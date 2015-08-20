namespace FineHomeMadeFoodNearMe.Services.Models
{
    using Enums;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class OrderItemModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long OrderId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long DishId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ItemStatus ItemStatus { get; set; }
    }
}