using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSConnector.Abstracts;
using System.Data.Common;

namespace NSConnector.Models 
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class NSCustomer : NSEntity
    {
        public NSCustomer() : base("customer") {}
        
        [JsonProperty(PropertyName = "email")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "custentitybirth_data")]
        public string DateOfBirth { get; set; }

        [JsonIgnore]
        public string FullName { get { return String.Format("{0} {1}", FirstName, LastName); } }

        [JsonProperty(PropertyName = "mobilephone")]
        public string MobilePhone { get; set; }

        [JsonProperty(PropertyName = "homephone")]
        public string HomePhone { get; set; }

        [JsonProperty(PropertyName = "custentity_fac_fl")]
        public string IsFacilitator { get; set; }

        [JsonProperty(PropertyName = "isinactive")]
        public string IsInactive { get; set; }

        [JsonProperty(PropertyName = "subsidiary")]
        public int Subsidiary { get; set; }

        [JsonProperty(PropertyName = "addressbook")]
        public NSAddressBook AddressBook { get; set; }

    }

    [JsonObject("addressbook")]
    public class NSAddressBook
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("addr1")]
        public string Address1 { get; set; }
        [JsonProperty("addr2")]
        public string Address2 { get; set; }
        [JsonProperty("zip")]
        public string Zip { get; set; }
        [JsonProperty("addrphone")]
        public string AddressPhone { get; set; }
        [JsonProperty("isresidential")]
        public string IsResidential { get; set; }
    }

}
