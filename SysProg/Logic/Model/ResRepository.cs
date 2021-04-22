using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.contexts;
using Logic.models;

namespace Logic.Model
{
    class ResRepository
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
    }
}
