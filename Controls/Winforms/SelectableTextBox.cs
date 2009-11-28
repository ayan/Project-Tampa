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
    class SelectableTextBox: TextBox, ISelectableControl
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public bool IsSelected
        {
            get;
            set;
        }

        public void Unselect()
        {
            this.IsSelected = false;
            this.Cursor = this.DefaultCursor;
            this.Parent.Refresh();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0085://WM_NCPAINT
                    if (IsSelected)
                    {
                        IntPtr hDC = GetWindowDC(this.Handle);
                        Graphics g = Graphics.FromHdc(hDC);

                        SelectableControlHelper.Select(this, g);


                        ReleaseDC(this.Handle, hDC);//another win32 API
                        g.Dispose();
                    }
                    break;

                default:
                    base.WndProc(ref m);
                    break;
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
