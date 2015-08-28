namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract]
    public class UserErrorModel : ErrorModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public long UserId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public bool IsProvider { get; set; }


    }
}