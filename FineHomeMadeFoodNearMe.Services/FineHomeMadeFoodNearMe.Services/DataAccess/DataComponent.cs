namespace FineHomeMadeFoodNearMe.Services.DataAccess
{
    using System.Collections.Generic;
    using Entities;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using FindHomeMadeFoodNearMe.DataAccessHelper;


    public sealed class DataComponent : IDataComponent
    {
        private static readonly IDataAccessHelper dbContext = new DataAccessHelper(ConnectionString);

        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["FindHomeMadeFoodNearMe"].ConnectionString; }
        }

        public static int Timeout
        {
            get { return 20; }
        }

        public IList<UserEntity> GetUsers()
        {
            return dbContext.ExecuteReader("GetUsers", UserEntity.UserEntityColumns.Instance, null, Timeout);
        }

        public IList<DishEntity> GetDishes()
        {
            return dbContext.ExecuteReader("GetDishes", DishEntity.DishEntityColumns.Instance, null, Timeout);
        }

        public IList<OrderEntity> GetOrders()
        {
            return dbContext.ExecuteReader("GetOrders", OrderEntity.OrderEntityColumns.Instance, null, Timeout);
        }

        public IList<OrderItemEntity> GetOrderItems()
        {
            return dbContext.ExecuteReader("GetOrderItems", OrderItemEntity.OrderItemEntityColumns.Instance, null, Timeout);
        }

        public void SaveUsers(IList<UserEntity> usersToSave)
        {
            if (usersToSave == null || !usersToSave.Any())
            {
                return;
            }

            var sqlParam = UserEntity.BindUserTable("@users", usersToSave);
            dbContext.ExecuteNonQuery("SaveUsers", Timeout, sqlParam);
        }

        public void SaveDishes(IList<DishEntity> dishesToSave)
        {
            if (dishesToSave == null || !dishesToSave.Any())
            {
                return;
            }

            var sqlParam = DishEntity.BindDishTable("@dishes", dishesToSave);
            dbContext.ExecuteNonQuery("SaveDishes", Timeout, sqlParam);
        }

        public void SaveOrders(IList<OrderEntity> ordersToSave)
        {
            if (ordersToSave == null || !ordersToSave.Any())
            {
                return;
            }

            var sqlParam = OrderEntity.BindOrderTable("@orders", ordersToSave);
            dbContext.ExecuteNonQuery("SaveOrders", Timeout, sqlParam);
        }

        public void SaveOrderItem(long orderId, IList<OrderItemEntity> orderItemsToSave)
        {
            if (orderItemsToSave == null || !orderItemsToSave.Any())
            {
                return;
            }

            var sqlParams = new [] {OrderItemEntity.BindOrderItemTable("@@orderItems", orderItemsToSave), new SqlParameter("@orderId", SqlDbType.BigInt) {Value = orderId}};
            dbContext.ExecuteNonQuery("SaveOrderItems", Timeout, sqlParams);
        }

        public IList<UserEntity> GetProvidersInRange(double latitude, double longitude, int range)
        {
            var sqlParams = new[]
            {
                new SqlParameter("@sourceLatitude", SqlDbType.Float) {Value = latitude}, 
                new SqlParameter("@sourceLongtitude", SqlDbType.Float) {Value = longitude}, 
                new SqlParameter("@range", SqlDbType.Int) {Value = range} 
            };
            return dbContext.ExecuteReader("GetProvidersInRange", UserEntity.UserEntityColumns.Instance, null, Timeout, sqlParams);
        }

        public void CancelOrder(long orderId)
        {
            dbContext.ExecuteNonQuery("CancelOrder", Timeout, new SqlParameter("@orderId", SqlDbType.BigInt) { Value = orderId });
        }
    }
}