namespace FindHomeMadeFoodNearMe.Services.Models
{
    using Enums;
    using DataAccess;
    using System;
    using System.Runtime.Serialization;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.SqlServer.Server;
    using System.Data;
    using System.Data.SqlClient;
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;

    [DataContract]
    public sealed class OrderModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public long OrderId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
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

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public OrderStatus Status { get; set; }

        public OrderEntity ToEntity()
        {
            return new OrderEntity
            {
                OrderId = this.OrderId,
                UserId = this.UserId,
                OrderDate = this.OrderDate,
                SubTotal = this.SubTotal,
                Tax = this.Tax,
                OtherCharges = this.OtherCharges,
                Notes = this.Notes,
                Status = this.Status,
            };
        }

        public static OrderModel CreateFromEntity(OrderEntity o)
        {
            if (o == null)
            {
                return null;
            }
            return new OrderModel
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                SubTotal = o.SubTotal,
                Tax = o.Tax,
                OtherCharges = o.OtherCharges,
                Notes = o.Notes,
                Status = o.Status
            };
        }
    }
}