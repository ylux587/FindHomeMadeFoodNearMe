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

        public string Password { get; set; }

        public UserStatus UserStatus { get; set; }

        public class UserEntityColumns : IEntityBinder<UserEntity>
        {
            private SqlColumnBinder userIdColumn = new SqlColumnBinder("UserId");
            private SqlColumnBinder firstNameColumn = new SqlColumnBinder("FirstName");
            private SqlColumnBinder lastNameColumn = new SqlColumnBinder("LastName");
            private SqlColumnBinder emailColumn = new SqlColumnBinder("Email");
            private SqlColumnBinder phoneNumberColumn = new SqlColumnBinder("PhoneNumber");
            private SqlColumnBinder passwordColumn = new SqlColumnBinder("Password");
            private SqlColumnBinder userStatusColumn = new SqlColumnBinder("UserStatus");

            private static IEntityBinder<UserEntity> instance;

            private UserEntityColumns()
            {
            }

            public static IEntityBinder<UserEntity> Instance
            {
                get { return instance ?? (instance = new UserEntityColumns()); }
            }

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
                    LastName = lastNameColumn.GetString(reader, false),
                    Email = emailColumn.GetString(reader, false),
                    PhoneNumber = phoneNumberColumn.GetString(reader, false),
                    Password = passwordColumn.GetString(reader, false),
                    UserStatus = (UserStatus)userStatusColumn.GetInt32(reader),
                };

                return result;
            }
        }

        public static SqlParameter BindUserTable(string parameterName, IEnumerable<UserEntity> usersToUpdate)
        {
            usersToUpdate = usersToUpdate ?? Enumerable.Empty<UserEntity>();

            return TableBinder.BindTable(parameterName, "typ_Users_v1", BindUserRows(usersToUpdate));
        }

        private static IEnumerable<SqlDataRecord> BindUserRows(IEnumerable<UserEntity> usersToUpdate)
        {
            foreach (var user in usersToUpdate)
            {
                SqlDataRecord record = new SqlDataRecord(typ_UserTable);

                record.SetString(0, user.Email);
                record.SetString(1, user.Password);
                record.SetString(2, user.FirstName);
                record.SetString(3, user.LastName);
                record.SetString(4, user.PhoneNumber);
                record.SetInt32(5, (int) user.UserStatus);

                yield return record;
            }
        }

        private static readonly SqlMetaData[] typ_UserTable = new SqlMetaData[]
        {
            new SqlMetaData("Email", SqlDbType.NVarChar, 200),
            new SqlMetaData("Password", SqlDbType.NVarChar, 20),
            new SqlMetaData("FirstName", SqlDbType.NVarChar, 200),
            new SqlMetaData("LastName", SqlDbType.NVarChar, 200),
            new SqlMetaData("PhoneNumber", SqlDbType.NVarChar, 200),
            new SqlMetaData("UserStatus", SqlDbType.Int),
        };
    }
}