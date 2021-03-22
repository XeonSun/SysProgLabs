using System;
using System.Windows.Forms;
using Logic;

namespace SysProg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine(Logic.Logic.CheckStructVar7("while(true);"));
        }
    }
}
