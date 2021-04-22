using Logic.models;
using SysProg.views;
using System;
using System.Windows.Forms;

namespace SysProg
{
    public partial class ResourceInputForm : Form, IFillView<Resource>
    {
        public ResourceInputForm()
        {
            InitializeComponent();
            submitButton.Click += (sender, args) => { if (Submit != null) Submit(); };
        }

        public event Action Submit;

        public void GetData(Resource resource)
        {
            resource.Address = resourceAddressBox.Text;
            resource.Type = resTypeComboBox.Text;
            resource.Date = resourceDatePicker.Value;
        }

        public void SetData(Resource resource)
        {
            resourceAddressBox.Text = resource.Address;
            resTypeComboBox.Text = resource.Type;
            resourceDatePicker.Value = resource.Date;
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
