using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tampa.Interfaces;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Tampa.Controls.WinForms
{
    class SelectableControlHelper
    {
        private static Rectangle[] GetCornersForControl(Control c)
        {
            return new Rectangle[] 
            {
                // Top Row
                new Rectangle(new Point(0, 0), _grabHandleSize),
                new Rectangle(new Point((c.Width - GrabHandleLength) / 2, 0), _grabHandleSize),
                new Rectangle(new Point(c.Width - GrabHandleLength, 0), _grabHandleSize),

                // Right
                new Rectangle(new Point((c.Width - GrabHandleLength), (c.Height - GrabHandleLength) / 2), _grabHandleSize),

                // Bottom row
                new Rectangle(new Point(c.Width - GrabHandleLength, c.Height - GrabHandleLength), _grabHandleSize),
                new Rectangle(new Point((c.Width - GrabHandleLength) / 2, (c.Height - GrabHandleLength)), _grabHandleSize),
                new Rectangle(new Point(0, c.Height - GrabHandleLength), _grabHandleSize),

                // Left
                new Rectangle(new Point(0, (c.Height - GrabHandleLength) / 2), _grabHandleSize)
            };
        }

        public static void Select(Control c, Graphics g)
        {
            Rectangle[] corners = GetCornersForControl(c);
            foreach (Rectangle corner in corners)
            {
                ControlPaint.DrawGrabHandle(g, corner, true, true);
            }
        }

        public static void Select(Control c, PaintEventArgs p)
        {
            Select(c, p.Graphics);
        }

        public static void MouseMove(Control c, MouseEventArgs e)
        {
            Rectangle[] corners = GetCornersForControl(c);

            int i = 0;
            for (; i < corners.Length; i++)
            {
                if (corners[i].Contains(e.Location))
                {
                    c.Cursor = _cursorsForCorners[i];
                    return;
                }
            }
            c.Cursor = _cursorsForCorners[i];
        }

        public const int GrabHandleLength = 5;
        static readonly Size _grabHandleSize = new Size(GrabHandleLength, GrabHandleLength);
        static readonly Cursor[] _cursorsForCorners = new Cursor[]
            {
                Cursors.SizeNWSE,
                Cursors.SizeNS,
                Cursors.SizeNESW,
                Cursors.SizeWE,
                Cursors.SizeNWSE,
                Cursors.SizeNS,
                Cursors.SizeNESW,
                Cursors.SizeWE,
                Cursors.Default
            };
    }
}
