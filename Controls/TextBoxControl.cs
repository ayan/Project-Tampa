using System;
using System.Windows.Forms;

namespace Tampa.Controls
{
    class TextBoxControl : BaseControl
    {
        public TextBoxControl() :
            base()
        {
        }

        #region IControl Members

        public override string Icon
        {
            // Temptemp: return a hardcoded string instead of the icon
            // PaletteButton just displays the text instead of the icon
            // need to fix both places
            get { return "Text Box"; }
        }

        public override Type AssociatedUserControlType
        {
            get { return typeof(TextBox); }
        }

        #endregion
    }
}
