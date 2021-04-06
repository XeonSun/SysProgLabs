using Logic.models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Logic
{
    static class WorkerCSV
    {   
        public static bool ExportResource(string path, IEnumerable<Resource> resources)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    foreach(Resource m in resources)
                        sw.WriteLine(m.Address + "," + m.Type + "," + m.Date);
                }
                return true;
            }
            catch(IOException) { }
            return false;
        }

        public static bool ExportFill(string path, IEnumerable<FileDLL> files)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    foreach (FileDLL m in files)
                        sw.WriteLine(m.Name + "," + m.Version + "," + m.Date);
                }
                return true;
            }
            catch (IOException) { }
            return false;
        }

        public static IEnumerable<Resource> ImportResource(string path)
        {
            try
            {
                List<Resource> resources = new List<Resource>();
                using (StreamReader sr = new StreamReader(path)) {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        resources.Add(new Resource(line[0], line[1], DateTime.Parse(line[2])));
                    }
                }
                return resources;
            }
            catch(Exception) { }
            return null;
        }

        public static IEnumerable<FileDLL> ImportFile(string path)
        {
            try
            {
                List<FileDLL> files = new List<FileDLL>();
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        files.Add(new FileDLL(line[0], line[1], DateTime.Parse(line[2])));
                    }
                }
                return files;
            }
            catch (Exception) { }
            return null;
        }
    }
}
