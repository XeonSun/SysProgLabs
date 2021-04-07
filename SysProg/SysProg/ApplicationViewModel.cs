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
        public int Count { get { return _count; } set { _count = value; Text = _count.ToString(); PropertyChanged(this, new PropertyChangedEventArgs("Text")); } }
        public string Text { get; set; }
        public string StructureWhile { get; set; }
        public string StructureFor { get; set; }
        public string ResultWhile { get; set; }
        public string ResultFor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;



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


        private ICommand _analysisWhile;
        public ICommand AnalisisWhile
        {
            get { return _analysisWhile ?? (_analysisWhile = new AnalisisWhileCommand(this)); }
        }


        private ICommand _analysisFor;
        public ICommand AnalisisFor
        {
            get { return _analysisFor ?? (_analysisFor = new AnalisisForCommand(this)); }
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

    public class AnalisisWhileCommand : ICommand
    {
        #region CTor

        public AnalisisWhileCommand(ApplicationViewModel viewModel)
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
            //TODO
        }

        #endregion
    }

    public class AnalisisForCommand : ICommand
    {
        #region CTor

        public AnalisisForCommand(ApplicationViewModel viewModel)
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
            //TODO
        }

        #endregion
    }

}


