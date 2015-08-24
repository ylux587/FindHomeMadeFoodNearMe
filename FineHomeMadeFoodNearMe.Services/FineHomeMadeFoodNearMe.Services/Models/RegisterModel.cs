namespace FineHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class RegisterModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public UserModel UserInfo { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public ProviderModel ProviderInfo { get; set; }
    }
}