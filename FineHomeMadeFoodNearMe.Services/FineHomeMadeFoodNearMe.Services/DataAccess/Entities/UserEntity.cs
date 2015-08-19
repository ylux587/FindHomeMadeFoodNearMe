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
    }
}