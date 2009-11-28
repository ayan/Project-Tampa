using System;
using System.Windows.Forms;
using Tampa.Controls.WinForms;

namespace Tampa.Controls
{
    class ButtonControl: BaseControl
    {
        public ButtonControl():
            base()
        {
        }

        #region IControl Members

        public override string Icon
        {
            get { return "Button"; }
        }

        public override Type AssociatedUserControlType
        {
            get { return typeof(SelectableButton); }
        }

        #endregion

    }
}
