namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract]
    public class ErrorModel
    {
        public ErrorModel()
        {
            Messages = new List<string>();
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public List<string> Messages{get;set;}
    }
}