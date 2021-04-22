using Logic.contexts;
using Logic.models;
using System.Collections.Generic;
using System.Linq;

namespace SysProg
{
    public class ResRepository: IRepository<Resource>
    {
        private ResContext _resContext;

        public ResRepository()
        {
            _resContext = new ResContext();
        }

        public IList<Resource> Data { get { return _resContext.Resources.ToList(); } }

        public void Add(Resource data)
        {
            
            _resContext.Resources.Add(data);
            _resContext.SaveChanges();
        }
        public void Edit(int index,Resource data)
        {
            Resource updated = Data[index];
            updated.Address = data.Address;
            updated.Type = data.Address;
            updated.Date = data.Date;
            _resContext.SaveChanges();
        }

        public void Delete(int index)
        {
            if (index < Data.Count)
            {
                _resContext.Resources.Remove(Data[index]);
                _resContext.SaveChanges();
            }
        }

        public void Import(string path)
        {

        }

        public void Export(string path)
        {

        }
    }
}
