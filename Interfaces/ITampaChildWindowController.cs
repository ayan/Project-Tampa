using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tampa.Interfaces
{
    /// <summary>
    /// Interface f
    /// </summary>
    public interface ITampaChildWindowController
    {
        /// <summary>
        /// Shows the child window
        /// </summary>
        /// <param name="tampaWindow">Parent window</param>
        void Show(IWin32Window tampaWindow);
    }
}
