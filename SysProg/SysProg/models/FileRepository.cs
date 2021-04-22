using Logic.contexts;
using Logic.Model;
using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SysProg
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
            if (data.Name != null)
            {
                _fileContext.Files.Add(data);
                _fileContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректное имя файла. Должен заканчиваться на .exe");
        }

        public void Edit(int index, File data)
        {
            if (data.Name != null)
            {
                File updated = Data[index];
                updated.Name = data.Name;
                updated.Version = data.Version;
                updated.Date = data.Date;
                _fileContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректное имя файла. Должен заканчиваться на .exe");
        }

        public void Delete(int index)
        {
            if (index < Data.Count)
            {
                _fileContext.Files.Remove(Data[index]);
                _fileContext.SaveChanges();
            }
        }

        private void DeleteAll()
        {
            _fileContext.Files.RemoveRange(Data);
            _fileContext.SaveChanges();
        }

        public void AddRange(IEnumerable<File> files)
        {
            _fileContext.Files.AddRange(files);
            _fileContext.SaveChanges();
        }

        public void Export(string path)
        {
            CsvWorker.ExportFiles(path, Data);
        }

        public void Import(string path)
        {
            DeleteAll();
            CsvWorker.ImportFiles(path, Data);
            _fileContext.SaveChanges();
        }
    }
}
