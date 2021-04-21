using Logic.models;
using SysProg.views;
using System;
using System.Collections.Generic;
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
            addFButton.Click += (sender, args) => Invoke(AddFile);
            editFButton.Click += (sender, args) => Invoke(UpdateFile);
            deleteFButton.Click += (sender, args) => Invoke(DeleteFile);
        }

        private void Invoke(Action action)
        {
            if (action != null) action();
        }

        public event Action WhileAnalysis;

        public event Action ForAnalysis;

        public event Action DivCalculation;

        public event Action XorCalculation;

        public event Action AddFile;
        public event Action UpdateFile;
        public event Action DeleteFile;

        public event Action AddResource;
        public event Action UpdateResource;
        public event Action DeleteResource;

        public void LoadFiles(IList<File> files)
        {
            for(int i =0; i < fDataGridView.Rows.Count; i++)
                files.Add(new File((string)fDataGridView.Rows[i].Cells[0].Value, (string)fDataGridView.Rows[i].Cells[0].Value, DateTime.Parse(fDataGridView.Rows[i].Cells[0].Value.ToString())));
        }

        public void UpdateFiles(IList<File> files)
        {
            fDataGridView.Rows.Clear();
            foreach (var file in files)
                fDataGridView.Rows.Add(file.Name, file.Version, file.Date);
        }

        public void GetFileIndex(ref int index)
        {
            index = fDataGridView.Rows.IndexOf(fDataGridView.SelectedRows[0]);
        }

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
