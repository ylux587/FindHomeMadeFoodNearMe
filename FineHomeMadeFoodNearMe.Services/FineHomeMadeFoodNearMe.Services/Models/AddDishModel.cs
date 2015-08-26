namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System;
    using Enums;
    using System.Runtime.Serialization;
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;

    [DataContract]
    public sealed class AddDishModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DishModel DishToAdd { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long ProviderId { get; set; }
    }
}