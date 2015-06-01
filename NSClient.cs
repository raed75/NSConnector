using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using NSConnector.Abstracts;
using NSConnector.EntityMappers;

namespace NSConnector
{
    public class NSClient
    {
       
        // GET Async
        public static async Task<NSResponse<T>> GetAsync<T>(NSRequest<T> request) where T : NSEntity
        {
            NSResponse<T> response = new NSResponse<T>();
            
            NSCaller nsCaller = new NSCaller(request.Url,
                request.NSAuth.AccountID.ToString(),
                request.NSAuth.Username,
                request.NSAuth.Password,
                request.ScriptID.ToString(),
                request.DeployID.ToString());

            string jsonResult = await nsCaller.Get(request.RequestParameters);
            
            JToken jsonObject = JToken.Parse(jsonResult);

            // check for error
            JToken error = jsonObject.SelectToken("error");
            
            if (error != null)
            {
                response.Error = new NSError()
                {
                    Code = (string)error["code"],
                    Message = (string)error["message"]
                };
            }
            else
            {
                NSMapperFactory mapperFactory = NSMapperFactory.GetNSMapperFactory();
                INSEntityMapper<NSEntity> mapper = mapperFactory.GetMapper(typeof(T));
                response.Entity = (T)mapper.MapToEntity(jsonObject);
            }
            
            return response;

        }

        // POST Async
        public static async Task<NSResponse<T>> PostAsync<T>(NSRequest<T> request) where T : NSEntity
        {

            NSResponse<T> response = new NSResponse<T>();
            
            NSCaller nsCaller = new NSCaller(request.Url,
                request.NSAuth.AccountID.ToString(),
                request.NSAuth.Username,
                request.NSAuth.Password,
                request.ScriptID.ToString(),
                request.DeployID.ToString());

            
            string jsonResult = await nsCaller.Post(JsonConvert.SerializeObject(request.NSEntity));

            JToken jsonObject = JToken.Parse(jsonResult);

            // check for error
            JToken error = jsonObject.SelectToken("error");
            if (error != null)
                response.Error = new NSError()
                {
                    Code = (string)error["code"],
                    Message = (string)error["message"]
                };
            else
            {
                NSMapperFactory mapperFactory = NSMapperFactory.GetNSMapperFactory();
                INSEntityMapper<NSEntity> mapper = mapperFactory.GetMapper(typeof(T));
                response.Entity = (T)mapper.MapToEntity(jsonObject);
            }            
            return response;

        }
        
        }
}
