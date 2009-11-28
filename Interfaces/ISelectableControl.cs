using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tampa.Interfaces
{
    interface ISelectableControl
    {
        bool IsSelected { get; set; }
        void Unselect();
    }
}
