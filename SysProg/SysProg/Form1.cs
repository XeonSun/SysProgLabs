using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SysProg
{
    public partial class Form1 : Form
    {
        private ApplicationViewModel _applicationViewModel;
        public Form1()
        {
            InitializeComponent();
            InitialControlHandlers();
        }

        private void InitialControlHandlers()
        {
            _applicationViewModel = new ApplicationViewModel();

            label1.DataBindings.Add(new Binding("Text", applicationViewModel, "Text"));
            button1.Tag = applicationViewModel.IncCommand;
            button1.Click+= new EventHandler(
                (object sender, EventArgs e) =>
                {
                    IncCount();
                }
                );



            whileRichTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "StructureWhile"));

            whileResultLabel.DataBindings.Add(new Binding("Text", _applicationViewModel, "ResultWhile"));
            checkWhileButton.Click+= new EventHandler((object sender, EventArgs e) => { _applicationViewModel.Execute(_applicationViewModel.AnalysisWhile, null); });

            forRichTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "StructureFor"));

            forResultLabel.DataBindings.Add(new Binding("Text", _applicationViewModel, "ResultFor"));
            checkForButton.Click += new EventHandler((object sender, EventArgs e) => { _applicationViewModel.Execute(_applicationViewModel.AnalysisFor, null); });


            divParamATextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "divParamA"));
            divParamBTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "divParamB"));
            divResultTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "divResult"));

            divCountButton.Click += new EventHandler((object sender, EventArgs e) => { _applicationViewModel.Execute(_applicationViewModel.CalcLowLevelDiv, null); });

            xorParamATextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "xorParamA"));
            xorParamBTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "xorParamB"));
            xorResultTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "xorResult"));
            xorCountButton.Click += new EventHandler((object sender, EventArgs e) => { _applicationViewModel.Execute(_applicationViewModel.CalcLowLevelXor, null); });
        }


        private void IncCount()
        {
            applicationViewModel.Execute(button1.Tag, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
