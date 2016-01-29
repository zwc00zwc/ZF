using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.Db
{
    public class LibJsonConvert
    {
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T DeserializeObject<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
