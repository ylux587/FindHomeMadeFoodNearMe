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
        public UserErrorModel LoginUser(LoginModel login)
        {
            return Service.LoginUser(login);
        }

        [HttpGet]
        public List<UserModel> RegisteredUsers()
        {
            return Service.GetRegisteredUsers();
        }

        [HttpPost]
        public ErrorModel AddDishToMenu(AddDishModel addModel)
        {
            return Service.AddDishToMenu(addModel);
        }

        [HttpGet]
        public List<DishModel> GetDishesByProviderId(long providerId)
        {
            return Service.GetDishesByProviderId(providerId);
        }

        [HttpPost]
        public ErrorModel RemoveDishFromMenu(RemoveDishModel removeModel)
        {
            return Service.RemoveDishFromMenu(removeModel);
        }

        [HttpGet]
        public UserModel GetUser(long userId)
        {
            return Service.GetUser(userId);
        }

        [HttpPost]
        public ErrorModel PlaceOrder(PlaceOrderModel orderPlaceModel)
        {
            return Service.PlaceOrder(orderPlaceModel);
        }

        [HttpGet]
        public List<UserModel> FindProvidersWithinRange(SearchFoodModel searchModel)
        {
            return Service.FindProvidersWithinRange(searchModel);
        }

        [HttpPost]
        public ErrorModel UpdateOrderItemStatus(UpdateOrderItemModel updateModel)
        {
            return Service.UpdateOrderItemStatus(updateModel);
        }
    }
}
