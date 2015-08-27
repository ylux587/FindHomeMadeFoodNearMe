namespace FindHomeMadeFoodNearMe.Services.Models
{
    using Enums;
    using System.Runtime.Serialization;
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;

    [DataContract]
    public sealed class UserModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long UserId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string LastName { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string PhoneNumber { get; set; }
       
        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public UserStatus UserStatus { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Password { get; set; }

        public UserEntity ToEntity()
        {
            return new UserEntity
            {
                UserId = this.UserId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                UserStatus = this.UserStatus,
                Password = this.Password,
            };
        }

        public static UserModel CreateFromEntity(UserEntity u)
        {
            return new UserModel
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UserStatus = u.UserStatus,
                Password = u.Password,
            };
        }
    }
}