using System;
using System.Windows.Forms;
using Tampa.Interfaces;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Xml;
using Tampa.Controls;
using System.Collections.Generic;

namespace Tampa.UI.Controllers
{
    /// <summary>
    /// An implementation of the controller for the canvas
    /// </summary>
    class CanvasController: ICanvasController
    {
        /// <summary>
        /// The constructor for the controller
        /// </summary>
        public CanvasController()
        {
            _canvasView = new Canvas(this as ICanvasController);
        }

        #region ITampaChildWindowController Members

        /// <summary>
        /// Method to display the associated UI
        /// </summary>
        /// <param name="tampaWindow">The parent window for the view</param>
        public void Show(IWin32Window tampaWindow)
        {
            _canvasView.MdiParent = (tampaWindow as Form);
            _canvasView.Show();
            TampaController.GetInstance().SetSelectedControl(_canvasView.Control);
        }

        public void AddControl(IControl controlTypeToAdd, int x, int y)
        {
            _canvasView.AddControl(controlTypeToAdd, x, y);
        }

        /// <summary>
        /// Called when a control is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlSelected(ControlInstance control, object sender, EventArgs e)
        {
            if (_lastSelectedControl != null)
            {
                _lastSelectedControl.IsSelected = false;
                _lastSelectedControl.UnderlyingControl.Refresh();
            }

            (TampaController.GetInstance()).SetSelectedControl(control);

            control.IsSelected = true;
            _lastSelectedControl = control;
            _lastSelectedControl.UnderlyingControl.Refresh();
        }

        public void RemoveControl(ControlInstance control)
        {
            _canvasView.RemoveControl(control);
        }

        public string GetCanvasXml()
        {
            StringBuilder sb = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;

#if DEBUG
            settings.Indent = true;
#endif

            using (XmlWriter xmlWriter = XmlWriter.Create(sb, settings))
            {
                _canvasView.Serialize(xmlWriter);
            }

            return sb.ToString();
        }

        #endregion

        // TEMPTEMP: Move to model
        private ControlInstance _lastSelectedControl;
        private Canvas _canvasView;

        public ControlInstance GetControlFrom(Point screenLocation)
        {
            return _canvasView.GetControlAtPoint(screenLocation);
        }

        #region ICanvasController Members


        public void Close()
        {
            _canvasView.Close();
        }

        public void SetCanvasXml(XmlDocument document)
        {
            XmlNode node = document.SelectSingleNode("/Canvas");

            if (node == null)
            {
                return;
            }

            foreach (XmlAttribute attr in node.Attributes)
            {
                _canvasView.Control.Properties[attr.LocalName] = attr.Value;
            }

            foreach (XmlNode n in node.ChildNodes)
            {
                ControlManager manager = ControlManagerFactory.GetControlManager();
                IControl control = manager.GetControlForType(n.Name);

                Dictionary<string, object> properties = new Dictionary<string, object>();
                
                foreach (XmlAttribute attr in n.Attributes)
                {
                    properties[attr.LocalName] = attr.Value;
                }

                _canvasView.AddControl(control, properties);
            }

            _canvasView.Control.Update();
        }

        #endregion
    }
}
