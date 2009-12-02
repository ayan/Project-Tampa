using System.Windows.Forms;
using Tampa.Interfaces;
using Tampa.UI.Controllers;
using System.Threading;
using Tampa.Controls.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Text;
using Tampa.Common;
using Tampa.InterfaceCompiler;

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
            _paletteController = new PaletteController((ToolStrip) _tampaMainWindow.Controls["toolStrip1"]);
            _canvasController = new CanvasController();
            _propertyDialogController = new PropertyDialogController();
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        public bool Quit()
        {
            Application.Exit();
            return false;
        }

        public void CloseCanvas()
        {
            _canvasController.Close();
            _tampaMainWindow.CanvasClosed();
            _canvasController = null;
        }

        public void NewCanvas(string fileName)
        {
            CloseCanvas();
            _canvasController = new CanvasController();

            if (fileName != null)
            {
                XmlDocument document = new XmlDocument();
                document.Load(fileName);
                _canvasController.SetCanvasXml(document);
            }

            _canvasController.Show(_tampaMainWindow);
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
            int x = (new Random()).Next(800);
            int y = (new Random()).Next(600);
            _canvasController.AddControl(controlTypeToAdd, x, y);
        }

        /// <summary>
        /// Handles a request to add a control
        /// </summary>
        /// <param name="controlTypeToAdd">The control to add</param>
        public void HandleAddControlRequest(IControl controlTypeToAdd, int x, int y)
        {
            _canvasController.AddControl(controlTypeToAdd, x, y);
        }

        public void ShowContextMenu(Point p)
        {
            _tampaMainWindow.ContextMenuStrip.Show(p);
        }

        public void HandleEditControlRequest(ControlInstance instance)
        {
            _propertyDialogController.ShowControlProperties(_tampaMainWindow, instance);
        }

        public void SetSelectedControl(ControlInstance control)
        {
            SelectionOverlay overlay = (SelectionOverlay)_tampaMainWindow.Controls["selectionOverlay"];

            overlay.SetSelectedControl(control);
        }

        public void SelectControlAt(Point screenLocation)
        {
            SelectionOverlay overlay = (SelectionOverlay)_tampaMainWindow.Controls["selectionOverlay"];

            overlay.SetSelectedControl(_canvasController.GetControlFrom(screenLocation));
        }

        public void RequestRemoveControl(ControlInstance instance)
        {
            _canvasController.RemoveControl(instance);
        }

        public void SaveFile(string fileName)
        {
            using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.Unicode))
                {
                    writer.Write(_canvasController.GetCanvasXml());
                }
            }
        }

        public void Compile(string fileName, CompileTargets target)
        {
            string source = _canvasController.GetCanvasXml();
            XmlDocument document = new XmlDocument();

            document.LoadXml(source);

            ICompiler compiler = CompilerFactory.GetCompiler(target);
            string className = Path.GetFileNameWithoutExtension(fileName);
            string javaSource = compiler.Compile(document, className);
            
            using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate | FileMode.Truncate))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.Unicode))
                {
                    writer.Write(javaSource);
                }
            }
        }

        public Control GetView()
        {
            return _tampaMainWindow;
        }

        #endregion

        private TampaWindow _tampaMainWindow;
        private IPaletteController _paletteController;
        private ICanvasController _canvasController;
        private IPropertyDialogController _propertyDialogController;
    }
}
