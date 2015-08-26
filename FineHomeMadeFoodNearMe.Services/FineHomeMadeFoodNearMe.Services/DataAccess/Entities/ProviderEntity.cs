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

    public sealed class ProviderEntity
    {
        public long ProviderId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string Country { get; set; }

        public ProviderStatus ProviderStatus { get; set; }

        public double? GeoLatitude { get; set; }

        public double? GeoLongitude { get; set; }

        public string ZipCode { get; set; }

        public class ProviderEntityColumns : IEntityBinder<ProviderEntity>
        {
            private SqlColumnBinder providerIdColumn = new SqlColumnBinder("ProviderId");
            private SqlColumnBinder addressLine1Column = new SqlColumnBinder("AddressLine1");
            private SqlColumnBinder addressLine2Column = new SqlColumnBinder("AddressLine2");
            private SqlColumnBinder addressLine3Column = new SqlColumnBinder("AddressLine3");
            private SqlColumnBinder cityColumn = new SqlColumnBinder("City");
            private SqlColumnBinder stateOrProvinceColumn = new SqlColumnBinder("StateOrProvince");
            private SqlColumnBinder countryColumn = new SqlColumnBinder("Country");
            private SqlColumnBinder providerStatusColumn = new SqlColumnBinder("ProviderStatus");
            private SqlColumnBinder geoLatitudeColumn = new SqlColumnBinder("GeoLatitude");
            private SqlColumnBinder geoLongitudeColumn = new SqlColumnBinder("GeoLongitude");
            private SqlColumnBinder zipCodeColumn = new SqlColumnBinder("ZipCode");

            private static IEntityBinder<ProviderEntity> instance;

            private ProviderEntityColumns()
            {
            }

            public static IEntityBinder<ProviderEntity> Instance
            {
                get { return instance ?? (instance = new ProviderEntityColumns()); }
            }

            public ProviderEntity BindEntity(SqlDataReader reader)
            {
                if (reader == null)
                {
                    throw new ArgumentNullException("reader");
                }
                var result = new ProviderEntity
                {
                    ProviderId = providerIdColumn.GetInt64(reader),
                    AddressLine1 = addressLine1Column.GetString(reader, false),
                    AddressLine2 = addressLine2Column.GetString(reader, true),
                    AddressLine3 = addressLine3Column.GetString(reader, true),
                    City = cityColumn.GetString(reader, false),
                    StateOrProvince = stateOrProvinceColumn.GetString(reader, true),
                    Country = countryColumn.GetString(reader, false),
                    ProviderStatus = (ProviderStatus)providerStatusColumn.GetInt32(reader),
                    GeoLatitude = geoLatitudeColumn.IsNull(reader) ? null : (double?) geoLatitudeColumn.GetDouble(reader),
                    GeoLongitude = geoLongitudeColumn.IsNull(reader) ? null : (double?) geoLongitudeColumn.GetDouble(reader),
                    ZipCode = zipCodeColumn.GetString(reader, false)
                };

                return result;
            }
        }

        public static SqlParameter BindProviderTable(string parameterName, IEnumerable<ProviderEntity> providersToUpdate)
        {
            providersToUpdate = providersToUpdate ?? Enumerable.Empty<ProviderEntity>();

            return TableBinder.BindTable(parameterName, "typ_Providers_v1", BindProviderRows(providersToUpdate));
        }

        private static IEnumerable<SqlDataRecord> BindProviderRows(IEnumerable<ProviderEntity> providersToUpdate)
        {
            foreach (var provider in providersToUpdate)
            {
                SqlDataRecord record = new SqlDataRecord(typ_ProviderTable);

                record.SetInt64(0, provider.ProviderId);
                record.SetString(1, provider.AddressLine1);
                if (string.IsNullOrWhiteSpace(provider.AddressLine2))
                {
                    record.SetDBNull(2);
                }
                else
                {
                    record.SetString(2, provider.AddressLine2);
                }
                if (string.IsNullOrWhiteSpace(provider.AddressLine3))
                {
                    record.SetDBNull(3);
                }
                else
                {
                    record.SetString(3, provider.AddressLine3);
                }
                record.SetString(4, provider.City);
                if (string.IsNullOrWhiteSpace(provider.StateOrProvince))
                {
                    record.SetDBNull(5);
                }
                else
                {
                    record.SetString(5, provider.StateOrProvince);
                }
                record.SetString(6, provider.Country);
                record.SetInt32(7, (int)provider.ProviderStatus);
                if (provider.GeoLatitude.HasValue)
                {
                    record.SetDouble(8, provider.GeoLatitude.Value);
                }
                else
                {
                    record.SetDBNull(8);
                }
                if (provider.GeoLongitude.HasValue)
                {
                    record.SetDouble(9, provider.GeoLongitude.Value);
                }
                else
                {
                    record.SetDBNull(9);
                }
                record.SetString(10, provider.ZipCode);

                yield return record;
            }
        }

        private static readonly SqlMetaData[] typ_ProviderTable = new SqlMetaData[]
        {
            new SqlMetaData("ProviderId", SqlDbType.BigInt),
            new SqlMetaData("AddressLine1", SqlDbType.NVarChar, 200),
            new SqlMetaData("AddressLine2", SqlDbType.NVarChar, 200),
            new SqlMetaData("AddressLine3", SqlDbType.NVarChar, 200),
            new SqlMetaData("City", SqlDbType.NVarChar, 50),
            new SqlMetaData("StateOrProvince", SqlDbType.NVarChar, 50),
            new SqlMetaData("Country", SqlDbType.NVarChar, 10),
            new SqlMetaData("ProviderStatus", SqlDbType.Int),
            new SqlMetaData("GeoLatitude", SqlDbType.Float),
            new SqlMetaData("GeoLongitude", SqlDbType.Float),
            new SqlMetaData("ZipCode", SqlDbType.NVarChar, 10),
        };
    }
}