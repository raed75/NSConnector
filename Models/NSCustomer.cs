using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NSConnector.Models 
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class NSCustomer : NSRootEntity
    {
        public NSCustomer() : base("customer") {}
        
        [JsonProperty(PropertyName = "email")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [JsonIgnore()]
        public string FullName { get { return String.Format("{0} {1}", FirstName, LastName); } }

        [JsonProperty(PropertyName = "defaultaddress")]
        public string FullAddress { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "addressee")]
        public string Addressee { get; set; }

        [JsonProperty(PropertyName = "address1")]
        public string Address1 { get; set; }

        [JsonProperty(PropertyName = "address2")]
        public string Address2 { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "zipcode")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "mobilephone")]
        public string MobilePhone { get; set; }

        [JsonProperty(PropertyName = "homephone")]
        public string HomePhone { get; set; }

        [JsonProperty(PropertyName = "custentity_fac_fl")]
        public bool IsFacilitator { get; set; }

        [JsonProperty(PropertyName = "subsidiary")]
        public object Subsidiary { get; set; }

        [JsonProperty(PropertyName = "companyname")]
        public string CompanyName { get; set; }

    }

}
