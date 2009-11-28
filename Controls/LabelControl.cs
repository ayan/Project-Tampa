using System;
using System.Windows.Forms;
using Tampa.Controls.WinForms;

namespace Tampa.Controls
{
    class LabelControl : BaseControl
    {
        public LabelControl():
            base()
        {
        }

        #region IControl Members

        public override string Icon
        {
            get { return "Label"; }
        }

        public override Type AssociatedUserControlType
        {
            get { return typeof(SelectableLabel); }
        }

        #endregion
    }
}
