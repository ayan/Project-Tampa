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

            if (IsSelected)
            {
                SelectableControlHelper.Select(this, pevent);
            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);

            if (IsSelected)
            {
                SelectableControlHelper.MouseMove(this, mevent);
            }
        }
    }
}
