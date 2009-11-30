using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tampa.Interfaces
{
    public interface IPropertyDialogController: ITampaChildWindowController
    {
        void ShowControlProperties(IWin32Window tampaWindow, ControlInstance instance);
        void UpdateProperties(ControlInstance instance);
        void RevertProperties(ControlInstance instance);
    }
}
