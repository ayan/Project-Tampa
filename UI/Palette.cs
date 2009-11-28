using System.Windows.Forms;
using Tampa.Interfaces;
using System.Drawing;
using Tampa.UI.Models;

namespace Tampa.UI
{
    /// <summary>
    /// The Palette window
    /// </summary>
    public partial class Palette : Form
    {
        /// <summary>
        /// Constructor for the palette class
        /// </summary>
        /// <param name="controller">The controller that should handle user events</param>
        public Palette(IPaletteController controller)
        {
            _controller = controller;

            InitializeComponent();

            Rectangle rect = Screen.FromHandle(this.Handle).Bounds;
            this.Left = rect.Left + 20; // Add 20 px padding on the left

            PopulateButtons();
        }

        private void PopulateButtons()
        {
            PaletteModel model = _controller.GetModel();

            this.flowLayoutPanel1.Width = model.PaletteBounds.Width + this.flowLayoutPanel1.Margin.Left + this.flowLayoutPanel1.Margin.Right;
            this.flowLayoutPanel1.Height = model.PaletteBounds.Height;

            this.Width = this.flowLayoutPanel1.Width + 4;

            foreach (PaletteButton button in model.PaletteButtons)
            {
                this.flowLayoutPanel1.Controls.Add(button.Button);
            }
        }

        IPaletteController _controller;
    }
}
