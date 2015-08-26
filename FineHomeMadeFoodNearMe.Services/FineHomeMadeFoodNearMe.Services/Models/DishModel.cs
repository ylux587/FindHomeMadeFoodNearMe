namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System;
    using Enums;
    using System.Runtime.Serialization;
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;

    [DataContract]
    public sealed class DishModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long DishId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long ProviderId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DishType DishType { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string DishName { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Ingredients { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public decimal Price { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public int WaitingTimeInMins { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public Guid? ThumbNailPictureKey { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public bool Available { get; set; }

        public DishEntity ToEntity()
        {
            return new DishEntity
            {
                DishId = this.DishId,
                DishName = this.DishName,
                Description = this.Description,
                Ingredients = this.Ingredients,
                Price = this.Price,
                ThumbNailPictureKey = this.ThumbNailPictureKey,
                ProviderId = this.ProviderId,
                DishType = this.DishType,
                WaitingTimeInMins = this.WaitingTimeInMins,
                Available = true
            };
        }

        public static DishModel CreateFromEntity(DishEntity d)
        {
            if (d == null)
            {
                return null;
            }
            return new DishModel
            {
                DishId = d.DishId,
                DishName = d.DishName,
                Description = d.Description,
                Ingredients = d.Ingredients,
                Price = d.Price,
                ThumbNailPictureKey = d.ThumbNailPictureKey,
                ProviderId = d.ProviderId,
                DishType = d.DishType,
                WaitingTimeInMins = d.WaitingTimeInMins,
                Available = d.Available
            };
        }
    }
}