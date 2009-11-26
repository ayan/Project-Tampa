using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tampa.Interfaces
{
    public interface ITampaController
    {
        void Run();
        void Quit();
        void OnMainWindowShown();
    }
}
