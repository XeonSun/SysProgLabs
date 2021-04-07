using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg
{
    public interface ICommand
    {
        void Execute(object sender);
    }
}
