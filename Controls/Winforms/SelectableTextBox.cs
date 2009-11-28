using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Interfaces;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Tampa.Controls.WinForms
{
    /// <summary>
    /// So text boxes are sneaky bastards
    /// The problem is this. A text box looks like this:
    /// 
    ///  --------------
    /// | foo          |
    ///  --------------
    ///  
    /// Now, the border for the textbox is in the non-client area of the 
    /// text boxes window. So you end up with all kinds of fun problems.
    /// First, your grab handles are inside the text box borders now, which 
    /// is no fun. Secondly, even if you were to paint the grab handles in
    /// the non-client area- you dont get the OnMouseMove event from those
    /// areas. So this weird bastardized grab handle clusterfuck emerges.
    /// To simplify things, we become sneaky too and just use a disguised
    /// selectableLabel as a text box
    /// </summary>
    class SelectableTextBox: TextBox, ISelectableControl
    {
        //public SelectableTextBox()
        //{
        //    this.BackColor = System.Drawing.SystemColors.Window;
        //    this.ForeColor = TextBox.DefaultForeColor;
        //    this.TextAlign = ContentAlignment.MiddleLeft;
        //    this.Padding = new Padding(5);
        //    // 10 px padding
        //    this.Height = (int) TextBox.DefaultFont.GetHeight() + 10;
        //}
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
