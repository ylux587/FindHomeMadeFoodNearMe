namespace FineHomeMadeFoodNearMe.Services.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using FineHomeMadeFoodNearMe.Services.Models;
    using FineHomeMadeFoodNearMe.Services.Services;
    using FineHomeMadeFoodNearMe.Services.Models.Enums;

    public class SearchFoodController : ApiController
    {
        private static readonly IFindHomeMadeFoodNearMeService Service = new FindHomeMadeFoodNearMeService();

        [HttpPost]
        public UserErrorModel RegisterUser(UserModel user)
        {
            return Service.RegisterUser(user);
        }

        [HttpPost]
        public long LoginUser(string email, string password)
        {
            return Service.LoginUser(email, password);
        }

        [HttpGet]
        public List<UserModel> RegisteredUsers()
        {
            return Service.GetRegisteredUsers();
        }

        [HttpPost]
        public ErrorModel AddDishToMenu(DishModel dish, long userId)
        {
            return Service.AddDishToMenu(dish, userId);
        }

        [HttpGet]
        public List<DishModel> GetDishesByProviderId(long providerId)
        {
            return Service.GetDishesByProviderId(providerId);
        }

        [HttpPost]
        public ErrorModel RemoveDishFromMenu(long dishId, long providerId)
        {
            return Service.RemoveDishFromMenu(dishId, providerId);
        }

        [HttpGet]
        public UserModel GetUser(long userId)
        {
            return Service.GetUser(userId);
        }

        [HttpPost]
        public ErrorModel PlaceOrder(List<long> dishIds, long userId)
        {
            return Service.PlaceOrder(dishIds, userId);
        }

        [HttpGet]
        public List<UserModel> FindProvidersWithinRange(double latitude, double longitude, int range)
        {
            return Service.FindProvidersWithinRange(latitude, longitude, range);
        }

        [HttpPost]
        public ErrorModel UpdateOrderItemStatus(long orderId, long dishId, ItemStatus targetStatus)
        {
            return Service.UpdateOrderItemStatus(orderId, dishId, targetStatus);
        }
    }
}
