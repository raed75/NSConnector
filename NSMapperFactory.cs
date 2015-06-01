using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSConnector.Models;
using NSConnector.EntityMappers;
using NSConnector.Abstracts;

namespace NSConnector
{
    public sealed class NSMapperFactory
    {
        private static readonly NSMapperFactory _instance = new NSMapperFactory();

        private NSMapperFactory(){}
        
        public static NSMapperFactory GetNSMapperFactory()
        {
            return _instance;
        }

        public INSEntityMapper<NSEntity> GetMapper(Type entityType)
        {

            INSEntityMapper<NSEntity> mapper;
            
             if (entityType == typeof(NSCustomer))
               mapper = new NSCustomerMapper();
            else
                mapper = null;
            return mapper;
        }
  
    }
}
