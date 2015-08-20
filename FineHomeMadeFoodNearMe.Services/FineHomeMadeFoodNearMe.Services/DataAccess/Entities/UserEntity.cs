namespace FineHomeMadeFoodNearMe.Services.DataAccess.Entities
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

    public sealed class UserEntity
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string Country { get; set; }

        public UserStatus Status { get; set; }

        public double GeoLatitude { get; set; }

        public double GeoLongitude { get; set; }

        public string ZipCode { get; set; }

        public string Password { get; set; }

        public class OrderItemEntityColumns : IEntityBinder<OrderItemEntity>
        {
            private SqlColumnBinder userIdColumn = new SqlColumnBinder("UserId");
            private SqlColumnBinder firstNameColumn = new SqlColumnBinder("FirstName");
            private SqlColumnBinder lastNameColumn = new SqlColumnBinder("LastName");
            private SqlColumnBinder emailColumn = new SqlColumnBinder("Email");
            private SqlColumnBinder phoneNumberColumn = new SqlColumnBinder("PhoneNumber");
            private SqlColumnBinder addressLine1Column = new SqlColumnBinder("AddressLine1");
            private SqlColumnBinder addressLine2Column = new SqlColumnBinder("AddressLine2");
            private SqlColumnBinder addressLine3Column = new SqlColumnBinder("AddressLine3");
            private SqlColumnBinder cityColumn = new SqlColumnBinder("City");
            private SqlColumnBinder stateOrProvinceColumn = new SqlColumnBinder("StateOrProvince");
            private SqlColumnBinder countryColumn = new SqlColumnBinder("Country");
            private SqlColumnBinder statusColumn = new SqlColumnBinder("Status");
            private SqlColumnBinder geoLatitudeColumn = new SqlColumnBinder("GeoLatitude");
            private SqlColumnBinder geoLongitudeColumn = new SqlColumnBinder("GeoLongitude");
            private SqlColumnBinder zipCodeColumn = new SqlColumnBinder("ZipCode");
            private SqlColumnBinder passwordColumn = new SqlColumnBinder("Password");

            public UserEntity BindEntity(SqlDataReader reader)
            {
                if (reader == null)
                {
                    throw new ArgumentNullException("reader");
                }
                var result = new UserEntity
                {
                    UserId = userIdColumn.GetInt64(reader),
                    FirstName = firstNameColumn.GetString(reader, false),
                    LastName = firstNameColumn.GetString(reader, false),
                    Email = emailColumn.GetString(reader, false),
                    PhoneNumber = phoneNumberColumn.GetString(reader, false),
                    AddressLine1 = addressLine1Column.GetString(reader, false),
                    AddressLine2 = addressLine2Column.GetString(reader, true),
                    AddressLine3 = addressLine3Column.GetString(reader, true),
                    City = cityColumn.GetString(reader, false),
                    StateOrProvince = stateOrProvinceColumn.GetString(reader, true),
                    Country = countryColumn.GetString(reader, false),
                    Status = (UserStatus)statusColumn.GetInt32(reader),
                    GeoLatitude = geoLatitudeColumn.GetDouble(reader),
                    GeoLongitude = geoLongitudeColumn.GetDouble(reader),
                    ZipCode = zipCodeColumn.GetString(reader, false),
                    Password = passwordColumn.GetString(reader, false)
                };

                return result;
            }
        }

        public static SqlParameter BindOrderTable(string parameterName, IEnumerable<UserEntity> usersToUpdate)
        {
            usersToUpdate = usersToUpdate ?? Enumerable.Empty<UserEntity>();

            return TableBinder.BindTable(parameterName, "typ_Users_v1", BindUserRows(usersToUpdate));
        }

        private static IEnumerable<SqlDataRecord> BindUserRows(IEnumerable<UserEntity> usersToUpdate)
        {
            foreach (var user in usersToUpdate)
            {
                SqlDataRecord record = new SqlDataRecord(typ_UserTable);

                record.SetInt64(0, user.UserId);
                record.SetString(1, user.FirstName);
                record.SetString(2, user.LastName);
                record.SetString(3, user.Email);
                record.SetString(4, user.PhoneNumber);
                record.SetString(5, user.AddressLine1);
                record.SetString(6, user.AddressLine2);
                record.SetString(7, user.AddressLine3);
                record.SetString(8, user.City);
                record.SetString(9, user.StateOrProvince);
                record.SetString(10, user.Country);
                record.SetInt32(11, (int) user.Status);
                record.SetDouble(12, user.GeoLatitude);
                record.SetDouble(13, user.GeoLongitude);
                record.SetString(14, user.ZipCode);
                record.SetString(15, user.Password);

                yield return record;
            }
        }

        private static readonly SqlMetaData[] typ_UserTable = new SqlMetaData[]
        {
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),
            new SqlMetaData("UserId", SqlDbType.BigInt),


        };
    }
}