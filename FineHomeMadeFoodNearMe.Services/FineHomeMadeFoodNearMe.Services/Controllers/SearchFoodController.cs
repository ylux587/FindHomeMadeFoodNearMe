namespace FindHomeMadeFoodNearMe.Services.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using FindHomeMadeFoodNearMe.Services.Models;
    using FindHomeMadeFoodNearMe.Services.Services;
    using FindHomeMadeFoodNearMe.Services.Models.Enums;

    public class SearchFoodController : ApiController
    {
        private static readonly IFindHomeMadeFoodNearMeService Service = new FindHomeMadeFoodNearMeService();

        [HttpPost]
        public UserErrorModel RegisterUser(RegisterModel model)
        {
            return Service.RegisterUser(model);
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
        public List<DishModel> GetDishes(long providerId)
        {
            return Service.GetDishes(providerId);
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

        [HttpPost]
        public SearchFoodResultModel FindProvidersWithinRange(SearchFoodModel searchModel)
        {
            return Service.FindProvidersWithinRange(searchModel);
        }

        [HttpPost]
        public AddressSearchFoodResultModel FindProvidersWithinRangeByAddress(AddressModel addressModel)
        {
            return Service.FindProvidersWithinRangeByAddress(addressModel);
        }

        [HttpPost]
        public ErrorModel UpdateOrderItemStatus(UpdateOrderItemModel updateModel)
        {
            return Service.UpdateOrderItemStatus(updateModel);
        }
    }
}
