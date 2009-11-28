using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Interfaces;
using System.Diagnostics;

namespace Tampa.Controls.WinForms
{
    /// <summary>
    /// C# is lame- this really should have been a generic class so that
    /// we didnt have to duplicate this everywhere but you cant derive
    /// from generic parameters
    /// </summary>
    partial class SelectableButton: Button, ISelectableControl
    {
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

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            //if (IsSelected)
            //{
            //    SelectableControlHelper.Select(this, pevent);
            //}
        }
        public ControlInstance Control { get; set; }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);

            //if (IsSelected)
            //{
            //    SelectableControlHelper.MouseMove(this, mevent);
            //}
        }
    }
}
