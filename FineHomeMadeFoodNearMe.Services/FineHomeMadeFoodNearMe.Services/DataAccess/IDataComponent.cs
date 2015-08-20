namespace FineHomeMadeFoodNearMe.Services.DataAccess
{
    using FineHomeMadeFoodNearMe.Services.DataAccess.Entities;
    using System.Collections.Generic;

    public interface IDataComponent
    {
        IList<UserEntity> GetUsers();

        IList<DishEntity> GetDishes();

        IList<OrderEntity> GetOrders();

        IList<OrderItemEntity> GetOrderItems();

        void SaveUsers(IList<UserEntity> usersToSave);

        void SaveDishes(IList<DishEntity> dishesToSave);

        void SaveOrders(IList<OrderEntity> ordersToSave);

        void SaveOrderItem(long orderId, IList<OrderItemEntity> orderItemsToSave);

        IList<UserEntity> GetProvidersInRange(double latitude, double longitude, int range);

        void CancelOrder(long orderId);
    }
}
