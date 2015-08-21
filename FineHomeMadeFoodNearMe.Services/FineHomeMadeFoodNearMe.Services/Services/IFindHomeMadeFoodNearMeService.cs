namespace FineHomeMadeFoodNearMe.Services.Services
{
    using FineHomeMadeFoodNearMe.Services.Models;
    using System.Collections.Generic;
    using FineHomeMadeFoodNearMe.Services.Models.Enums;

    public interface IFindHomeMadeFoodNearMeService
    {
        ErrorModel RegisterUser(UserModel user);

        ErrorModel AddDishToMenu(DishModel dish, long userId);

        List<DishModel> GetDishesByProviderId(long providerId);

        ErrorModel RemoveDishFromMenu(long dishId, long providerId);

        UserModel GetUser(long userId);

        ErrorModel PlaceOrder(List<long> dishIds, long userId);

        List<UserModel> FindProvidersWithinRange(double latitude, double longitude, int range);

        List<UserModel> GetRegisteredUsers();

        ErrorModel RemoveDish(long dishId, long providerId);

        ErrorModel UpdateOrderItemStatus(long orderId, long dishId, ItemStatus targetStatus);
    }
}
