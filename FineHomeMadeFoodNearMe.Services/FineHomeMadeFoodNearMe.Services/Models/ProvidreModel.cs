namespace FindHomeMadeFoodNearMe.Services.Models
{
    using Enums;
    using System.Runtime.Serialization;
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;

    [DataContract]
    public sealed class ProviderModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long ProviderId { get; set; }

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
        public ProviderStatus ProviderStatus { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double? GeoLatitude { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double? GeoLongitude { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string ZipCode { get; set; }

        public ProviderEntity ToEntity()
        {
            return new ProviderEntity
            {
                ProviderId = this.ProviderId,
                AddressLine1 = this.AddressLine1,
                AddressLine2 = this.AddressLine2,
                AddressLine3 = this.AddressLine3,
                City = this.City,
                StateOrProvince = this.StateOrProvince,
                Country = this.Country,
                ZipCode = this.ZipCode,
                ProviderStatus = this.ProviderStatus,
                GeoLatitude = this.GeoLatitude,
                GeoLongitude = this.GeoLongitude
            };
        }

        public static ProviderModel CreateFromEntity(ProviderEntity p)
        {
            return new ProviderModel
            {
                ProviderId = p.ProviderId,
                AddressLine1 = p.AddressLine1,
                AddressLine2 = p.AddressLine2,
                AddressLine3 = p.AddressLine3,
                City = p.City,
                StateOrProvince = p.StateOrProvince,
                Country = p.Country,
                ZipCode = p.ZipCode,
                ProviderStatus = p.ProviderStatus,
                GeoLatitude = p.GeoLatitude,
                GeoLongitude = p.GeoLongitude
            };
        }
    }
}