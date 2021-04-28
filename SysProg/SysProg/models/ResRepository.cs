using Logic.contexts;
using Logic.Model;
using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SysProg
{
    public class ResRepository : IRepository<Resource>
    {
        private ResContext _resContext;

        public ResRepository()
        {
            _resContext = new ResContext();
        }

        public IList<Resource> Data { get { return _resContext.Resources.ToList(); } }

        public void Add(Resource data)
        {
            if (data.Type != null)
            {
                _resContext.Resources.Add(data);
                _resContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректный тип доступа.'открытый' или 'закрытый')");
        }

        public void Add(string path)
        {
            throw new Exception("Не доступно.");
        }

        public void Edit(int index, Resource data)
        {
            if (data.Type != null)
            {
                Resource updated = Data[index];
                updated.Address = data.Address;
                updated.Type = data.Type;
                updated.Date = data.Date;
                _resContext.SaveChanges();
            }
            else
                throw new ArgumentException("Не корректный тип доступа.'открытый' или 'закрытый')");
        }

        public void Delete(int index)
        {
            if (index < Data.Count)
            {
                _resContext.Resources.Remove(Data[index]);
                _resContext.SaveChanges();
            }
        }

        private void DeleteAll()
        {
            _resContext.Resources.RemoveRange(Data);
            _resContext.SaveChanges();
        }

        public void AddRange(IEnumerable<Resource> resources)
        {
            _resContext.Resources.AddRange(resources);
            _resContext.SaveChanges();
        }

        public void Import(string path)
        {
            DeleteAll();
            List<Resource> resources = new List<Resource>();
            CsvWorker.ImportResources(path, resources);
            _resContext.Resources.AddRange(resources);
            _resContext.SaveChanges();
        }

        public void Export(string path)
        {
            CsvWorker.ExportResources(path, Data);
        }
    }
}
