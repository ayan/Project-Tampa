using System;
using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa.UI.Controllers
{
    class PaletteController: IPaletteController
    {
        #region IPaletteController Members

        public PaletteController()
        {
            _paletteView = new Palette(this as IPaletteController);
        }

        public void Show(IWin32Window tampaWindow)
        {
            _paletteView.Show(tampaWindow);
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        #endregion

        private Palette _paletteView;
    }
}
