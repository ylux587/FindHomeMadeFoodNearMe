namespace FineHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract]
    public class UserErrorModel : ErrorModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long UserId { get; set; }
    }
}