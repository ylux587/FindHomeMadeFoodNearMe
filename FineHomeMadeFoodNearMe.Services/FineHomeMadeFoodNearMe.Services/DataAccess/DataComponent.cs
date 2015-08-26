namespace FindHomeMadeFoodNearMe.Services.DataAccess
{
    using System.Collections.Generic;
    using Entities;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using FindHomeMadeFoodNearMe.DataAccessHelper;
    using FindHomeMadeFoodNearMe.Services.Models.Enums;

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

        public IList<ProviderEntity> GetProviders()
        {
            return dbContext.ExecuteReader("GetProviders", ProviderEntity.ProviderEntityColumns.Instance, null, Timeout);
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

        public void SaveProviders(IList<ProviderEntity> providersToSave)
        {
            if (providersToSave == null || !providersToSave.Any())
            {
                return;
            }

            var sqlParam = ProviderEntity.BindProviderTable("@providers", providersToSave);
            dbContext.ExecuteNonQuery("SaveProviders", Timeout, sqlParam);
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

        public void SaveOrder(OrderEntity orderToSave, IList<OrderItemEntity> itemsToSave)
        {
            if (orderToSave == null || !itemsToSave.Any())
            {
                return;
            }

            var userIdParam = new SqlParameter("@userId", SqlDbType.BigInt) { Value = orderToSave.UserId };
            var orderDateParam = new SqlParameter("@orderDate", SqlDbType.DateTime2) { Value = orderToSave.OrderDate };
            var subTotalParam = new SqlParameter("@subTotal", SqlDbType.Decimal) { Value = orderToSave.SubTotal };
            var taxParam = new SqlParameter("@tax", SqlDbType.Decimal) { Value = orderToSave.Tax };
            var otherChargesParam = new SqlParameter("@otherCharges", SqlDbType.Decimal) { Value = orderToSave.OtherCharges };
            var notesParam = new SqlParameter("@notes", SqlDbType.NVarChar) { Value = orderToSave.Notes };
            var statusParam = new SqlParameter("@status", SqlDbType.Int) { Value = (int) orderToSave.Status };
            var orderItemParam = OrderItemEntity.BindOrderItemTable("@orderItems", itemsToSave);
            dbContext.ExecuteNonQuery("SaveOrders", Timeout, userIdParam, orderDateParam, subTotalParam, taxParam, otherChargesParam, notesParam, statusParam, orderItemParam);
        }

        public void SaveOrderItems(long orderId, IList<OrderItemEntity> orderItemsToSave)
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


        public void RemoveDish(long dishId, long providerId)
        {
            dbContext.ExecuteNonQuery("RemoveDish", Timeout, new[] 
            {
                new SqlParameter("@dishId", SqlDbType.BigInt){ Value = dishId},
                new SqlParameter("@providerId", SqlDbType.BigInt){ Value = providerId},
            });
        }

        public void UpdateOrderItemStatus(long orderId, long dishId, long providerId, ItemStatus targetStatus)
        {
            dbContext.ExecuteNonQuery("UpdateOrderItemStatus", Timeout, new[] 
            {
                new SqlParameter("@orderId", SqlDbType.BigInt){ Value = orderId},
                new SqlParameter("@dishId", SqlDbType.BigInt){ Value = dishId},
                new SqlParameter("@providerId", SqlDbType.BigInt){ Value = providerId},
                new SqlParameter("@targetStatus", SqlDbType.Int){ Value = (int) targetStatus},
            });
        }
    }
}