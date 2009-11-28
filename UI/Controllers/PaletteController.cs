using System;
using System.Windows.Forms;
using Tampa.Interfaces;
using Tampa.UI.Models;
using Tampa.Controls;

namespace Tampa.UI.Controllers
{
    /// <summary>
    /// Implementation of the palette controller
    /// </summary>
    class PaletteController: IPaletteController
    {
        #region IPaletteController Members

        /// <summary>
        /// Constructor for the Palette controller
        /// </summary>
        public PaletteController()
        {
            _model = new PaletteModel(this);
            _model.InitializeFromControlManager(ControlManagerFactory.GetControlManager());

            // View has to be initialized only after the model
            _paletteView = new Palette(this as IPaletteController);
        }

        /// <summary>
        /// Shows the palette view
        /// </summary>
        /// <param name="tampaWindow">The parent window of the palette view</param>
        public void Show(IWin32Window tampaWindow)
        {
            _paletteView.Show(tampaWindow);
        }

        public PaletteModel GetModel()
        {
            return _model;
        }

        public void RequestAddControl(IControl control)
        {
            TampaController.GetInstance().HandleAddControlRequest(control);            
        }

        #endregion

        private PaletteModel _model;
        private Palette _paletteView;
    }
}
