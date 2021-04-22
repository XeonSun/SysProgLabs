using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg
{
    public interface IRepository<T>
    {
        IList<T> Data { get; }

        void Add(T data);
        void Edit(int index, T data);
        void Delete(int index);
        void Import(string path);
        void Export(string path);
    }
}
