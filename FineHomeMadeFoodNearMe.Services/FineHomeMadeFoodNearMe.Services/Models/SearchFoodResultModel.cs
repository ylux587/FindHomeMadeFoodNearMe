namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract]
    public class SearchFoodResultModel : ErrorModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public List<SearchHitProviderModel> ProviderInfos { get; set; }

    }
}