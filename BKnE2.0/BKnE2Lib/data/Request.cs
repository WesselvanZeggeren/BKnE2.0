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
        public List<Tuple<string, dynamic>> parameters;

        private Request() { }

        public static Request newRequest(string type = null)
        {

            Request request = new Request();
            request.type = type;
            request.parameters = new List<Tuple<string, dynamic>>();

            return request;
        }

        public void add(string id, dynamic value)
        {

            this.parameters.Add(new Tuple<string, dynamic>(id, value));
        }

        public dynamic get(string id)
        {

            foreach (Tuple<string, dynamic> parameter in this.parameters)
                if (parameter.Item1 == id)
                    return parameter.Item2;

            return null;
        }

        public void clear()
        {

            this.parameters = new List<Tuple<string, dynamic>>();
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
