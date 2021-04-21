using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysProg
{
    public abstract class Command : ICommand
    {
        public Command(ApplicationViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ApplicationViewModel ViewModel { get; set; }

        public abstract void Execute(object sender);
    }
}
