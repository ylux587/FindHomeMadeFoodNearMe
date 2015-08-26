namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System;
    using Enums;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class RemoveDishModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long ProviderId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long DishId { get; set; }
    }
}