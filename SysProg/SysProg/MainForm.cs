using Logic.models;
using SysProg.views;
using System;
using System.Collections.Generic;
using System.Text;
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

            checkWhileButton.Click += (sender, args) => Invoke(WhileAnalysis);
            checkForButton.Click += (sender, args) => Invoke(ForAnalysis);
            divCountButton.Click += (sender, args) => Invoke(DivCalculation);
            xorCountButton.Click += (sender, args) => Invoke(XorCalculation);

            addFButton.Click += (sender, args) => Invoke(AddFile);
            editFButton.Click += (sender, args) => Invoke(UpdateFile);
            deleteFButton.Click += (sender, args) => Invoke(DeleteFile);
            exportFButton.Click += (sender, args) => Invoke(ExportFiles);
            importFButton.Click += (sender, args) => Invoke(ImportFiles);

            addRButton.Click += (sender, args) => Invoke(AddResource);
            editRButton.Click += (sender, args) => Invoke(UpdateResource);
            deleteRButton.Click += (sender, args) => Invoke(DeleteResource);
            exportRButton.Click += (sender, args) => Invoke(ExportRes);
            importRButton.Click += (sender, args) => Invoke(ImportRes);
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
        public event Action ExportFiles;
        public event Action ImportFiles;

        public event Action AddResource;
        public event Action UpdateResource;
        public event Action DeleteResource;
        public event Action ExportRes;
        public event Action ImportRes;

        public void LoadFiles(IList<File> files)
        {
            for (int i = 0; i < fDataGridView.Rows.Count - 1; i++)
            {
                files.Add(new File((string)fDataGridView.Rows[i].Cells[0].Value, (string)fDataGridView.Rows[i].Cells[1].Value, DateTime.Parse(fDataGridView.Rows[i].Cells[2].Value.ToString())));
            }
        }

        public void UpdateFiles(IList<File> files)
        {
            fDataGridView.Rows.Clear();
            foreach (var file in files)
                fDataGridView.Rows.Add(file.Name, file.Version, file.Date);
        }

        public void GetFileIndex(ref int index)
        {
            var selected = fDataGridView.SelectedRows;
            if (selected.Count > 0)
            {
                index = fDataGridView.Rows.IndexOf(selected[0]);
            }
            else
                index = -1;
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            TextBoxWriter writer = new TextBoxWriter(richTextBoxLogs);
            Console.SetOut(writer);
        }

        public void GetRecourceIndex(ref int index)
        {
            var selected = rDataGridView.SelectedRows;
            if (selected.Count > 0)
            {
                index = rDataGridView.Rows.IndexOf(selected[0]);
            }
            else
                index = -1;
        }

        public void LoadResources(IList<Resource> recources)
        {
            for (int i = 0; i < rDataGridView.Rows.Count - 1; i++)
            {
                recources.Add(new Resource((string)rDataGridView.Rows[i].Cells[0].Value, (string)rDataGridView.Rows[i].Cells[1].Value, DateTime.Parse(rDataGridView.Rows[i].Cells[2].Value.ToString())));
            }
        }

        public void UpdateResources(IList<Resource> recources)
        {
            rDataGridView.Rows.Clear();
            foreach (var res in recources)
                rDataGridView.Rows.Add(res.Address, res.Type, res.Date);
        }
    }
}

