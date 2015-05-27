using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NSConnector.Models
{
    public abstract class NSRootEntity
    {

        private string _recordType;
        
        protected NSRootEntity(string recordType)
        {
            _recordType = recordType;
        }
        
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "entityid")]
        public string EntityID { get; set; }

        [JsonProperty(PropertyName = "recordtype")]
        public string RecordType { get {return _recordType;}}
    }
}
