using System;
using System.Windows.Forms;
using Tampa.Interfaces;
using System.Diagnostics;
using System.Drawing;

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
            _canvasView.MdiParent = (tampaWindow as Form);
            _canvasView.Show();
        }

        public void AddControl(IControl controlTypeToAdd, int x, int y)
        {
            _canvasView.AddControl(controlTypeToAdd, x, y);
        }

        /// <summary>
        /// Called when a control is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlSelected(ControlInstance control, object sender, EventArgs e)
        {
            if (_lastSelectedControl != null)
            {
                _lastSelectedControl.IsSelected = false;
                _lastSelectedControl.UnderlyingControl.Refresh();
            }

            (TampaController.GetInstance()).SetSelectedControl(control);

            control.IsSelected = true;
            _lastSelectedControl = control;
            _lastSelectedControl.UnderlyingControl.Refresh();
        }

        #endregion

        // TEMPTEMP: Move to model
        private ControlInstance _lastSelectedControl;
        private Canvas _canvasView;

        public ControlInstance GetControlFrom(Point screenLocation)
        {
            return _canvasView.GetControlAtPoint(screenLocation);
        }
    }
}
