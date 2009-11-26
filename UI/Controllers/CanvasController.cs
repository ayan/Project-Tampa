using System;
using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa.UI.Controllers
{
    class CanvasController: ICanvasController
    {
        public CanvasController()
        {
            _canvasView = new Canvas(this as ICanvasController);
        }

        #region ITampaChildWindowController Members

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Show(IWin32Window tampaWindow)
        {
            _canvasView.Show(tampaWindow);
        }

        #endregion

        private Canvas _canvasView;
    }
}
