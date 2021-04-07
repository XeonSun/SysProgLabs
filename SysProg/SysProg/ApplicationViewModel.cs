using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace SysProg
{
    public class ApplicationViewModel: INotifyPropertyChanged
    {
        private int _count = 0;
        public string Text { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Count { get { return _count; } set { _count = value;Text = _count.ToString();PropertyChanged(this, new PropertyChangedEventArgs("Text")); } }


        private ICommand _incCommand;
        public ICommand IncCommand
        {
            get
            {
                if (_incCommand == null)
                    _incCommand = new IncCounter(this);
                return _incCommand;
            }
        }
        public void Execute(object sender, object parameter)
        {
            ((ICommand)sender).Execute(parameter);
        }
    }



    public class IncCounter : ICommand
    {
        #region CTor

        public IncCounter(ApplicationViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        #endregion

        #region Properties

        public ApplicationViewModel ViewModel { get; set; }

        #endregion

        #region ICommand Members

        public void Execute(object sender)
        {
            ViewModel.Count = ViewModel.Count+1;
            Console.WriteLine(ViewModel.Text);
        }

        #endregion
    }
}
