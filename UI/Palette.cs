using System.Windows.Forms;
using Tampa.Interfaces;
using System.Drawing;

namespace Tampa.UI
{
    public partial class Palette : Form
    {
        public Palette(IPaletteController controller)
        {
            _controller = controller;

            Rectangle rect = Screen.FromHandle(this.Handle).Bounds;
            this.Left = rect.Left + 20; // Add 20 px padding on the left

            InitializeComponent();
        }

        IPaletteController _controller;
    }
}
