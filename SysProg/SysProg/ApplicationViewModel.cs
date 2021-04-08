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
        public int Count { get { return _count; } set { _count = value; Text = _count.ToString();PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text")); } }
        public string Text { get; set; }
        public string StructureWhile { get; set; }
        public string StructureFor { get; set; }
        public string ResultWhile { get; set; }
        public string ResultFor { get; set; }

        //DIV
        public string divParamA { get; set; }
        public string divParamB { get; set; }

        private string _divResult;
        public string divResult { get { return _divResult; } set {_divResult = value ; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("divResult")); } }

        //XOR

        public string xorParamA { get; set; }
        public string xorParamB { get; set; }

        private string _xorResult;
        public string xorResult { get { return _xorResult; } set { _xorResult = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("xorResult")); } }




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

        private ICommand _calcLowLevelDiv;
        public ICommand CalcLowLevelDiv
        {
            get { return _calcLowLevelDiv ?? (_calcLowLevelDiv = new CalcLowLevelDiv(this)); }
        }

        private ICommand _calcLowLevelXor;
        public ICommand CalcLowLevelXor
        {
            get { return _calcLowLevelXor ?? (_calcLowLevelXor = new CalcLowLevelXor(this)); }
        }

    }
    
    
    public abstract class Command:ICommand
    {
        public Command(ApplicationViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ApplicationViewModel ViewModel { get; set; }

        public abstract void Execute(object sender);
    }



    public class CalcLowLevelDiv : Command
    {
        public CalcLowLevelDiv(ApplicationViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object sender)
        {
            ViewModel.divResult = (int.Parse(ViewModel.divParamA) / int.Parse(ViewModel.divParamB)).ToString();
            Console.WriteLine(ViewModel.divResult);
        }
    }


    public class CalcLowLevelXor : Command
    {
        public CalcLowLevelXor(ApplicationViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object sender)
        {
            ViewModel.xorResult = (int.Parse(ViewModel.xorParamA) ^ int.Parse(ViewModel.xorParamB)).ToString();
            Console.WriteLine(ViewModel.xorResult);
        }
    }


    public class IncCounter : ICommand
    {

        public IncCounter(ApplicationViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ApplicationViewModel ViewModel { get; set; }

        public void Execute(object sender)
        {
            ViewModel.Count = ViewModel.Count+1;
            Console.WriteLine(ViewModel.Text);
        }

    }

    public class AnalisisWhileCommand : ICommand
    {

        public AnalisisWhileCommand(ApplicationViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ApplicationViewModel ViewModel { get; set; }


        public void Execute(object sender)
        {
            //TODO
        }

    }

    public class AnalisisForCommand : ICommand
    {

        public AnalisisForCommand(ApplicationViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ApplicationViewModel ViewModel { get; set; }

        public void Execute(object sender)
        {
            //TODO
        }

    }

}


