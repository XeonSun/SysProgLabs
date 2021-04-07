using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SysProg
{
    class Model : INotifyPropertyChanged
    {
        private ObservableCollection<WebResData> _webResList;


        public Model()
        {

            
        }


        public event PropertyChangedEventHandler PropertyChanged;

    }
}
