namespace FineHomeMadeFoodNearMe.Services.Services
{
    using FineHomeMadeFoodNearMe.Services.Models;
    using System.Collections.Generic;
    using FineHomeMadeFoodNearMe.Services.Models.Enums;

    public interface IFindHomeMadeFoodNearMeService
    {
        UserErrorModel RegisterUser(UserModel user);

        UserErrorModel LoginUser(LoginModel login);

        ErrorModel AddDishToMenu(AddDishModel model);

        List<DishModel> GetDishesByProviderId(long providerId);

        ErrorModel RemoveDishFromMenu(RemoveDishModel removeModel);

        UserModel GetUser(long userId);

        ErrorModel PlaceOrder(PlaceOrderModel orderPlaceModel);

        List<UserModel> FindProvidersWithinRange(SearchFoodModel searchModel);

        List<UserModel> GetRegisteredUsers();

        ErrorModel UpdateOrderItemStatus(UpdateOrderItemModel updateModel);
    }
}
