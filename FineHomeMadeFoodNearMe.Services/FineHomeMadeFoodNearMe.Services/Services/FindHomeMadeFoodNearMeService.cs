namespace FindHomeMadeFoodNearMe.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FindHomeMadeFoodNearMe.Services.DataAccess;
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;
    using FindHomeMadeFoodNearMe.Services.GeoLocationService;
    using FindHomeMadeFoodNearMe.Services.Models;
    using FindHomeMadeFoodNearMe.Services.Models.Enums;

    public sealed class FindHomeMadeFoodNearMeService : IFindHomeMadeFoodNearMeService
    {
        private static readonly IDataComponent DbContext = new DataComponent();

        private static readonly IGeoSearchProvider GeoServiceProvider = new BingGeoSearchProvider();

        public UserErrorModel RegisterUser(RegisterModel model)
        {
            var errors = new UserErrorModel();
            if (model == null)
            {
                errors.Messages.Add("Invalid data.");
                return errors;
            }

            if (model.UserInfo == null)
            {
                errors.Messages.Add("Invalid user data.");
                return errors;
            }

            if (
                DbContext.GetUsers()
                    .Any(
                        u =>
                            string.Equals(u.Email, model.UserInfo.Email, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(u.PhoneNumber, model.UserInfo.PhoneNumber, StringComparison.OrdinalIgnoreCase)))
            {
                errors.Messages.Add("The email address or phone number you are registering is not available. Please choose another email and phone number.");
                return errors;
            }

            var newUser = model.UserInfo.ToEntity();
            newUser.UserStatus = UserStatus.PendingVerification;

            var newProvider = default(ProviderEntity);
            if (model.ProviderInfo != null)
            {
                newProvider = model.ProviderInfo.ToEntity();
                var coordinates = GeoServiceProvider.FindGeoLocationByAddress(model.ProviderInfo.Address.StateOrProvince, model.ProviderInfo.Address.ZipCode, model.ProviderInfo.Address.City,
                    model.ProviderInfo.Address.FullAddressLine);

                if (coordinates == null || coordinates.Length != 2)
                {
                    errors.Messages.Add("Cannot verify the provider address");
                    newProvider.ProviderStatus = ProviderStatus.FailedOnVerifyAddress;
                }
                else
                {
                    newProvider.GeoLatitude = coordinates[0];
                    newProvider.GeoLongitude = coordinates[1];
                    newProvider.ProviderStatus = ProviderStatus.Verified;
                }
            }

            try
            {
                DbContext.SaveUsers(new List<UserEntity> { newUser });
                errors.UserId = DbContext.GetUsers().Single(u => string.Equals(u.Email, newUser.Email, StringComparison.OrdinalIgnoreCase)).UserId;
                if (newProvider != null)
                {
                    newProvider.ProviderId = errors.UserId;
                    DbContext.SaveProviders(new List<ProviderEntity> { newProvider });
                }

                return errors;
            }
            catch (Exception ex)
            {
                return new UserErrorModel { Messages = new List<string> { ex.Message } };
            }
        }

        public ErrorModel AddDishToMenu(AddDishModel model)
        {
            var errors = new ErrorModel();
            if (model == null)
            {
                errors.Messages.Add("Invalid data.");
                return errors;
            }

            var dish = model.DishToAdd;
            if (dish == null)
            {
                errors.Messages.Add("No dish found to add.");
                return errors;
            }

            var provider = DbContext.GetProviders().SingleOrDefault(p => p.ProviderId == model.ProviderId);
            if (provider == null || provider.ProviderStatus != ProviderStatus.Verified)
            {
                errors.Messages.Add("Cannot add dish for any address not verified provider.");
                return errors;
            }

            var newDish = dish.ToEntity();
            newDish.ProviderId = model.ProviderId;
            newDish.Available = true;

            try 
            { 
                DbContext.SaveDishes(new List<DishEntity>{newDish});
                return errors;
            }
            catch (Exception ex)
            {
                errors.Messages.Add(ex.Message);
                return errors;
            }
        }

        public List<DishModel> GetDishes(long providerId)
        {
            return DbContext.GetDishes()
                .Where(d => d.ProviderId == providerId)
                .Select(DishModel.CreateFromEntity)
                .ToList();
        }

        public ErrorModel RemoveDishFromMenu(RemoveDishModel removeModel)
        {
            var errors = new ErrorModel();

            if (removeModel == null)
            {
                errors.Messages.Add("Invalid data.");
                return errors;
            }
            var dishId = removeModel.DishId;
            var providerId = removeModel.ProviderId;

            try
            {
                DbContext.RemoveDish(dishId, providerId);
                return errors;
            }
            catch (Exception ex)
            {
                errors.Messages.Add(ex.Message);
                return errors;
            }
            
        }

        public UserModel GetUser(long userId)
        {
            var user = DbContext.GetUsers().SingleOrDefault(u => u.UserId == userId);
            return user == null ? null : UserModel.CreateFromEntity(user);
        }

        public OrderErrorModel PlaceOrder(PlaceOrderModel orderPlaceModel)
        {
            var errors = new OrderErrorModel();
            if (orderPlaceModel == null)
            {
                errors.Messages.Add("Invalid data.");
                return errors;
            }
            var dishIds = orderPlaceModel.DishIds;
            var userId = orderPlaceModel.UserId;

            if (dishIds == null || dishIds.Count == 0)
            {
                errors.Messages.Add("No items found to order");
                return errors;
            }
            var dishes = DbContext.GetDishes().Where(d => dishIds.Contains(d.DishId)).ToList();
            var newOrder = new OrderEntity {OrderDate = DateTime.Now, UserId = userId, SubTotal = dishes.Sum(d => d.Price)};

            var dishesToOrder = dishIds.Select(dishId => new OrderItemEntity
            {
                DishId = dishId,
                ItemStatus = ItemStatus.Confirmed,
            }).ToList();

            try
            {
                DbContext.SaveOrder(newOrder, dishesToOrder);
                return errors;
            }
            catch(Exception ex)
            {
                errors.Messages.Add(ex.Message);
                return errors;
            }
        }

        public SearchFoodResultModel FindProvidersWithinRange(SearchFoodModel searchModel)
        {
            if (searchModel == null)
            {
                return null;
            }
            var providers = DbContext.GetProvidersInRange(searchModel.Latitude, searchModel.Longitude, searchModel.Range).Select(ProviderModel.CreateFromEntity).ToList();
            var users = DbContext.GetUsers();
            return new SearchFoodResultModel { ProviderInfos = providers.Select(p => new UserProviderModel { UserInfo = UserModel.CreateFromEntity(users.Single(u => u.UserId == p.ProviderId)), ProviderInfo = p }).ToList() };
        }

        public AddressSearchFoodResultModel FindProvidersWithinRangeByAddress(AddressModel addressModel)
        {
            if (addressModel == null)
            {
                return null;
            }
            var coordinates = GeoServiceProvider.FindGeoLocationByAddress(addressModel.StateOrProvince, addressModel.ZipCode, addressModel.City, addressModel.FullAddressLine);
            if (coordinates == null || coordinates.Length != 2)
            {
                return null;
            }
            else
            {
                return new AddressSearchFoodResultModel { AddressLatitude = coordinates[0], AddressLongitude = coordinates[1], ProviderInfos = FindProvidersWithinRange(new SearchFoodModel { Latitude = coordinates[0], Longitude = coordinates[1], Range = 25 }).ProviderInfos };
            }
        }

        public List<UserModel> GetRegisteredUsers()
        {
            return DbContext.GetUsers().Select(UserModel.CreateFromEntity).ToList();
        }


        public ErrorModel UpdateOrderItemStatus(UpdateOrderItemModel updateModel)
        {
            var errors = new ErrorModel();
            if (updateModel == null)
            {
                errors.Messages.Add("Invalid data. Please try again!");
                return errors;
            }

            try
            {
                DbContext.UpdateOrderItemStatus(updateModel.OrderId, updateModel.DishId, updateModel.ProviderId, updateModel.TargetStatus);
            }
            catch (Exception ex)
            {
                errors.Messages.Add(ex.Message);
            }
            return errors;
        }


        public UserErrorModel LoginUser(LoginModel login)
        {
            var errors = new UserErrorModel();
            if (login == null)
            {
                errors.Messages.Add("Invalid data.");
                return errors;
            }
            var email = login.Username;
            var password = login.Password;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                errors.Messages.Add("Invalid username/password.");
                return errors;
            }
            var user =
                DbContext.GetUsers()
                    .SingleOrDefault(u => string.Equals(email, u.Email, StringComparison.OrdinalIgnoreCase));
            if (user != null && string.Equals(user.Password, password, StringComparison.Ordinal))
            {
                errors.UserId = user.UserId;
            }
            else
            {
                errors.Messages.Add("Invalid username/password.");
            }
            return errors;
        }
    }
}