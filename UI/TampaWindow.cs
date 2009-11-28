using System;
using System.Drawing;
using System.Windows.Forms;
using Tampa.Interfaces;

namespace Tampa
{
    public partial class TampaWindow : Form
    {
        public TampaWindow(ITampaController controller)
        {
            _controller = controller;
            InitializeComponent();
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
    }
}
