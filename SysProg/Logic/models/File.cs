using System;

namespace Logic.models
{
    public class File
    {
        private string _name, _version;
        private DateTime _date;

        public int Id { get; set; }
        /// <summary>
        /// Имя exe файла
        /// </summary>
        public string Name { get { return _name; } set { if (value.EndsWith(".exe")) _name = value; else _name = null; } }
        /// <summary>
        /// Версия файла
        /// </summary>
        public string Version { get { return _version; } set { _version = value; } }
        /// <summary>
        /// Дата создания файла
        /// </summary>
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
