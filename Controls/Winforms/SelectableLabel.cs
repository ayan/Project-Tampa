using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa.Controls.WinForms
{
    class SelectableLabel: Label, ITampaControl
    {
        public ControlInstance Control { get; set; }

        private static int i = 0;
        public string GetUniqueName()
        {
            return ("Label " + i++);
        }

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

        public string OnClickHandler
        {
            get;
            set;
        }

        public string OnKeyPressHandler
        {
            get;
            set;
        }

        public string OnMouseMoveHandler
        {
            get;
            set;
        }

        public string OnMouseUpHandler
        {
            get;
            set;
        }

        public string OnMouseDownHandler
        {
            get;
            set;
        }

        public int ZIndex
        {
            get;
            set;
        }
    }
}
