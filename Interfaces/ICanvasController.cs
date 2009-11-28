
using System;
namespace Tampa.Interfaces
{
    public interface ICanvasController: ITampaChildWindowController
    {
        /// <summary>
        /// Adds a control to the canvas
        /// </summary>
        /// <param name="controlTypeToAdd"></param>
        void AddControl(IControl controlTypeToAdd, int x, int y);

        /// <summary>
        /// Called when a control is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ControlSelected(ControlInstance control, object sender, EventArgs e);

        ControlInstance GetControlFrom(System.Drawing.Point screenLocation);
    }
}
