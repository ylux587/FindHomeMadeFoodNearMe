namespace FineHomeMadeFoodNearMe.Services.Models
{
    using Enums;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class User
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long UserId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string LastName { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string PhoneNumber { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string AddressLine1 { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string AddressLine2 { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string AddressLine3 { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string City { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string StateOrProvince { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Country { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public UserStatus Status { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double GeoLatitude { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double GeoLongitude { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string ZipCode { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Password { get; set; }
    }
}