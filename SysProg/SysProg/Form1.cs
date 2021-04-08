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

            divParamATextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "divParamA"));
            divParamBTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "divParamB"));
            divResultTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "divResult"));

            divCountButton.Tag = _applicationViewModel.CalcLowLevelDiv;
            divCountButton.Click += new EventHandler((object sender, EventArgs e) => { _applicationViewModel.Execute(divCountButton.Tag, null); });

            xorParamATextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "xorParamA"));
            xorParamBTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "xorParamB"));
            xorResultTextBox.DataBindings.Add(new Binding("Text", _applicationViewModel, "xorResult"));

            xorCountButton.Tag = _applicationViewModel.CalcLowLevelXor;
            xorCountButton.Click += new EventHandler((object sender, EventArgs e) => { _applicationViewModel.Execute(_applicationViewModel.CalcLowLevelXor, null); });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
