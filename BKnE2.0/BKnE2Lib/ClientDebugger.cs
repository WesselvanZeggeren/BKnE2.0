using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKnE2Lib
{
    public class Debug
    {
        static string pathName = $@"C:\Users\Jaspe\Desktop\File.txt";
        static bool entered = false;

        public static void Log(string msg)
        {
            if(entered == false)
            {
                entered = true;
                File.WriteAllText(pathName, msg);
            }
            else
            {
                string[] lines = File.ReadAllLines(pathName);
                string[] newLines = new string[lines.Length + 1];
                lines.CopyTo(newLines, 0);
                newLines[newLines.Length - 1] = msg;
                File.WriteAllLines(pathName, newLines);
            }
        }
    }
}
