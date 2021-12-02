using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.infrestracuter
{
    public static class SessionExtentions
    {
        public static void setjson(this ISession session,string key,object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Getjson<T>(this ISession session, string key)
        {
            var sessiondata = session.GetString(key);
            return sessiondata == null ? default(T) : JsonConvert.DeserializeObject<T>(sessiondata);
        }
    }
}
