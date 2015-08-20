﻿namespace FineHomeMadeFoodNearMe.Services.DataAccess.Entities
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

    public sealed class DishEntity
    {
        public long DishId { get; set; }

        public long ProviderId { get; set; }

        public DishType DishType { get; set; }

        public string DishName { get; set; }

        public string Description { get; set; }

        public string Ingredients { get; set; }

        public decimal Price { get; set; }

        public int WaitingTimeInMins { get; set; }

        public Guid? ThumbNailPictureKey { get; set; }

        public bool Available { get; set; }

        public class DishEntityColumns : IEntityBinder<DishEntity>
        {
            private SqlColumnBinder dishIdColumn = new SqlColumnBinder("DishId");
            private SqlColumnBinder privoderIdColumn = new SqlColumnBinder("ProviderId");
            private SqlColumnBinder dishTypeColumn = new SqlColumnBinder("DishType");
            private SqlColumnBinder dishNameColumn = new SqlColumnBinder("DishName");
            private SqlColumnBinder descriptionColumn = new SqlColumnBinder("Description");
            private SqlColumnBinder ingredientsColumn = new SqlColumnBinder("Ingredients");
            private SqlColumnBinder priceColumn = new SqlColumnBinder("Price");
            private SqlColumnBinder waitingTimeInMinsColumn = new SqlColumnBinder("WaitingTimeInMins");
            private SqlColumnBinder thumbNailPictureKeyColumn = new SqlColumnBinder("ThumbNailPictureKey");
            private SqlColumnBinder availableColumn = new SqlColumnBinder("Available");

            public DishEntity BindEntity(SqlDataReader reader)
            {
                if (reader == null)
                {
                    throw new ArgumentNullException("reader");
                }
                var result = new DishEntity
                {
                    DishId = dishIdColumn.GetInt64(reader),
                    ProviderId = privoderIdColumn.GetInt64(reader),
                    DishType = (DishType)dishTypeColumn.GetInt32(reader),
                    DishName = dishNameColumn.GetString(reader, false),
                    Description = descriptionColumn.GetString(reader, true),
                    Ingredients = ingredientsColumn.GetString(reader, true),
                    Price = priceColumn.GetDecimal(reader),
                    WaitingTimeInMins = waitingTimeInMinsColumn.GetInt32(reader),
                    ThumbNailPictureKey = thumbNailPictureKeyColumn.IsNull(reader) ? null : (Guid?) thumbNailPictureKeyColumn.GetGuid(reader),
                    Available = availableColumn.GetBoolean(reader)
                };

                return result;
            }
        }

        public static SqlParameter BindDishTable(string parameterName, IEnumerable<DishEntity> dishesToUpdate)
        {
            dishesToUpdate = dishesToUpdate ?? Enumerable.Empty<DishEntity>();

            return TableBinder.BindTable(parameterName, "typ_Dishs_v1", BindDishRows(dishesToUpdate));
        }

        private static IEnumerable<SqlDataRecord> BindDishRows(IEnumerable<DishEntity> dishesToUpdate)
        {
            foreach (var dish in dishesToUpdate)
            {
                SqlDataRecord record = new SqlDataRecord(typ_DishTable);

                record.SetInt64(0, dish.DishId);
                record.SetInt64(1, dish.ProviderId);
                record.SetInt32(2, (int) dish.DishType);
                record.SetString(3, dish.DishName);
                record.SetString(4, dish.Description);
                record.SetString(5, dish.Ingredients);
                record.SetDecimal(6, dish.Price);
                record.SetInt32(7, dish.WaitingTimeInMins);
                if (dish.ThumbNailPictureKey.HasValue)
                {
                    record.SetDBNull(8);
                }
                else
                {
                    record.SetGuid(8, dish.ThumbNailPictureKey.Value);
                }
                record.SetBoolean(9, dish.Available);

                yield return record;
            }
        }

        private static readonly SqlMetaData[] typ_DishTable = new SqlMetaData[]
        {
            new SqlMetaData("DishId", SqlDbType.BigInt),
            new SqlMetaData("ProviderId", SqlDbType.BigInt),
            new SqlMetaData("DishType", SqlDbType.DateTime2),
            new SqlMetaData("DishName", SqlDbType.Decimal),
            new SqlMetaData("Description", SqlDbType.Decimal),
            new SqlMetaData("Ingredients", SqlDbType.Decimal),
            new SqlMetaData("Price", SqlDbType.NVarChar, 1000),
            new SqlMetaData("WaitingTimeInMins", SqlDbType.Int),
            new SqlMetaData("ThumbNailPictureKey", SqlDbType.Int),
            new SqlMetaData("Available", SqlDbType.Int),
        };
    }
}