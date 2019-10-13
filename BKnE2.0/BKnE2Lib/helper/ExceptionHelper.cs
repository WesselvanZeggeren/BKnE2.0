using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Lib.helper
{
    public static class ExceptionHelper
    {

        public static void print(Exception e)
        {

            Console.WriteLine(
                "Message: {0}\n name: {1}\nStacktrace: {2}", 
                e.Message, 
                e.GetType().Name, 
                e.StackTrace
            );

            if (e.InnerException != null)
                Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
        }
    }
}
