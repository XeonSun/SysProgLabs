using Logic.models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Logic.Model
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

        public static bool ExportFill(string path, IEnumerable<models.File> files)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    foreach (models.File m in files)
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

        public static IEnumerable<models.File> ImportFile(string path)
        {
            try
            {
                List<models.File> files = new List<models.File>();
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        files.Add(new models.File(line[0], line[1], DateTime.Parse(line[2])));
                    }
                }
                return files;
            }
            catch (Exception) { }
            return null;
        }
    }
}
