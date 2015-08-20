namespace FineHomeMadeFoodNearMe.Services.Services
{
    using FineHomeMadeFoodNearMe.Services.Models;
    using System.Collections.Generic;

    public interface IFindHomeMadeFoodNearMeService
    {
        ErrorModel RegisterUser(UserModel user);

        ErrorModel AddDishToMenu(DishModel dish, long userId);

        List<DishModel> GetDishListByProviderId(long providerId);

        ErrorModel RemoveDishFromMenu(long dishId, long providerId);

        UserModel GetUserInfoById(long userId);

        OrderModel PlaceOrder(List<long> dishIds, long userId);

        List<UserModel> FindProvidersWithinRange(double latitude, double longitude, int range);

        List<UserModel> GetRegisteredUsers();
    }
}
