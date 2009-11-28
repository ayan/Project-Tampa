using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa.Controls.WinForms
{
    class SelectableLabel: Label, ISelectableControl
    {
        public ControlInstance Control { get; set; }

        public bool IsSelected
        {
            get;
            set;
        }

        public void Unselect()
        {
            this.IsSelected = false;
            this.Cursor = this.DefaultCursor;
        }
    }
}
