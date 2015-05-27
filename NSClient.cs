using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using NSConnector.Models;
using Newtonsoft.Json.Linq;

namespace NSConnector
{
    public class NSClient
    {
        private const string CONTENT_TYPE = "application/json";
        private const string ACCEPT = "*/*";

       
        // GET Async
        public static async Task<NSResponse<T>> GetAsync<T>(NSRequest<T> request) where T : NSRootEntity
        {

            HttpWebRequest httpWReq = InitNSRequest(request, "GET");

            NSResponse<T> response = new NSResponse<T>();

            try
            {
                using (var webResponse = await httpWReq.GetResponseAsync())
                {
                    using (var stream = new StreamReader(webResponse.GetResponseStream()))
                    {
                        string jsonText = stream.ReadToEnd();
                        response.NSEntity = JsonConvert.DeserializeObject<T>(jsonText);
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                var webResponse = ex.Response;
                using (var stream = new StreamReader(webResponse.GetResponseStream()))
                {
                    string jsonText = stream.ReadToEnd();
                    JObject json = JObject.Parse(jsonText);
                    // had to use jObject and deserialize because the resturend error object from NS has a root object.
                    response.Error = new NSError
                    {
                        Code = (string)json["error"]["code"],
                        Message = (string)json["error"]["message"]
                    };
                }
            }

            return response;

        }

        // POST Async
        public static async Task<NSResponse<T>> PostAsync<T>(NSRequest<T> request) where T : NSRootEntity
        {
           
            HttpWebRequest httpWReq = InitNSRequest(request,"POST");

            NSResponse<T> response = new NSResponse<T>();
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request.NSEntity));
                httpWReq.ContentLength = data.Length;

                using (var requestStream = await httpWReq.GetRequestStreamAsync())
                {
                   
                    requestStream.Write(data, 0, data.Length);
                }

                using (var webResponse = await httpWReq.GetResponseAsync())
                {
                    using (var stream = new StreamReader(webResponse.GetResponseStream()))
                    {
                        string jsonText = stream.ReadToEnd();
                        response.NSEntity = JsonConvert.DeserializeObject<T>(jsonText);
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                var webResponse = ex.Response;
                using (var stream = new StreamReader(webResponse.GetResponseStream()))
                {
                    string jsonText = stream.ReadToEnd();
                    JObject json = JObject.Parse(jsonText);
                    // had to use jObject and deserialize because the resturend error object from NS has a root object.
                    response.Error = new NSError
                    {
                        Code = (string)json["error"]["code"],
                        Message = (string)json["error"]["message"]
                    };
                }
            }

            return response;

        }
       // Init
        private static HttpWebRequest InitNSRequest<T>(NSRequest<T> request, string methodType) where T : NSRootEntity
        {

            UriBuilder url = new UriBuilder(request.Url);

            var query = HttpUtility.ParseQueryString(url.Query);

            query["script"] = request.ScriptID.ToString();
            query["deploy"] = request.DeployID.ToString();
            query["recordtype"] = request.NSEntity.RecordType;
            query["id"] = request.NSEntity.ID.ToString();


            url.Query = query.ToString();

            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(url.Uri);

            httpReq.ContentType = CONTENT_TYPE;
            httpReq.Accept = ACCEPT;
            httpReq.Headers.Add(HttpRequestHeader.Authorization,
                String.Format("NLAuth nlauth_account={0},nlauth_email={1},nlauth_signature={2}",
                request.NSAuth.AccountID, request.NSAuth.Username, request.NSAuth.Password));
            httpReq.Method = methodType;

            return httpReq;
        }

    }
}
