using SysProg.views;
using System;
using System.Windows.Forms;

namespace SysProg
{
    public partial class MainForm : Form, IMainView
    {
        protected ApplicationContext _context;

        public MainForm(ApplicationContext context)
        {
            _context = context;
            InitializeComponent();

            checkWhileButton.Click += (sender, args) =>  Invoke(WhileAnalysis);
            checkForButton.Click += (sender, args) => Invoke(ForAnalysis);
            divCountButton.Click += (sender, args) => Invoke(DivCalculation);
            xorCountButton.Click += (sender, args) => Invoke(XorCalculation);
        }

        private void Invoke(Action action)
        {
            if (action != null) action();
        }

        public event Action WhileAnalysis;

        public event Action ForAnalysis;

        public event Action DivCalculation;

        public event Action XorCalculation;

        public void GetWhileStruct(ref string structure)
        {
            structure = whileRichTextBox.Text;
        }
        public void SetWhileResult(string result)
        {
            whileResultLabel.Text = result;
        }

        public void GetForStruct(ref string structure)
        {
            structure = forRichTextBox.Text;
        }
        public void SetForResult(string result)
        {
            forResultLabel.Text = result;
        }

        public void GetDivParams(ref string a, ref string b)
        {
            a = divParamATextBox.Text;
            b = divParamBTextBox.Text;
        }
        public void SetDivResult(string result)
        {
            divResultTextBox.Text = result;
        }

        public void GetXorParams(ref string a, ref string b)
        {
            a = xorParamATextBox.Text;
            b = xorParamBTextBox.Text;
        }
        public void SetXorResult(string result)
        {
            xorResultTextBox.Text = result;
        }

        public new void Show()
        {
            _context.MainForm = this;
            Application.Run(this);
        }
    }
}
