namespace FindHomeMadeFoodNearMe.Services.Services
{
    using FindHomeMadeFoodNearMe.Services.Models;
    using System.Collections.Generic;
    using FindHomeMadeFoodNearMe.Services.Models.Enums;

    public interface IFindHomeMadeFoodNearMeService
    {
        UserErrorModel RegisterUser(RegisterModel model);

        UserErrorModel LoginUser(LoginModel login);

        ErrorModel AddDishToMenu(AddDishModel model);

        List<DishModel> GetDishes(long providerId);

        ErrorModel RemoveDishFromMenu(RemoveDishModel removeModel);

        UserModel GetUser(long userId);

        OrderErrorModel PlaceOrder(PlaceOrderModel orderPlaceModel);

        SearchFoodResultModel FindProvidersWithinRange(SearchFoodModel searchModel);

        AddressSearchFoodResultModel FindProvidersWithinRangeByAddress(AddressModel searchModel);

        List<UserModel> GetRegisteredUsers();

        ErrorModel UpdateOrderItemStatus(UpdateOrderItemModel updateModel);
    }
}
