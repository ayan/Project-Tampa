using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa.UI
{
    public partial class Palette : Form
    {
        public Palette(IPaletteController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        IPaletteController _controller;
    }
}
