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
                "Location: {0}\nMessage: {1}\nName: {2}\nStacktrace:\n{3}",
                location,
                e.Message,
                e.GetType().Name,
                e.StackTrace
            );

            if (e.InnerException != null)
                message += String.Format("\nInner exception: {0}", getMessage(e.GetType().Name, e.InnerException));

            return message;
        }
    }
}
