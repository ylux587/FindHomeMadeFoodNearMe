namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class AddressSearchFoodResultModel : SearchFoodResultModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public double AddressLatitude { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public double AddressLongitude { get; set; }
    }
}