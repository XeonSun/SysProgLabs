using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg.views
{
    public interface IFillView<T>:IView
    {
        void GetData(T data);

        void SetData(T data);

        void SetError(string error);

        event Action Submit;
    }
}
