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
            Console.WriteLine(Logic.Logic.CheckStructVar7("bool condition = true || false;" +
                "while((true) && (condition));" +
                "condition = false;"));
            Console.WriteLine(Logic.Logic.CheckStructVar11("int length = 800;" +
                "for(int i =0; i < length; i++) " +
                "length--;"));
        }
    }
}
