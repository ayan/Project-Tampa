﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Tampa.Interfaces;
using Tampa.Controls.WinForms;

namespace Tampa
{
    public partial class TampaWindow : Form
    {
        SelectionOverlay Overlay { get; set; }
        public TampaWindow(ITampaController controller)
        {
            _controller = controller;
            InitializeComponent();

            //this.Paint += new PaintEventHandler(TampaWindow_Paint);
            Overlay = new SelectionOverlay(this);
            this.Controls.Add(Overlay);
            Overlay.Refresh();
        }

        void TampaWindow_Paint(object sender, PaintEventArgs e)
        {
            Overlay.Invalidate();
            Overlay.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            _controller.Quit();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = _controller.Quit();
        }

        private void TampaWindow_Loaded(object sender, EventArgs e)
        {
            _controller.OnMainWindowShown();
            //Rectangle rect = Screen.FromHandle(this.Handle).Bounds;
            //this.Width = rect.Width;
            //this.Left = rect.Left;
            //this.Top = rect.Top;
        }

        private ITampaController _controller;

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                _controller.SaveFile(saveFileDialog1.FileName);
            }
        }

        internal void CanvasClosed()
        {
            this.Overlay.Reset();
            this.Invalidate(true);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.NewCanvas(null);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                _controller.NewCanvas(openFileDialog1.FileName);
            }
        }
    }
}
