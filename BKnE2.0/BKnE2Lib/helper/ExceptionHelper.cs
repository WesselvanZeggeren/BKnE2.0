using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Lib.helper
{
    public static class ExceptionHelper
    {

        public static void print(string location, Exception e)
        {

            Console.WriteLine(getMessage(location, e));
        }

        public static string getMessage(string location, Exception e)
        {

            string message = String.Format(
                "Location: {0}\n\tMessage: {1}\n\tName: {2}\n\tStacktrace: {3}",
                location,
                e.Message,
                e.GetType().Name,
                e.StackTrace
            );

            if (e.InnerException != null)
                message += String.Format("Inner exception: {0}", e.InnerException.Message);

            return message;
        }
    }
}
