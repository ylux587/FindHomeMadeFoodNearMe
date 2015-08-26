namespace FindHomeMadeFoodNearMe.Services.DataAccess
{
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;
    using System.Collections.Generic;
    using FindHomeMadeFoodNearMe.Services.Models.Enums;

    public interface IDataComponent
    {
        IList<UserEntity> GetUsers();

        IList<ProviderEntity> GetProviders();

        IList<DishEntity> GetDishes();

        IList<OrderEntity> GetOrders();

        IList<OrderItemEntity> GetOrderItems();

        void SaveUsers(IList<UserEntity> usersToSave);

        void SaveProviders(IList<ProviderEntity> providersToSave);

        void SaveDishes(IList<DishEntity> dishesToSave);

        void SaveOrder(OrderEntity orderToSave, IList<OrderItemEntity> itemsToSave);

        void SaveOrderItems(long orderId, IList<OrderItemEntity> orderItemsToSave);

        IList<UserEntity> GetProvidersInRange(double latitude, double longitude, int range);

        void CancelOrder(long orderId);

        void RemoveDish(long dishId, long providerId);

        void UpdateOrderItemStatus(long orderId, long dishId, long providerId, ItemStatus targetStatus);
    }
}
