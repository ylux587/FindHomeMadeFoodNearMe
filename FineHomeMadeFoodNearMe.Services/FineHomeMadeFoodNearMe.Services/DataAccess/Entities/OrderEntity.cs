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

    public sealed class OrderEntity
    {
        public long OrderId { get; set; }

        public long UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal? Tax { get; set; }

        public decimal? OtherCharges { get; set; }

        public string Notes { get; set; }

        public OrderStatus Status { get; set; }

        public class OrderEntityColumns : IEntityBinder<OrderEntity>
        {
            private SqlColumnBinder orderIdColumn = new SqlColumnBinder("OrderId");
            private SqlColumnBinder userIdColumn = new SqlColumnBinder("UserId");
            private SqlColumnBinder orderDateColumn = new SqlColumnBinder("OrderDate");
            private SqlColumnBinder subTotalColumn = new SqlColumnBinder("SubTotal");
            private SqlColumnBinder taxColumn = new SqlColumnBinder("Tax");
            private SqlColumnBinder otherChargesColumn = new SqlColumnBinder("OtherCharges");
            private SqlColumnBinder notesColumn = new SqlColumnBinder("Notes");
            private SqlColumnBinder statusColumn = new SqlColumnBinder("Status");

            private static IEntityBinder<OrderEntity> instance;

            private OrderEntityColumns()
            {
            }

            public static IEntityBinder<OrderEntity> Instance
            {
                get { return instance ?? (instance = new OrderEntityColumns()); }
            }

            public OrderEntity BindEntity(SqlDataReader reader)
            {
                if (reader == null)
                {
                    throw new ArgumentNullException("reader");
                }
                var result = new OrderEntity
                    {
                        OrderId = orderIdColumn.GetInt64(reader),
                        UserId = userIdColumn.GetInt64(reader),
                        OrderDate = orderDateColumn.GetDateTime(reader),
                        SubTotal = subTotalColumn.GetDecimal(reader),
                        Tax = taxColumn.IsNull(reader) ? null : (decimal?) taxColumn.GetDecimal(reader),
                        OtherCharges = otherChargesColumn.IsNull(reader) ? null : (decimal?) otherChargesColumn.GetDecimal(reader, 0m),
                        Notes = notesColumn.GetString(reader, true),
                        Status = (OrderStatus) statusColumn.GetInt32(reader)
                    };

                return result;
            }
        }

        public static SqlParameter BindOrderTable(string parameterName, IEnumerable<OrderEntity> ordersToUpdate)
        {
            ordersToUpdate = ordersToUpdate ?? Enumerable.Empty<OrderEntity>();

            return TableBinder.BindTable(parameterName, "typ_Orders_v1", BindOrderRows(ordersToUpdate));
        }

        private static IEnumerable<SqlDataRecord> BindOrderRows(IEnumerable<OrderEntity> ordersToUpdate)
        {
            foreach (var order in ordersToUpdate)
            {
                SqlDataRecord record = new SqlDataRecord(typ_OrderTable);

                record.SetInt64(0, order.OrderId);
                record.SetInt64(1, order.UserId);
                record.SetDateTime(2, order.OrderDate);
                record.SetDecimal(3, order.SubTotal);
                if (order.Tax.HasValue)
                {
                    record.SetDecimal(4, order.Tax.Value);
                }
                else
                {
                    record.SetDBNull(4);
                }
                if (order.OtherCharges.HasValue)
                {
                    record.SetDecimal(5, order.OtherCharges.Value);
                }
                else
                {
                    record.SetDBNull(5);
                }
                if (!string.IsNullOrWhiteSpace(order.Notes))
                {
                    record.SetString(6, order.Notes);
                }
                else
                {
                    record.SetDBNull(6);
                }
                record.SetInt32(7, (int)order.Status);

                yield return record;
            }
        }

        private static readonly SqlMetaData[] typ_OrderTable = new SqlMetaData[]
        {
            new SqlMetaData("OrderId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("OrderDate", SqlDbType.DateTime2),
            new SqlMetaData("SubTotal", SqlDbType.Decimal),
            new SqlMetaData("Tax", SqlDbType.Decimal),
            new SqlMetaData("OtherCharges", SqlDbType.Decimal),
            new SqlMetaData("Notes", SqlDbType.NVarChar, 1000),
            new SqlMetaData("Status", SqlDbType.Int),
        };
    }
}