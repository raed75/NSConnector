using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.IO;

namespace NSConnector
{
    public class NSCaller
    {


        private const string CONTENT_TYPE = "application/json";
        private const string ACCEPT = "*/*";

        private string _serverUrl;
        private string _accountId;
        private string _username;
        private string _password;
        private string _scriptId;
        private string _deployId;
        private HttpWebRequest _nsHttpRequest;

        public NSCaller(string serverUrl, string accountId, string username, string password, string scriptId, string deployId = "1")
        {
            _serverUrl = serverUrl;
            _accountId = accountId;
            _username = username;
            _password = password;
            _scriptId = scriptId;
            _deployId = deployId;
        }

        public async Task<string> Get(NameValueCollection queryItems = null)
        {

            string jsonResult = "";
            
            try
            {

                // initialize the http request
                InitNSRequest("GET", queryItems);
                
                // Get Response from Net Suite
                jsonResult = await GetNSResponse();

            }

            catch { throw; }

            return jsonResult;

        }

        public async Task<string> Post(string jsonData, NameValueCollection queryItems = null)
        {

            string jsonResult = "";
                        
            try
            {
                byte[] postContent = Encoding.UTF8.GetBytes(jsonData);
                int postContentLength = postContent.Length;


                // initialize the http request
                InitNSRequest("POST", queryItems, postContentLength);
                
                // Write the POST reqeust to HTTP Web stream
                using (var requestStream = await _nsHttpRequest.GetRequestStreamAsync())
                {

                    requestStream.Write(postContent, 0, postContentLength);
                }

                // Get Response from Net Suite
                jsonResult = await GetNSResponse();

            }

            catch { throw; }
            
            return jsonResult;
        }

        // Init HttpWebRequest for NetSuite
        private void InitNSRequest(string methodType, NameValueCollection queryItems = null, long contentLength = 0)
        {
            UriBuilder url = new UriBuilder(_serverUrl);

            var query = HttpUtility.ParseQueryString(url.Query);
            query["script"] = _scriptId;
            query["deploy"] = _deployId;
            // check the request has additional query string parameters
            if (queryItems != null)
                query.Add(queryItems);

            url.Query = query.ToString();

            _nsHttpRequest = (HttpWebRequest)WebRequest.Create(url.Uri);

            _nsHttpRequest.ContentType = CONTENT_TYPE;
            _nsHttpRequest.Accept = ACCEPT;
            _nsHttpRequest.Method = methodType;
            _nsHttpRequest.Headers.Add(HttpRequestHeader.Authorization,
                String.Format("NLAuth nlauth_account={0},nlauth_email={1},nlauth_signature={2}", _accountId, _username, _password));
            if(contentLength != 0)
                _nsHttpRequest.ContentLength = contentLength;
        }

        // Get JSON response from NetSuite
        private async Task<string> GetNSResponse()
        {
            string jsonResult = "";
            HttpWebResponse nsHttpResponse;
            
            try
            {
                nsHttpResponse = (HttpWebResponse)await _nsHttpRequest.GetResponseAsync();
            }

            catch (System.Net.WebException ex)
            {
                nsHttpResponse = (HttpWebResponse)ex.Response;
            }

            if (nsHttpResponse != null)
            {
                using (var stream = new StreamReader(nsHttpResponse.GetResponseStream()))
                {
                    jsonResult = stream.ReadToEnd();
                }
            }

            return jsonResult;
        }
    }
}
