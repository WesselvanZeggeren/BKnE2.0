using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Lib.data
{

    public class Request
    {

        public string type;
        public Dictionary<string, dynamic> parameters;

        private Request() { }

        public static Request newRequest(string type = null)
        {

            Request request = new Request();
            request.type = type;
            request.parameters = new Dictionary<string, dynamic>();

            return request;
        }

        public void add(string id, dynamic value)
        {

            this.parameters.Add(id, value);
        }

        public dynamic get(string id)
        {

            dynamic value;

            this.parameters.TryGetValue(id, out value);

            return value;
        }

        public void clear()
        {

            this.parameters = new Dictionary<string, dynamic>();
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
