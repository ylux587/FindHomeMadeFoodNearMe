namespace FineHomeMadeFoodNearMe.Services.DataAccess
{
    using FineHomeMadeFoodNearMe.Services.DataAccess.Entities;
    using System.Collections.Generic;
    using FineHomeMadeFoodNearMe.Services.Models.Enums;

    public interface IDataComponent
    {
        IList<UserEntity> GetUsers();

        IList<DishEntity> GetDishes();

        IList<OrderEntity> GetOrders();

        IList<OrderItemEntity> GetOrderItems();

        void SaveUsers(IList<UserEntity> usersToSave);

        void SaveDishes(IList<DishEntity> dishesToSave);

        void SaveOrder(OrderEntity orderToSave, IList<OrderItemEntity> itemsToSave);

        void SaveOrderItems(long orderId, IList<OrderItemEntity> orderItemsToSave);

        IList<UserEntity> GetProvidersInRange(double latitude, double longitude, int range);

        void CancelOrder(long orderId);

        void RemoveDish(long dishId, long providerId);

        void UpdateOrderItemStatus(long orderId, long dishId, ItemStatus targetStatus);
    }
}
