using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace NSConnector.Models
{
    public class NSRequest<T> where T : NSEntity
    {
        private NSAuth nsAuth;
        
        public NSRequest(string url, int accountId, string username, string password)
        {
            nsAuth = new NSAuth 
            {
                AccountID = accountId,
                Username = username,
                Password = password
            };

            Url = url;
        }

        public string Url { get; set; }
        public int ScriptID { get; set; }
        public int DeployID { get; set; }
        public NSAuth NSAuth { get { return nsAuth; } }
        public T NSEntity { get; set; }
        public NameValueCollection RequestParameters { get; set; }
    }
}
