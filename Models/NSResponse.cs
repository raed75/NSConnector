using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace NSConnector.Models
{

    public class NSResponse
    {

        public NSEntity Entity { get; set; }
        public NSError Error { get; set; }

    }

}