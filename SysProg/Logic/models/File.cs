using System;

namespace Logic.models
{
    public class File
    {
        private string _name, _version;
        private DateTime _date;

        public string Name { get { return _name; } set { _name = value; } }
        public string Version { get { return _version; } set { _version = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }

        public File(string name, string version, DateTime date)
        {
            Name = name;
            Version = version;
            Date = date;
        }

        public File() { }
    }
}
