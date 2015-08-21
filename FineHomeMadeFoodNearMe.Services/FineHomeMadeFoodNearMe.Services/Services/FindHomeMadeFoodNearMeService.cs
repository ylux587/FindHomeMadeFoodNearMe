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

        public ErrorModel RegisterUser(UserModel user)
        {
            var errors = new ErrorModel();
            if (user == null)
            {
                errors.Messages.Add("No user model found");
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
                return errors;
            }
            catch(Exception ex)
            {
                return new ErrorModel {Messages = new List<string> {ex.Message}};
            }
        }

        public ErrorModel AddDishToMenu(DishModel dish, long userId)
        {
            var errors = new ErrorModel();
            
            if (dish == null)
            {
                errors.Messages.Add("No dish found to add");
                return errors;
            }

            var newDish = dish.ToEntity();
            newDish.ProviderId = userId;
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
                .Select(d => DishModel.CreateFromEntity(d))
                .ToList();
        }

        public ErrorModel RemoveDishFromMenu(long dishId, long providerId)
        {
            var errors = new ErrorModel();

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

        public ErrorModel PlaceOrder(List<long> dishIds, long userId)
        {
            var errors = new ErrorModel();
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

        public List<UserModel> FindProvidersWithinRange(double latitude, double longitude, int range)
        {
            return DbContext.GetProvidersInRange(latitude, longitude, range).Select(u => UserModel.CreateFromEntity(u)).ToList();
        }


        public List<UserModel> GetRegisteredUsers()
        {
            return DbContext.GetUsers().Select(u => UserModel.CreateFromEntity(u)).ToList();
        }


        public ErrorModel RemoveDish(long dishId, long providerId)
        {
            var errors = new ErrorModel();

            try
            {
                DbContext.RemoveDish(dishId, providerId);
            }
            catch(Exception ex)
            {
                errors.Messages.Add(ex.Message);
            }
            return errors;
        }

        public ErrorModel UpdateOrderItemStatus(long orderId, long dishId, ItemStatus targetStatus)
        {
            var errors = new ErrorModel();

            try
            {
                DbContext.UpdateOrderItemStatus(orderId, dishId, targetStatus);
            }
            catch (Exception ex)
            {
                errors.Messages.Add(ex.Message);
            }
            return errors;
        }
    }
}