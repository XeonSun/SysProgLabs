using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg
{
    class WebResData
    {
        private readonly string _adress;
        private readonly string _acsess;
        private readonly DateTime _date;

        public string Adress { get { return _adress; } }
        public string Acsess { get { return _acsess; } }
        public DateTime Date { get { return _date; } }

        public WebResData(string Adress,string Acsess, DateTime Date)
        {
            _adress = Adress;
            _adress = Adress;
            _date = Date;
        }
    }
}
