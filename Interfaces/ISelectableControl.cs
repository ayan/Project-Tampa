﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tampa.Interfaces
{
    interface ISelectableControl
    {
        ControlInstance Control { get; set; }
        bool IsSelected { get; set; }
        void Unselect();
    }
}
