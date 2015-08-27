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
        public AddressModel Address { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ProviderStatus ProviderStatus { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double? GeoLatitude { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double? GeoLongitude { get; set; }

        public ProviderEntity ToEntity()
        {
            return new ProviderEntity
            {
                ProviderId = this.ProviderId,
                AddressLine1 = this.Address.AddressLine1,
                AddressLine2 = this.Address.AddressLine2,
                AddressLine3 = this.Address.AddressLine3,
                City = this.Address.City,
                StateOrProvince = this.Address.StateOrProvince,
                Country = this.Address.Country,
                ZipCode = this.Address.ZipCode,
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
                Address = new AddressModel
                    {
                        AddressLine1 = p.AddressLine1,
                            AddressLine2 = p.AddressLine2,
                            AddressLine3 = p.AddressLine3,
                            City = p.City,
                            StateOrProvince = p.StateOrProvince,
                            Country = p.Country,
                            ZipCode = p.ZipCode,
                    },
                ProviderStatus = p.ProviderStatus,
                GeoLatitude = p.GeoLatitude,
                GeoLongitude = p.GeoLongitude
            };
        }
    }
}