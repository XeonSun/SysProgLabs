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
        public void Edit(int index)
        {

        }
        public void Delete(int index)
        {

        }
    }
}
