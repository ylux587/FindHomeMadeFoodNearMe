namespace FineHomeMadeFoodNearMe.Services.Models
{
    using Enums;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class Order
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long OrderId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long UserId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime OrderDate { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public decimal SubTotal { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public decimal? Tax { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public decimal? OtherCharges { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Notes { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public OrderStatus Status { get; set; }
    }
}