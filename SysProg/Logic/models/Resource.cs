using System;

namespace Logic.models
{
    public class Resource
    {
        private string _address, _type;
        private DateTime _date;

        public int Id { get; set; }
        public string Address { get { return _address; } set { _address = value; } }
        public string Type { get { return _type; } set { _type = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }

        public Resource(string address, string type, DateTime date)
        {
            Address = address;
            Type = type;
            Date = date;
        }

        public Resource() { }
    }
}
