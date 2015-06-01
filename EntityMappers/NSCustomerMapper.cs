using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSConnector.Abstracts;
using NSConnector.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NSConnector.EntityMappers
{
    public class NSCustomerMapper : INSEntityMapper<NSCustomer>
    {

        public NSCustomer MapToEntity(JToken jsonObject)
        {
            NSCustomer customer = JsonConvert.DeserializeObject<NSCustomer>(jsonObject.ToString());

            return customer;
        }
    }
}
