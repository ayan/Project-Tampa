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
    public enum GrabHandles
    {
        TopLeft,
        TopMiddle,
        TopRight,
        Right,
        BottomRight,
        BottomMiddle,
        BottomLeft,
        Left,
        None
    };

    class SelectableControlHelper
    {
        private static Rectangle[] GetCornersForControl(Rectangle c)
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
            Select(c.ClientRectangle, g);
        }

        public static void Select(Rectangle c, Graphics g)
        {
//            Debug.WriteLine(String.Format("selection overlay bounds: {0}", c));
            Rectangle[] corners = GetCornersForControl(c);
            foreach (Rectangle corner in corners)
            {
                corner.Offset(c.Left, c.Top);
//                Debug.WriteLine(String.Format("Grab Handle: {0}", corner));
                ControlPaint.DrawGrabHandle(g, corner, true, true);
            }

            ControlPaint.DrawFocusRectangle(g, c);
        }

        public static void Select(Control c, PaintEventArgs p)
        {
            Select(c, p.Graphics);
        }

        public static GrabHandles OnMouseMove(Rectangle c, Point p)
        {
            Rectangle[] corners = GetCornersForControl(c);

            int i = 0;
            for (; i < corners.Length; i++)
            {
                corners[i].Offset(c.Left, c.Top);
                //Debug.WriteLine(String.Format("Checking if {0} is in {1}", p, corners[i]));
                if (corners[i].Contains(p))
                {
                    break;
                }
            }
            
            return (GrabHandles) i;
        }

        public const int GrabHandleLength = 5;
        static readonly Size _grabHandleSize = new Size(GrabHandleLength, GrabHandleLength);
        public static readonly Cursor[] CursorsForCorners = new Cursor[]
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
