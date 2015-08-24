namespace FineHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    using FineHomeMadeFoodNearMe.Services.Models.Enums;

    [DataContract]
    public sealed class UpdateOrderItemModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long OrderId { get; set;}

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long DishId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long ProviderId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ItemStatus TargetStatus { get; set; }
    }
}