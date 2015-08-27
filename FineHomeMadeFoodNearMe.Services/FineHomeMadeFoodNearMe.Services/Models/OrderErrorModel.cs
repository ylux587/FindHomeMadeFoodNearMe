namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract]
    public sealed class OrderErrorModel : ErrorModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public OrderModel Order { get; set; }
    }
}