using Logic.models;
using SysProg.views;
using System;
using System.Windows.Forms;

namespace SysProg
{
    public partial class FileInputForm : Form, IFillView<File>
    {
        public FileInputForm()
        {
            InitializeComponent();

            submitButton.Click += (sender, args) => { if (Submit != null) Submit(); };
        }

        public event Action Submit;

        public void GetData(File file)
        {
            file.Name = fileNameBox.Text;
            file.Version = fileVersionBox.Text;
            file.Date = fileDatePicker.Value;
        }

        public void SetData(File file)
        {
            fileNameBox.Text = file.Name;
            fileVersionBox.Text = file.Version;
            fileDatePicker.Value = file.Date;
        }

        public void SetError(string error)
        {
            errorLabel.Text = error;
        }

        public new void Show()
        {
            base.Show();
        }
    }
}
