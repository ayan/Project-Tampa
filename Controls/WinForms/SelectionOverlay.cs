using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Interfaces;
using System.Diagnostics;
using Tampa.UI.Models;
using Tampa.UI;
using Tampa.Controls.WinForms;
using Tampa.Common;

namespace Tampa.Controls.WinForms
{
    public partial class SelectionOverlay : Control
    {
        Form _parent;

        public ControlInstance SelectedControl
        {
            get;
            set;
        }

        private void TakeUpAllArea()
        {
            Rectangle mdiClientArea = Rectangle.Empty;
            foreach (Control c in _parent.Controls)
            {
                if (c is MdiClient)
                    mdiClientArea = c.ClientRectangle;
            }
            this.Bounds = mdiClientArea;
        } 

        public SelectionOverlay(Form parent)
        {
            _parent = parent;
            this.AllowDrop = true;
            Reset();
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(SelectionOverlay_KeyUp);
            this.MouseMove += new MouseEventHandler(SelectionOverlay_MouseMove);
            this.MouseUp += new MouseEventHandler(SelectionOverlay_MouseUp);
            this.MouseDown += new MouseEventHandler(SelectionOverlay_MouseDown);
            this.MouseDoubleClick += new MouseEventHandler(SelectionOverlay_MouseDoubleClick);
            this.DragDrop += new DragEventHandler(Canvas_DragDrop);
            this.DragOver += new DragEventHandler(Canvas_DragOver);
        }

        public void Reset()
        {
            this.GrabHandleBeingMoved = GrabHandles.None;
            this.SelectedControl = null;
            this.SelectionBounds = Rectangle.Empty;
            this.SelectedCenter = Point.Empty;
        }

        void SelectionOverlay_KeyUp(object sender, KeyEventArgs e)
        {
            if (SelectedControl == null) return;

            if (e.KeyCode == Keys.Delete)
            {
                TampaController.GetInstance().RequestRemoveControl(this.SelectedControl);
                Reset();
            }
        }

        void SelectionOverlay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowProperties();
        }

        void SelectionOverlay_MouseDown(object sender, MouseEventArgs mevent)
        {
            if (SelectedControl == null) return;

            trace("Location: " + mevent.Location);
            trace("Client loc: " + this.PointToClient(mevent.Location));
            trace("Bounds: " + SelectionBounds);
            trace("In Bounds: " + SelectionBounds.Contains(mevent.Location));
            trace("In ClientBounds: " + SelectionBounds.Contains(this.PointToClient(mevent.Location)));
        }

        void SelectionOverlay_MouseUp(object sender, MouseEventArgs e)
        {
            if (GrabHandleBeingMoved != GrabHandles.None)
            {
                MoveGrabHandle(GrabHandleBeingMoved, e.Location);
                Debug.WriteLine("SelectionBounds is " + this.SelectionBounds);
                Rectangle screenRect = _parent.RectangleToScreen(this.SelectionBounds);
                this.SelectedControl.UpdateProperties(screenRect.X + SelectableControlHelper.GrabHandleLength,
                    screenRect.Y + SelectableControlHelper.GrabHandleLength,
                    SelectionBounds.Width - SelectableControlHelper.GrabHandleLength * 2,
                    SelectionBounds.Height - SelectableControlHelper.GrabHandleLength * 2);

                this.Invalidate();
                this.GrabHandleBeingMoved = GrabHandles.None;
            }
            else if (NewSelectionBounds != null)
            {
                this.SelectionBounds = this.NewSelectionBounds.Value;
                this.SelectionBounds.Inflate(SelectableControlHelper.GrabHandleLength, SelectableControlHelper.GrabHandleLength);
                Rectangle screenRect = _parent.RectangleToScreen(this.SelectionBounds);
                this.SelectedControl.UpdateProperties(screenRect.X + SelectableControlHelper.GrabHandleLength,
                    screenRect.Y + SelectableControlHelper.GrabHandleLength,
                    SelectionBounds.Width - SelectableControlHelper.GrabHandleLength * 2,
                    SelectionBounds.Height - SelectableControlHelper.GrabHandleLength * 2);
                this.SelectedCenter = new Point(SelectionBounds.Left + (SelectionBounds.Width / 2), SelectionBounds.Top + (SelectionBounds.Height + 2));
                this.NewSelectionBounds = null;
                this.Invalidate();
            }
            else
            {
                TampaController.GetInstance().SelectControlAt(this.PointToScreen(e.Location));
                if (e.Button == MouseButtons.Right)
                {
                    TampaController.GetInstance().ShowContextMenu(this.PointToScreen(e.Location));
                }
            }
        }

        GrabHandles GrabHandleBeingMoved { get; set; }

