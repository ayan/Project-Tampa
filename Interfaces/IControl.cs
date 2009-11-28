using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace Tampa.Interfaces
{
    /// <summary>
    /// This interface is used to describe a control that can be hosted
    /// displayed in the Palette and hosted in the Canvas
    /// </summary>
    public interface IControl
    {
        /// <summary>
        /// Path to an icon that the palette displays
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// The User control associated with the control
        /// </summary>
        Type AssociatedUserControlType { get; }

        /// <summary>
        /// Instantiates the associated user control
        /// </summary>
        /// <returns>The user control that was returned</returns>
        ControlInstance InstantiateUserControl();
    }
}
