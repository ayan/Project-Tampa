using System.Windows.Forms;
using Tampa.Interfaces;
using System;
using Tampa.UI.Models;
using Tampa.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace Tampa.UI
{
    public partial class Canvas : Form, ITampaControl
    {
        public string GetUniqueName() { return "Canvas"; }

        public int ZIndex { get; set; }

        public Canvas(ICanvasController controller)
        {
            _controller = controller;
            InitializeComponent();
            this.AllowDrop = true;
            this.Control = new ControlInstance(this);
            this.ControlInstances = new List<ControlInstance>();
        }

        public void AddControl(IControl control, int x, int y)
        {
            Point p = PointToClient(new Point { X = x, Y = y });
            Dictionary<string, object> props = new Dictionary<string, object>();
            props[CommonProperties.Left] = p.X;
            props[CommonProperties.Top] = p.Y;
            props[CommonProperties.Text] = control.Icon + (this.Controls.Count + 1);

            AddControl(control, props);
        }

        public void AddControl(IControl control, Dictionary<string, object> properties)
        {
            ControlInstance instance = control.InstantiateUserControl();

            foreach (string prop in properties.Keys)
            {
                instance.Properties[prop] = properties[prop];
            }

            instance.OnClick += delegate(object o, EventArgs e) { _controller.ControlSelected(instance, o, e); };
            instance.Update();

            this.ControlInstances.Add(instance);
            this.Controls.Add(instance.UnderlyingControl);
            _controller.ControlSelected(instance, null, null);
        }

        public void RemoveControl(ControlInstance instance)
        {
            ControlInstances.Remove(instance);
            this.Controls.Remove(instance.UnderlyingControl);
        }

        public List<ControlInstance> ControlInstances { get; private set;  }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            //e.Cancel = TampaController.GetInstance().Quit();
        }

        private ICanvasController _controller;

        internal ControlInstance GetControlAtPoint(Point screenLocation)
        {
            Debug.WriteLine(String.Format("Getting control at {0}", screenLocation)); 
            Point p = this.PointToClient(screenLocation);
            Debug.WriteLine(String.Format("Getting child control at {0}", p));
            Control c = this.GetChildAtPoint(p, GetChildAtPointSkip.Invisible | GetChildAtPointSkip.Transparent);

            if (c is ISelectableControl)
            {
                Debug.WriteLine("Found!");
                return ((c as ISelectableControl).Control);
            }

            return this.Control;
        }

        #region ISelectableControl Members

        public ControlInstance Control
        {
            get;
            set;
        }

        public bool IsSelected
        {
            get;
            set;
        }

        public void Unselect()
        {
            throw new NotImplementedException();
        }

        #endregion

        public void Serialize(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("Canvas");

            Control.Slurp();
            // Weird slapdash hybrid because we dont support arbitrarily nested controls
            foreach (string property in Control.Properties.Keys)
            {
                if (Control.Properties[property] != null)
                {
                    writer.WriteAttributeString(property, Control.Properties[property].ToString());
                }
            }

            foreach (ControlInstance instance in this.ControlInstances)
            {
                instance.Serialize(writer);
            }

            writer.WriteEndElement();
        }

        public string OnClickHandler
        {
            get;
            set;
        }

        public string OnKeyPressHandler
        {
            get;
            set;
        }

        public string OnMouseMoveHandler
        {
            get;
            set;
        }

        public string OnMouseUpHandler
        {
            get;
            set;
        }

        public string OnMouseDownHandler
        {
            get;
            set;
        }
    }
}
