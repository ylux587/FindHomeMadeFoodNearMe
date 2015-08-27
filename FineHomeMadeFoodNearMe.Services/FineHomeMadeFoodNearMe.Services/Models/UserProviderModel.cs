namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System;
    using Enums;
    using System.Runtime.Serialization;
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;
    using System.Globalization;

    [DataContract]
    public sealed class UserProviderModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public UserModel UserInfo { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ProviderModel ProviderInfo { get; set; }
    }
}