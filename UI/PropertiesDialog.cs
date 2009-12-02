using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa.UI
{
    public partial class PropertiesDialog : Form
    {
        public PropertiesDialog(IPropertyDialogController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        private void PropertiesDialog_Load(object sender, EventArgs e)
        {

        }

        public void SetControl(ControlInstance instance)
        {
            _instance = instance;

            if (this.propertyGrid.SelectedObject != instance.UnderlyingControl)
            {
                this.propertyGrid.SelectedObject = instance.UnderlyingControl;
            }
        }

        IPropertyDialogController _controller;

        private void btnOK_Click(object sender, EventArgs e)
        {
            _controller.UpdateProperties(_instance);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _controller.RevertProperties(_instance);
        }

        ControlInstance _instance;
    }
}
