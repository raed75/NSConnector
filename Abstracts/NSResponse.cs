using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NSConnector.Abstracts;
using NSConnector.Models;


namespace NSConnector.Abstracts
{

    //public class NSResponse
    //{

    //    public NSEntity Entity { get; set; }
    //    public NSError Error { get; set; }

    //}

    public class NSResponse<T> where T:NSEntity
    {

        public T Entity { get; set; }
        public NSError Error { get; set; }

    }

}