namespace FineHomeMadeFoodNearMe.Services.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class LoginModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Username { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Password { get; set; }
    }
}