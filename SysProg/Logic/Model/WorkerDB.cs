using Logic.contexts;
using Logic.models;
using System.Collections.Generic;

namespace Logic.Model
{
    static class WorkerDB
    {
        public static bool ImportResources(IEnumerable<Resource> resources)
        {
            try
            {
                using (ResourceContext context = new ResourceContext())
                {
                    context.Resources.AddRange(resources);
                    context.SaveChanges();
                }
            }
            catch { }
            return false;
        }

        public static bool ImportFiles(IEnumerable<File> files)
        {
            try
            {
                using (FileContext context = new FileContext())
                {
                    context.Files.AddRange(files);
                    context.SaveChanges();
                }
            }
            catch { }
            return false;
        }

        public static IEnumerable<Resource> ExportResources()
        {
            try
            {
                using (ResourceContext context = new ResourceContext())
                {
                    return context.Resources;
                }
            }
            catch { }
            return null;
        }

        public static IEnumerable<File> ExportFiles()
        {
            try
            {
                using (FileContext context = new FileContext())
                {
                    return context.Files;
                }
            }
            catch { }
            return null;
        }
    }
}
