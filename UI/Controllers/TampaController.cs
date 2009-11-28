using System.Windows.Forms;
using Tampa.Interfaces;
using Tampa.UI.Controllers;
using System.Threading;

namespace Tampa.UI
{
    /// <summary>
    /// Controller for the tampa application
    /// </summary>
    class TampaController: ITampaController
    {
        #region ITampaController Members

        private static ITampaController _instance;
        public static ITampaController GetInstance()
        {
            return _instance ?? (_instance = new TampaController());
        }

        /// <summary>
        /// Constructor for the tampa controller
        /// </summary>
        public TampaController()
        {            
            // Enable theming for the application
            Application.EnableVisualStyles();

            // Use GDI to draw the text instead of GDI+- makes the text look better
            Application.SetCompatibleTextRenderingDefault(false);

            // Create the main view and the child window controllers
            _tampaMainWindow = new TampaWindow(this as ITampaController);
            _paletteController = new PaletteController();
            _canvasController = new CanvasController();
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        public void Quit()
        {
            Application.Exit();
        }

        /// <summary>
        /// Run the application
        /// </summary>
        public void Run()
        {
            Application.Run(_tampaMainWindow);
        }

        /// <summary>
        /// Called when the tampa window is shown- in this case
        /// tell the child controllers to reveal their UI
        /// </summary>
        public void OnMainWindowShown()
        {
            _paletteController.Show(_tampaMainWindow);
            _canvasController.Show(_tampaMainWindow);

        }

        /// <summary>
        /// Handles a request to add a control
        /// </summary>
        /// <param name="controlTypeToAdd">The control to add</param>
        public void HandleAddControlRequest(IControl controlTypeToAdd)
        {
            _canvasController.AddControl(controlTypeToAdd);
        }

        #endregion

        private TampaWindow _tampaMainWindow;
        private IPaletteController _paletteController;
        private ICanvasController _canvasController;
    }
}
