using System.Windows.Forms;
using Tampa.Interfaces;
using System;

namespace Tampa.UI
{
    public partial class Canvas : Form
    {
        public Canvas(ICanvasController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        public void AddControl(IControl control)
        {
            ControlInstance instance = control.InstantiateUserControl();

            // TEMPTEMP
            instance.UnderlyingControl.Text = control.Icon + (this.Controls.Count + 1);
            instance.UnderlyingControl.Left = (new Random()).Next(this.Width - instance.UnderlyingControl.Width);
            instance.UnderlyingControl.Top = (new Random()).Next(this.Height - -instance.UnderlyingControl.Height);
            instance.OnClick += delegate(object o, EventArgs e) { _controller.ControlSelected(instance, o, e); };

            _controller.ControlSelected(instance, null, null);
            this.Controls.Add(instance.UnderlyingControl);
        }

        private ICanvasController _controller;
    }
}
