using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa.UI
{
    public partial class Canvas : Form
    {
        public Canvas(ICanvasController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        private ICanvasController _controller;
    }
}
