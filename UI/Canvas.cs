using System.Windows.Forms;
using Tampa.Interfaces;
using System;
using Tampa.UI.Models;
using Tampa.Common;
using System.Collections.Generic;
using System.Drawing;

namespace Tampa.UI
{
    public partial class Canvas : Form
    {
        public Canvas(ICanvasController controller)
        {
            _controller = controller;
            InitializeComponent();
            this.AllowDrop = true;
            this.DragDrop += new DragEventHandler(Canvas_DragDrop);
            this.DragOver += new DragEventHandler(Canvas_DragOver);
        }

        void Canvas_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                PaletteButton button = (PaletteButton)e.Data.GetData(typeof(PaletteButton));
                Point p = this.PointToClient(new Point(e.X, e.Y));
                AddControl(button.AssociatedControl, p.X, p.Y);
            }
            catch (Exception)
            {
                // Ignore random drops
            }
        }

        public void AddControl(IControl control)
        {
            int x = (new Random()).Next(this.Width);
            int y = (new Random()).Next(this.Height);

            AddControl(control, x, y);
        }


        public void AddControl(IControl control, int x, int y)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            props[CommonProperties.Left] = x;
            props[CommonProperties.Top] = y;
            props[CommonProperties.Text] = control.Icon + (this.Controls.Count + 1);

            AddControl(control, props);
        }

        public void AddControl(IControl control, Dictionary<string, object> properties)
        {
            ControlInstance instance = control.InstantiateUserControl();

            foreach (string prop in properties.Keys)
            {
                instance.Properties[prop] = properties[prop];
            }

            instance.OnClick += delegate(object o, EventArgs e) { _controller.ControlSelected(instance, o, e); };
            instance.Update();

            _controller.ControlSelected(instance, null, null);
            this.Controls.Add(instance.UnderlyingControl);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            //e.Cancel = TampaController.GetInstance().Quit();
        }

        private ICanvasController _controller;
    }
}
