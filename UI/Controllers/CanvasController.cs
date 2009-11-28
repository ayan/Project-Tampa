using System;
using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa.UI.Controllers
{
    /// <summary>
    /// An implementation of the controller for the canvas
    /// </summary>
    class CanvasController: ICanvasController
    {
        /// <summary>
        /// The constructor for the controller
        /// </summary>
        public CanvasController()
        {
            _canvasView = new Canvas(this as ICanvasController);
        }

        #region ITampaChildWindowController Members

        /// <summary>
        /// Method to display the associated UI
        /// </summary>
        /// <param name="tampaWindow">The parent window for the view</param>
        public void Show(IWin32Window tampaWindow)
        {
            _canvasView.Show(tampaWindow);
        }

        public void AddControl(IControl controlTypeToAdd)
        {
            _canvasView.AddControl(controlTypeToAdd);
        }

        #endregion

        private Canvas _canvasView;
    }
}
