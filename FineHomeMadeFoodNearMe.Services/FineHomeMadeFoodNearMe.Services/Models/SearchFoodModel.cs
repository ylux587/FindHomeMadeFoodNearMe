namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class SearchFoodModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double Latitude { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public double Longitude { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public int Range { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public bool ConvertToMile { get; set; }
    }
}