using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg
{
    class LogWriter:ILogWriter
    {
        public void WriteToLog(string s)
        {
            Console.Write(DateTime.Now + ": " + s + "\n");
        }
    }
}
