namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System;
    using Enums;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract]
    public sealed class PlaceOrderModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long UserId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public List<OrderItemModel> OrderItems { get; set; }
    }
}