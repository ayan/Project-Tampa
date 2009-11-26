using System.Windows.Forms;
using Tampa.Interfaces;
using Tampa.UI.Controllers;

namespace Tampa.UI
{
    class TampaController: ITampaController
    {
        #region ITampaController Members

        public TampaController()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _tampaMainWindow = new TampaWindow(this as ITampaController);
            _paletteController = new PaletteController();
            _canvasController = new CanvasController();
        }

        public void Quit()
        {
            Application.Exit();
        }

        public void Run()
        {
            Application.Run(_tampaMainWindow);
        }

        public void OnMainWindowShown()
        {
            _paletteController.Show(_tampaMainWindow);
            _canvasController.Show(_tampaMainWindow);

        }

        #endregion

        private TampaWindow _tampaMainWindow;
        private IPaletteController _paletteController;
        private ICanvasController _canvasController;
    }
}
