namespace FineHomeMadeFoodNearMe.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FineHomeMadeFoodNearMe.Services.DataAccess;
    using FineHomeMadeFoodNearMe.Services.DataAccess.Entities;
    using FineHomeMadeFoodNearMe.Services.GeoLocationService;
    using FineHomeMadeFoodNearMe.Services.Models;
    using FineHomeMadeFoodNearMe.Services.Models.Enums;

    public sealed class FindHomeMadeFoodNearMeService : IFindHomeMadeFoodNearMeService
    {
        private static readonly IDataComponent DbContext = new DataComponent();

        private static readonly IGeoSearchProvider GeoServiceProvider = new BingGeoSearchProvider();

        public UserErrorModel RegisterUser(UserModel user)
        {
            var errors = new UserErrorModel();
            if (user == null)
            {
                errors.Messages.Add("No user model found");
                return errors;
            }

            if (
                DbContext.GetUsers()
                    .Any(
                        u =>
                            string.Equals(u.Email, user.Email, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(u.PhoneNumber, user.PhoneNumber, StringComparison.OrdinalIgnoreCase)))
            {
                errors.Messages.Add("The email address or phone number you are registering is not available. Please choose another email and phone number.");
                return errors;
            }

            var userStatus = UserStatus.PendingVerification;
            var latitude = 0d;
            var longitude = 0d;
            var coordinates = GeoServiceProvider.FindGeoLocationByAddress(user.StateOrProvince, user.ZipCode, user.City,
                user.AddressLine1);

            if (coordinates == null || coordinates.Length != 2)
            {
                errors.Messages.Add("Cannot verify the users address");
                userStatus = UserStatus.FailedOnVerifyAddress;
            }
            else
            {
                latitude = coordinates[0];
                longitude = coordinates[1];
            }
            var newUser = user.ToEntity();
            newUser.Status = userStatus;
            newUser.GeoLatitude = latitude;
            newUser.GeoLongitude = longitude;

            try
            {
                DbContext.SaveUsers(new List<UserEntity>{newUser});
                errors.UserId = DbContext.GetUsers().Single(u => string.Equals(u.Email, newUser.Email, StringComparison.OrdinalIgnoreCase)).UserId;
                return errors;
            }
            catch(Exception ex)
            {
                return new UserErrorModel {Messages = new List<string> {ex.Message}};
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
                errors.Messages.Add("No dish found to add");
                return errors;
            }

            var user = DbContext.GetUsers().SingleOrDefault(u => u.UserId == model.ProviderId);
            if (user == null || user.Status == UserStatus.FailedOnVerifyAddress)
            {
                errors.Messages.Add("Cannot add dish for any address not verified user.");
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
                return new ErrorModel { Messages = new List<string> { ex.Message } };
            }
        }

        public List<DishModel> GetDishesByProviderId(long providerId)
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

        public ErrorModel PlaceOrder(PlaceOrderModel orderPlaceModel)
        {
            var errors = new ErrorModel();
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

        public List<UserModel> FindProvidersWithinRange(SearchFoodModel searchModel)
        {
            if (searchModel == null)
            {
                return null;
            }
            return DbContext.GetProvidersInRange(searchModel.Latitude, searchModel.Longitude, searchModel.Range).Select(UserModel.CreateFromEntity).ToList();
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