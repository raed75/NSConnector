using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NSConnector.Abstracts
{

    
    public abstract class NSEntity
    {

        private string _recordType;
        
        protected NSEntity(string recordType)
        {
            _recordType = recordType;
        }
        
        [JsonProperty(PropertyName = "internalid")]
        public int InternalID { get; set; }

        [JsonProperty(PropertyName = "entityid")]
        public string EntityID { get; set; }

        [JsonProperty(PropertyName = "recordtype")]
        public string RecordType { get {return _recordType;}}
    }
}
