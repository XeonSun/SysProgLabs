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
            label1.Text = "Aa;fkaf;";

            applicationViewModel = new ApplicationViewModel();

            label1.DataBindings.Add(new Binding("Text", applicationViewModel, "Text"));




            button1.Tag = applicationViewModel.IncCommand;
            

            button1.Click+= new EventHandler(
                (object sender, EventArgs e) =>
                {
                    IncCount();
                }
                );


            divParamATextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "divParamA"));
            divParamBTextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "divParamB"));
            divResultTextBox.DataBindings.Add(new Binding("Text", applicationViewModel, "divResult"));

            divCountButton.Tag = applicationViewModel.CalcLowLevelDiv;
            divCountButton.Click += new EventHandler((object sender, EventArgs e) => { applicationViewModel.Execute(divCountButton.Tag, null); });

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
