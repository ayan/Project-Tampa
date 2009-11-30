using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tampa.Interfaces
{
    /// <summary>
    /// This describes the controller for the Tampa application
    /// </summary>
    public interface ITampaController
    {
        /// <summary>
        /// Runs the tampa app
        /// </summary>
        void Run();

        /// <summary>
        /// Called when the App should quit
        /// </summary>
        bool Quit();

        /// <summary>
        /// Called when the main tampa window is shown
        /// </summary>
        void OnMainWindowShown();

        /// <summary>
        /// Close the current canvas
        /// </summary>
        void CloseCanvas();

        /// <summary>
        /// Creates a new Canvas
        /// </summary>
        void NewCanvas(string fileName);

        Control GetView();

        /// <summary>
        /// Handles a request to add a control
        /// </summary>
        /// <param name="controlTypeToAdd">The control to add</param>
        void HandleAddControlRequest(IControl controlTypeToAdd);

        void SetSelectedControl(ControlInstance control);

        void HandleAddControlRequest(IControl iControl, int p, int p_3);

        void HandleEditControlRequest(ControlInstance instance);

        void RequestRemoveControl(ControlInstance instance);

        void SelectControlAt(System.Drawing.Point point);

        void SaveFile(string fileName);
    }
}
