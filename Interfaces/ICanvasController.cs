
using System;
using System.Collections.Generic;
using System.Xml;
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

        void RemoveControl(ControlInstance instance);

        string GetCanvasXml();

        ControlInstance GetControlFrom(System.Drawing.Point screenLocation);

        void Close();

        void SetCanvasXml(XmlDocument canvasXml);
    }
}
