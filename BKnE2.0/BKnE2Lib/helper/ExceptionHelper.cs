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

            Console.WriteLine(getMessage(e));
        }

        public static string getMessage(Exception e)
        {

            string message = String.Format(
                "Message: {0}\n name: {1}\nStacktrace: {2}",
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
