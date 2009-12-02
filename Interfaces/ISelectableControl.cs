using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tampa.Interfaces
{
    interface ISelectableControl
    {
        string GetUniqueName();
        ControlInstance Control { get; set; }
        bool IsSelected { get; set; }
        void Unselect();
        int ZIndex { get; set; }
    }
}
