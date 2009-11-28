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
        public PaletteController(ToolStrip paletteView)
        {
            _model = new PaletteModel(this);
            _model.InitializeFromControlManager(ControlManagerFactory.GetControlManager());

            // View has to be initialized only after the model
            _paletteView = paletteView;
        }

        /// <summary>
        /// Shows the palette view
        /// </summary>
        /// <param name="tampaWindow">The parent window of the palette view</param>
        public void Show(IWin32Window tampaWindow)
        {
            _paletteView.Items.Clear();
            _paletteView.Items.Add(new ToolStripLabel { Text = "Controls" });
            _paletteView.Items.Add(new ToolStripSeparator());

            foreach (PaletteButton button in _model.PaletteButtons)
            {
                _paletteView.Items.Add(button.Button);
            }            
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
        private ToolStrip _paletteView;
    }
}
