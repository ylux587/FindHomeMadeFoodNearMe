namespace FineHomeMadeFoodNearMe.Services.Controllers
{

    using System.Collections.Generic;
    using System.Web.Http;
    using FineHomeMadeFoodNearMe.Services.DataAccess;
    using FineHomeMadeFoodNearMe.Services.Models;
    using FineHomeMadeFoodNearMe.Services.Services;

    public class SearchFoodController : ApiController
    {
        private static readonly IFindHomeMadeFoodNearMeService Service = new FindHomeMadeFoodNearMeService();

        [HttpPost]
        public ErrorModel RegisterUser(UserModel user)
        {
            return Service.RegisterUser(user);
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
        public List<DishModel> GetDishListByProviderId(long providerId)
        {
            return Service.GetDishListByProviderId(providerId);
        }

        [HttpPost]
        public ErrorModel RemoveDishFromMenu(long dishId, long providerId)
        {
            return Service.RemoveDishFromMenu(dishId, providerId);
        }

        [HttpGet]
        public UserModel GetUserInfoById(long userId)
        {
            return Service.GetUserInfoById(userId);
        }

        [HttpPost]
        public OrderModel PlaceOrder(List<long> dishIds, long userId)
        {
            return Service.PlaceOrder(dishIds, userId);
        }

        [HttpGet]
        public List<UserModel> FindProvidersWithinRange(double latitude, double longitude, int range)
        {
            return Service.FindProvidersWithinRange(latitude, longitude, range);
        }
    }
}
