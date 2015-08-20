namespace FineHomeMadeFoodNearMe.Services.Models
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

    [DataContract]
    public sealed class OrderModel
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