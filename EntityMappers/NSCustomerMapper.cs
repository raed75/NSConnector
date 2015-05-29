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
    public class NSCustomerMapper : INSEntityMapper
    {

        public NSEntity MapToEntity(JToken jsonCustomer)
        {
            NSCustomer customer = null;
            JToken customerColumns;

            // check if array of customers returned then return the first customer in the array
            if (jsonCustomer.HasValues && jsonCustomer.First is JArray)
            {
                // return the first customer in the array
                customerColumns = jsonCustomer.First["columns"];
            }
            else
            {
                // try to parse the root element as a customer
                customerColumns = jsonCustomer["columns"];
            }

            if(customerColumns != null)
                customer = JsonConvert.DeserializeObject<NSCustomer>(customerColumns.ToString());

            return customer;
        }

    }
}
