using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// Handles a request to add a control
        /// </summary>
        /// <param name="controlTypeToAdd">The control to add</param>
        void HandleAddControlRequest(IControl controlTypeToAdd);
    }
}