        void MoveGrabHandle(GrabHandles g, Point p)
        {
            trace("Grab g picked: " + g);

            Rectangle modifiedRect = this.SelectionBounds;

            if (GrabHandleBeingMoved == GrabHandles.None)
                trace(string.Format("Starting with {0}", modifiedRect));

            if (g == GrabHandles.TopLeft || g == GrabHandles.TopMiddle || g == GrabHandles.TopRight)
            {
                trace("MoveY: " + p.Y);
                modifiedRect.Height += (modifiedRect.Y - p.Y);
                modifiedRect.Y = p.Y;
            }
            if (g == GrabHandles.TopLeft || g == GrabHandles.BottomLeft || g == GrabHandles.Left)
            {
                trace("MoveX: " + p.X);
                modifiedRect.Width += (modifiedRect.X - p.X);
                modifiedRect.X = p.X;
            }
            if (g == GrabHandles.Right || g == GrabHandles.TopRight || g == GrabHandles.BottomRight)
            {
                trace(string.Format("Fattening: {0}, {1}", p.X, modifiedRect.X));
                modifiedRect.Width = p.X - modifiedRect.X;
            }
            if (g == GrabHandles.BottomLeft || g == GrabHandles.BottomMiddle || g == GrabHandles.BottomRight)
            {
                trace(string.Format("Growing: {0}, {1}", p.Y, modifiedRect.Y));
                modifiedRect.Height = p.Y - modifiedRect.Y;
            }

            trace(String.Format("Modified rect: {0}", modifiedRect));
            this.SelectionBounds = modifiedRect;

        }

        void SelectionOverlay_MouseMove(object sender, MouseEventArgs mevent)
        {
            if (SelectedControl == null) return;

            //Debug.WriteLine(String.Format("Selection bounds is {0}", SelectionBounds));
            GrabHandles handle = SelectableControlHelper.OnMouseMove(SelectionBounds, mevent.Location);

            this.Cursor = SelectableControlHelper.CursorsForCorners[(int)handle];
            if (mevent.Button == MouseButtons.Left &&
                SelectionBounds != null)
            {
                if (handle != GrabHandles.None)
                {
                    trace("Resizing");
                    MoveGrabHandle(handle, mevent.Location);
                    GrabHandleBeingMoved = handle;
                }
                else if (SelectionBounds.Contains(mevent.Location) || this.NewSelectionBounds != null)
                {
                    if (NewSelectionBounds == null)
                    {
                        SelectedCenter = new Point(mevent.Location.X - SelectionBounds.Left, mevent.Location.Y - SelectionBounds.Top);
                    }

                    Point newCenter = new Point(mevent.Location.X - SelectedCenter.X, mevent.Location.Y - SelectedCenter.Y);

                    trace("New center is " + newCenter);
                    NewSelectionBounds = new Rectangle(newCenter, this.SelectionBounds.Size);
                    trace("Bounds were " + NewSelectionBounds);
                    this.GrabHandleBeingMoved = GrabHandles.None;
                }
                else
                {
                    trace("Location: " + mevent.Location);
                    trace("Client loc: " + this.PointToClient(mevent.Location));
                    trace("Bounds: " + SelectionBounds);
                }
            }
        }

        void trace(string s) { 
            Debug.WriteLine(s); 
        }

        void Canvas_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                PaletteButton button = (PaletteButton)e.Data.GetData(typeof(PaletteButton));
                
                TampaController.GetInstance().HandleAddControlRequest(button.AssociatedControl, e.X, e.Y);
            }
            catch (Exception)
            {
                // Ignore random drops
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            TakeUpAllArea();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;

                p.ExStyle |= 0x20;
                return p;
            }
        }

        public void SetSelectedControl(ControlInstance ci)
        {
            if (SelectedControl != null)
            {
                SelectedControl.ParentControl.Refresh();
            }

            SelectedControl = ci;

            if (ci != null)
            {
                Control c = ci.UnderlyingControl;
                Rectangle controlRectInScreenSpace = c.RectangleToScreen(c.ClientRectangle);

                // Special case forms- in that case, use the bounds instead
                // of the client rectangle
                if (c is Form || c is TextBox)
                {
                    controlRectInScreenSpace = c.Parent.RectangleToScreen(c.Bounds);
                }

                Rectangle r = _parent.RectangleToClient(controlRectInScreenSpace);

                r.Inflate(SelectableControlHelper.GrabHandleLength, SelectableControlHelper.GrabHandleLength);

                this.SelectionBounds = r;
            }

            this.Refresh();
        }

        public Rectangle SelectionBounds
        {
            get;
            set;
        }

        public Point SelectedCenter
        {
            get;
            set;
        }

        public Rectangle? NewSelectionBounds
        {
            get;
            set;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (SelectionBounds != null)
            {
                SelectableControlHelper.Select(this.SelectionBounds, e.Graphics);
            }
        }

        public override void Refresh()
        {
            TakeUpAllArea();
            base.Refresh();
        }

        private void InvalidateEx()
        {
            Invalidate();
            //let parent redraw the background

            if (Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            Parent.Invalidate(rc, true);
            //move and refresh the controls here

        }

        internal void SendControlBehind()
        {
            if (SelectedControl == null) return;

            SelectedControl.UnderlyingControl.SendToBack();
            (SelectedControl.UnderlyingControl as ISelectableControl).ZIndex--;
        }

        internal void BringControlInFront()
        {
            if (SelectedControl == null) return;

            SelectedControl.UnderlyingControl.BringToFront();
            (SelectedControl.UnderlyingControl as ISelectableControl).ZIndex++;
        }

        internal void ShowProperties()
        {
            if (SelectedControl == null) return;

            TampaController.GetInstance().HandleEditControlRequest(this.SelectedControl);
        }
    }
}
