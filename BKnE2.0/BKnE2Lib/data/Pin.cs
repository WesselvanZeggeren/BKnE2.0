using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Lib.data
{
    public class Pin
    {

        public int x { get; set; }
        public int y { get; set; }
        public bool isAssigned { get; set; }

        public static Pin newPin(int x, int y)
        {

            Pin pin = new Pin();

            pin.x = x;
            pin.y = y;
            pin.isAssigned = false;

            return pin;
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
    