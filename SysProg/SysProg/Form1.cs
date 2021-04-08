using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SysProg
{
    public partial class Form1 : Form
    {
        private ApplicationViewModel applicationViewModel;
        public Form1()
        {
            InitializeComponent();
            this.InitialControlHandlers();
        }

        private void InitialControlHandlers()
        {
            applicationViewModel = new ApplicationViewModel();

            label1.DataBindings.Add(new Binding("Text", applicationViewModel, "Text"));
            button1.Tag = applicationViewModel.IncCommand;
            button1.Click+= new EventHandler(
                (object sender, EventArgs e) =>
                {
                    IncCount();
                }
                );



            whileRichTextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "StructureWhile"));

            whileResultLabel.DataBindings.Add(new Binding("Text", applicationViewModel, "ResultWhile"));
            checkWhileButton.Click+= new EventHandler((object sender, EventArgs e) => { applicationViewModel.Execute(applicationViewModel.AnalysisWhile, null); });

            forRichTextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "StructureFor"));

            forResultLabel.DataBindings.Add(new Binding("Text", applicationViewModel, "ResultFor"));
            checkForButton.Click += new EventHandler((object sender, EventArgs e) => { applicationViewModel.Execute(applicationViewModel.AnalysisFor, null); });


            divParamATextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "divParamA"));
            divParamBTextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "divParamB"));
            divResultTextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "divResult"));

            divCountButton.Click += new EventHandler((object sender, EventArgs e) => { applicationViewModel.Execute(applicationViewModel.CalcLowLevelDiv, null); });

            xorParamATextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "xorParamA"));
            xorParamBTextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "xorParamB"));
            xorResultTextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "xorResult"));
            xorCountButton.Click += new EventHandler((object sender, EventArgs e) => { applicationViewModel.Execute(applicationViewModel.CalcLowLevelXor, null); });
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
