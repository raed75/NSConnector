using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSConnector.Models;
using Newtonsoft.Json.Linq;

namespace NSConnector.Abstracts
{
    public interface INSEntityMapper
    {
        NSEntity MapToEntity(JToken jsonEntityObject);
    }
}
