using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa.UI.Models
{
    /// <summary>
    /// Model for a button that will be displayed in a palette
    /// </summary>
    public class PaletteButton
    {
        public PaletteButton(int width, int height, IControl control, IPaletteController controller)
        {
            this.Button = new ToolStripButton();
            this.Button.Click += new EventHandler(Button_DoubleClick);
            this.Button.MouseMove += new MouseEventHandler(Button_MouseMove);
            this.Button.Width = width;
            this.Button.Height = height;
            this.Button.Text = this.Icon = control.Icon;
            _controller = controller;
            this.AssociatedControl = control;
        }

        void Button_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Button.DoDragDrop(this, DragDropEffects.Copy);
            }
        }

        void Button_DoubleClick(object sender, EventArgs e)
        {
            _controller.RequestAddControl(this.AssociatedControl);
        }

        public ToolStripButton Button { get; private set; }
        public string Icon { get; private set; }
        public IControl AssociatedControl { get; private set; }

        private IPaletteController _controller;
    }
}
