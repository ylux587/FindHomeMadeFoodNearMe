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

    public class FindHomeMadeFoodNearMeService : IFindHomeMadeFoodNearMeService
    {
        private static readonly DataComponent DbContext = new DataComponent();

        private static readonly IGeoSearchProvider GeoServiceProvider = new BingGeoSearchProvider();

        public ErrorModel RegisterUser(UserModel user)
        {
            var errors = new ErrorModel { Messages = new List<string>() };
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
            var errors = new ErrorModel { Messages = new List<string>() };
            
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

        public List<DishModel> GetDishListByProviderId(long providerId)
        {
            return DbContext.GetDishes()
                .Where(d => d.ProviderId == providerId)
                .Select(d => DishModel.CreateFromEntity(d))
                .ToList();
        }

        public ErrorModel RemoveDishFromMenu(long dishId, long providerId)
        {
            var dishToRemove = DbContext.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if (dishToRemove == null || dishToRemove.ProviderId != providerId)
            {
                return new ErrorModel {Messages = new List<string>{"No qualified dish found"}};
            }

            try
            {
                DbContext.Dishes.Remove(dishToRemove);
                DbContext.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
               return new ErrorModel {Messages = new List<string> {ex.Message}};
            }
            
        }

        public UserModel GetUser(long userId)
        {
            var user = DbContext.GetUsers().SingleOrDefault(u => u.UserId == userId);
            return user == null ? null : UserModel.CreateFromEntity(user);
        }

        public OrderModel PlaceOrder(List<long> dishIds, long userId)
        {
            if (dishIds == null || dishIds.Count == 0)
            {
                return null;
            }
            var dishes = DbContext.Dishes.Where(d => dishIds.Contains(d.DishId)).ToList();
            var newOrder = new Order {OrderDate = DateTime.Now, UserId = userId, SubTotal = dishes.Sum(d => d.Price)};
            DbContext.Orders.Add(newOrder);
            DbContext.SaveChanges();
            var orderId = newOrder.OrderId;
            var dishesToOrder = dishIds.Select(dishId => new OrderItem
            {
                DishId = dishId,
                ItemStatus = ItemStatus.Confirmed,
                OrderId = orderId
            });

            foreach (var newOrderItem in dishesToOrder)
            {
                DbContext.OrderItems.Add(newOrderItem);
            }
            DbContext.SaveChanges();
            return OrderModel.CreateFromEntity(newOrder, DbContext);
        }

        public List<UserModel> FindProvidersWithinRange(double latitude, double longitude, int range)
        {
            return DbContext.GetProvidersInRange(latitude, longitude, range).Select(u => UserModel.CreateFromEntity(u)).ToList();
        }


        public List<UserModel> GetRegisteredUsers()
        {
            return DbContext.GetUsers().Select(u => UserModel.CreateFromEntity(u)).ToList();
        }
    }
}