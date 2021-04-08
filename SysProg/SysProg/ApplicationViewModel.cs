using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Logic;
using LowLevelFunctions;


namespace SysProg
{
    public class ApplicationViewModel: INotifyPropertyChanged
    {
        private int _count = 0;
        public int Count { get { return _count; } set { _count = value; Text = _count.ToString();PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text")); } }
        public string Text { get; set; }

        //WHILE
        public string StructureWhile { get; set; }
        private string _resultWhile;
        public string ResultWhile { get { return _resultWhile; } set { _resultWhile = value;PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ResultWhile")); } }

        //FOR
        public string StructureFor { get; set; }
        private string _resultFor;
        public string ResultFor { get { return _resultFor; } set { _resultFor = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ResultFor")); } }

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
        public ICommand AnalysisWhile
        {
            get { return _analysisWhile ?? (_analysisWhile = new AnalisisWhileCommand(this)); }
        }


        private ICommand _analysisFor;
        public ICommand AnalysisFor
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
    

    public class CalcLowLevelDiv : Command
    {
        public CalcLowLevelDiv(ApplicationViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object sender)
        {
            ViewModel.divResult = LowLevelFunctions.LowLevelFunctions.LowLelelDiv(int.Parse(ViewModel.divParamA), int.Parse(ViewModel.divParamB)).ToString();
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
            ViewModel.xorResult = LowLevelFunctions.LowLevelFunctions.LowLelelXor(int.Parse(ViewModel.xorParamA), int.Parse(ViewModel.xorParamB)).ToString();
            Console.WriteLine(ViewModel.xorResult);
        }
    }

    public class IncCounter : Command
    {
        public IncCounter(ApplicationViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object sender)
        {
            ViewModel.Count = ViewModel.Count+1;
            Console.WriteLine(ViewModel.Text);
        }

    }

    public class AnalisisWhileCommand : Command
    {
        public AnalisisWhileCommand(ApplicationViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object sender)
        {
            try
            {
                Task.Factory.StartNew(() => ViewModel.ResultWhile = StructureAnalysis.CheckStructVar7(ViewModel.StructureWhile).ToString());
            }
            catch (Exception)
            {
                ViewModel.ResultWhile = "Неверно введенное выражение";
            }
        }

    }

    public class AnalisisForCommand : Command
    {
        public AnalisisForCommand(ApplicationViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object sender)
        {
            try
            {
                Task.Factory.StartNew(() => ViewModel.ResultFor = StructureAnalysis.CheckStructVar11(ViewModel.StructureFor).ToString());
            }
            catch(Exception)
            {
                ViewModel.ResultFor = "Неверно введенное выражение";
            }
        }
    }

}


