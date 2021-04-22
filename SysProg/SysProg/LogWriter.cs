using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg
{
    static class LogWriter
    {
        public static void WriteToLog(string s,bool showDateTime = true)
        {
            Console.Write(DateTime.Now + ": " + s + "\n");
        }
    }
}
