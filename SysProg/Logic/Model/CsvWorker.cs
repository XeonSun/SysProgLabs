using Logic.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logic.Model
{
    public static class CsvWorker
    {
        public static void ExportFiles(string path, IEnumerable<models.File> files)
        {
            using (var streamWriter = new StreamWriter(path, false))
            {
                foreach (var file in files)
                {
                    var str = new StringBuilder();
                    str.Append(file.Name).Append(',').Append(file.Version).Append(',').Append(file.Date).Append('\n');
                    streamWriter.Write(str.ToString());
                }
            }
        }

        public static void ImportFiles(string path, IList<models.File> files)
        {
            using (var streamReader = new StreamReader(path))
            {
                try
                {
                    while (!streamReader.EndOfStream)
                    {
                        string data = streamReader.ReadLine();
                        var file = data.Split(',');
                        if (file[0].EndsWith(".exe"))
                            files.Add(new models.File(file[0], file[1], DateTime.Parse(file[2])));
                        else
                            throw new ArgumentException("Не правильное имя файла");
                    }
                }
                catch (Exception)
                {
                    throw new ArgumentException();
                }
            }
        }

        public static void ExportResources(string path, IEnumerable<Resource> resources)
        {
            using (var streamWriter = new StreamWriter(path, false))
            {
                foreach (var resource in resources)
                {
                    var str = new StringBuilder();
                    str.Append(resource.Address).Append(',').Append(resource.Type).Append(',').Append(resource.Date).Append('\n');
                    streamWriter.Write(str.ToString());
                }
            }
        }

        public static void ImportResources(string path, IList<Resource> resources)
        {
            using (var streamReader = new StreamReader(path))
            {
                try
                {
                    while (!streamReader.EndOfStream)
                    {
                        string data = streamReader.ReadLine();
                        var file = data.Split(',');
                        if (file[1].ToUpper() == "ЗАКРЫТЫЙ" || file[1].ToUpper() == "ОТКРЫТЫЙ")
                            resources.Add(new Resource(file[0], file[1], DateTime.Parse(file[2])));
                        else
                            throw new ArgumentException("Не правильный тип доступа.");
                    }
                }
                catch (Exception)
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}
