using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Lib.data
{

    public class Color
    {

        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }

        public static Color newColor(int r, int g, int b)
        {

            Color color = new Color();

            color.r = r;
            color.g = g;
            color.b = b;

            return color;
        }

        public static Color generateColor()
        {

            Random random = new Random();

            return newColor(random.Next(256), random.Next(256), random.Next(256));
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this);
        }
    }
}
