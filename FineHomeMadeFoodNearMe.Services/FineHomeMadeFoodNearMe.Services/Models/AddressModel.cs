namespace FindHomeMadeFoodNearMe.Services.Models
{
    using System;
    using Enums;
    using System.Runtime.Serialization;
    using FindHomeMadeFoodNearMe.Services.DataAccess.Entities;
    using System.Globalization;

    [DataContract]
    public sealed class AddressModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string AddressLine1 { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string AddressLine2 { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string AddressLine3 { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string City { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string StateOrProvince { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Country { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string ZipCode { get; set; }

        public string FullAddressLine
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "{0}{1}{2}",
                    AddressLine1,
                    string.IsNullOrWhiteSpace(AddressLine2) ? string.Empty : " " + AddressLine2.Trim(),
                    string.IsNullOrWhiteSpace(AddressLine3) ? string.Empty : " " + AddressLine3.Trim());
            }
        }
    }
}