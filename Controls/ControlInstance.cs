using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Common;
using System.Drawing;
using System.Diagnostics;

namespace Tampa.Interfaces
{
    public class ControlInstance
    {
        /// <summary>
        /// Constructor for the control instance
        /// </summary>
        /// <param name="control"></param>
        public ControlInstance(IControl control)
        {
            this.UnderlyingControl = control.AssociatedUserControlType.GetConstructor(new Type[0]).Invoke(new object[0]) as Control;
            this.UnderlyingControl.Visible = true;
            (this.UnderlyingControl as ISelectableControl).Control = this;
            _props = new Dictionary<string, object>();
            //MakeControlEditable();
        }

        /// <summary>
        /// Constructor for the control instance
        /// </summary>
        /// <param name="control"></param>
        public ControlInstance(Control control)
        {
            this.UnderlyingControl = control;
            this.UnderlyingControl.Visible = true;
            (this.UnderlyingControl as ISelectableControl).Control = this;
            _props = new Dictionary<string, object>();
        }

        public EventHandler OnClick;
        public EventHandler OnMove;
        public EventHandler OnResize;

        public bool IsSelected
        {
            //get
            //{
            //    return (this.UnderlyingControl as ISelectableControl).IsSelected;
            //}
            //set
            //{
            //    (this.UnderlyingControl as ISelectableControl).IsSelected = value;

            //    // gah- ugly hack
            //    if (value == false)
            //    {
            //        (this.UnderlyingControl as ISelectableControl).Unselect();
            //    }
            //}
            get;
            set;
        }

        private void MakeControlEditable()
        {
            this.UnderlyingControl.Click += new EventHandler(UnderlyingControl_Click);
        }

        void UnderlyingControl_Click(object sender, EventArgs e)
        {
            OnClick.Invoke(sender, e);
        }

        /// <summary>
        /// Static constructor for the control instance to initialize the 
        /// property handlers
        /// </summary>
        static ControlInstance()
        {
            SetupPropertyHandlers();
        }

        /// <summary>
        /// Update the control based on the properties associated with it
        /// </summary>
        public virtual void Update()
        {
            // Go through the list of properties that have handlers associated with them
            foreach (string knownProperty in ControlInstance._propertyHandlers.Keys)
            {
                // If the property is something that we have, then run the handler
                // against the underlying control setting the property's value
                object val;
                if (this.Properties.TryGetValue(knownProperty, out val))
                {
                    ControlInstance._propertyHandlers[knownProperty](val, this.UnderlyingControl);
                }
            }

            this.UnderlyingControl.Refresh();
        }

        /// <summary>
        /// The underlying winforms control
        /// </summary>
        public Control UnderlyingControl
        {
            get;
            private set;
        }

        /// <summary>
        /// Properties associated with this instance
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get
            {
                return _props;
            }
        }

        /// <summary>
        /// Get the markup for this control instance
        /// </summary>
        /// <returns></returns>
        string GetMarkup()
        {
            // TEMPTEMP: Return the markup for the control here
            return "";
        }

        /// <summary>
        /// Simple static wrapper method which executes a function
        /// and returns true if the method succeeds and false if it
        /// throws
        /// </summary>
        /// <typeparam name="T">The return type of the method</typeparam>
        /// <param name="func">The method to execute</param>
        protected static bool Handle<T>(Func<T> func)
        {
            try
            {
                func();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Sets up the static property handler dictionary
        /// </summary>
        protected static void SetupPropertyHandlers()
        {
            _propertyHandlers = new Dictionary<string, Func<object, Control, bool>>()
            {
                { 
                    CommonProperties.Top, 
                    (o, c) =>
                    { 
                        return Handle<int>(() => c.Top = (int) o);
                    }
                },
                { 
                    CommonProperties.Left, 
                    (o, c) =>
                    { 
                        return Handle<int>(() => c.Left = (int) o);
                    }
                },
                { 
                    CommonProperties.Height, 
                    (o, c) =>
                    { 
                        return Handle<int>(() => c.Height = (int) o);
                    }
                },
                { 
                    CommonProperties.Width, 
                    (o, c) =>
                    { 
                        return Handle<int>(() => c.Width = (int) o);
                    }
                },
                { 
                    CommonProperties.Text, 
                    (o, c) =>
                    { 
                        return Handle<string>(() => c.Text = (string) o);
                    }
                }
            };
        }

        protected static Dictionary<string, Func<object, Control, bool>> _propertyHandlers;
        private Dictionary<string, object> _props;

        internal void UpdateProperties(int x, int y, int w, int h)
        {
            Debug.WriteLine(String.Format("Setting to {0}, {1}, {2}, {3}", x, y, w, h));
            Point clientPoint = UnderlyingControl.Parent.PointToClient(new Point { X = x, Y = y });
            this.Properties[CommonProperties.Left] = clientPoint.X;
            this.Properties[CommonProperties.Top] = clientPoint.Y;
            this.Properties[CommonProperties.Width] = w;
            this.Properties[CommonProperties.Height] = h;
            this.Update();
            this.UnderlyingControl.Parent.Invalidate();
        }
    }
}
