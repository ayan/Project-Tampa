using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tampa.Interfaces
{
    public interface ITampaChildWindowController
    {
        void Load();
        void Show(IWin32Window tampaWindow);
    }
}
