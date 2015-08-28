namespace FindHomeMadeFoodNearMe.Services.DataAccess.Entities
{
    using Models.Enums;
    using DataAccess;
    using System;
    using System.Runtime.Serialization;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.SqlServer.Server;
    using System.Data;
    using System.Data.SqlClient;
    using FindHomeMadeFoodNearMe.DataAccessHelper;

    public sealed class OrderItemEntity
    {
        public long OrderId { get; set; }

        public long DishId { get; set; }

        public int Quantity { get; set; }

        public ItemStatus ItemStatus { get; set; }

        public class OrderItemEntityColumns : IEntityBinder<OrderItemEntity>
        {
            private SqlColumnBinder orderIdColumn = new SqlColumnBinder("OrderId");
            private SqlColumnBinder dishIdColumn = new SqlColumnBinder("DishId");
            private SqlColumnBinder quantityColumn = new SqlColumnBinder("Quantity");
            private SqlColumnBinder itemStatusColumn = new SqlColumnBinder("ItemStatus");

            private static IEntityBinder<OrderItemEntity> instance;

            private OrderItemEntityColumns()
            {
            }

            public static IEntityBinder<OrderItemEntity> Instance
            {
                get { return instance ?? (instance = new OrderItemEntityColumns()); }
            }

            public OrderItemEntity BindEntity(SqlDataReader reader)
            {
                if (reader == null)
                {
                    throw new ArgumentNullException("reader");
                }
                var result = new OrderItemEntity
                {
                    OrderId = orderIdColumn.GetInt64(reader),
                    DishId = dishIdColumn.GetInt64(reader),
                    Quantity = quantityColumn.GetInt32(reader),
                    ItemStatus = (ItemStatus)itemStatusColumn.GetInt32(reader)
                };

                return result;
            }
        }

        public static SqlParameter BindOrderItemTable(string parameterName, IEnumerable<OrderItemEntity> orderItemsToUpdate)
        {
            orderItemsToUpdate = orderItemsToUpdate ?? Enumerable.Empty<OrderItemEntity>();

            return TableBinder.BindTable(parameterName, "typ_OrderItems_v1", BindOrderItemRows(orderItemsToUpdate));
        }

        private static IEnumerable<SqlDataRecord> BindOrderItemRows(IEnumerable<OrderItemEntity> orderItemsToUpdate)
        {
            foreach (var orderItem in orderItemsToUpdate)
            {
                SqlDataRecord record = new SqlDataRecord(typ_OrderItemTable);

                record.SetInt64(0, orderItem.OrderId);
                record.SetInt64(1, orderItem.DishId);
                record.SetInt32(2, orderItem.Quantity);
                record.SetInt32(3, (int)orderItem.ItemStatus);

                yield return record;
            }
        }

        private static readonly SqlMetaData[] typ_OrderItemTable = new SqlMetaData[]
        {
            new SqlMetaData("OrderId", SqlDbType.BigInt),
            new SqlMetaData("DishId", SqlDbType.BigInt),
            new SqlMetaData("Quantity", SqlDbType.Int),
            new SqlMetaData("ItemStatus", SqlDbType.Int),
        };
    }
}