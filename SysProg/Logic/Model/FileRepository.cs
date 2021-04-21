using Logic.contexts;
using Logic.models;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Model
{
    public class FileRepository : IRepository<File>
    {
        private FileContext _fileContext;

        public FileRepository()
        {
            _fileContext = new FileContext();
        }

        public IList<File> Data { get { return _fileContext.Files.ToList(); } }

        public void Add(File data)
        {
            _fileContext.Files.Add(data);
            _fileContext.SaveChanges();
        }
        public void Edit(int index, File data)
        {
            File updated = Data[index];
            File file = _fileContext.Files.Where(x => x.Name == updated.Name && x.Version == updated.Version && x.Date == updated.Date).FirstOrDefault();
            file.Name = data.Name;
            file.Version = data.Version;
            file.Date = data.Date;
           _fileContext.SaveChanges();
        }

        public void Delete(int index)
        {
            if (index < Data.Count)
            {
                _fileContext.Files.Remove(Data[index]);
                _fileContext.SaveChanges();
            }
        }
    }
}
