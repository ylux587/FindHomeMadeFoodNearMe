namespace FindHomeMadeFoodNearMe.Services.Models
{
    using Enums;
    using System.Runtime.Serialization;
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;

    [DataContract]
    public sealed class OrderItemModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public long OrderId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public long DishId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public int Quantity { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ItemStatus ItemStatus { get; set; }

        public OrderItemEntity ToEntity()
        {
            return new OrderItemEntity
            {
                OrderId = this.OrderId,
                DishId = this.DishId,
                Quantity = this.Quantity,
                ItemStatus = this.ItemStatus,
            };
        }

        public static OrderItemModel CreateFromEntity(OrderItemEntity o)
        {
            if (o == null)
            {
                return null;
            }
            return new OrderItemModel
            {
                OrderId = o.OrderId,
                DishId = o.DishId,
                Quantity = o.Quantity,
                ItemStatus = o.ItemStatus,
            };
        }
    }
}