using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PayMobIntegration.Extenions
{
    public static class JsonContent
    {
        public static StringContent ToJsonObject(this object Obj) 
        {
            return new StringContent(JsonConvert.SerializeObject(Obj), Encoding.UTF8, "application/json");
        }
    }
}
