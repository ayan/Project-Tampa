using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Common;
using System.Drawing;
using System.Diagnostics;
using Tampa.UI;

namespace Tampa.Interfaces
{
    public class ControlInstance
    {
        protected class PropertyDealer
        {
            public Func<object, Control, object> PropertySetter { get; set; }
            public Func<Control, object> PropertyGetter { get; set; }
        }
            
        /// <summary>
        /// Constructor for the control instance
        /// </summary>
        /// <param name="control"></param>
        public ControlInstance(IControl control)
        {
            this.UnderlyingControl = control.AssociatedUserControlType.GetConstructor(new Type[0]).Invoke(new object[0]) as Control;
            this.UnderlyingControl.Name = (this.UnderlyingControl as ISelectableControl).GetUniqueName();
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
            this.UnderlyingControl.Name = (this.UnderlyingControl as ISelectableControl).GetUniqueName(); 
            this.UnderlyingControl.Visible = true;
            (this.UnderlyingControl as ISelectableControl).Control = this;
            _props = new Dictionary<string, object>();
        }

        public EventHandler OnClick;
        public EventHandler OnMove;
        public EventHandler OnResize;

        public bool IsSelected
        {
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

        public void Slurp()
        {
            // Go through the list of properties that have handlers associated with them
            foreach (string knownProperty in ControlInstance._propertyHandlers.Keys)
            {
                this.Properties[knownProperty] = ControlInstance._propertyHandlers[knownProperty].PropertyGetter(this.UnderlyingControl);
            }
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
                    ControlInstance._propertyHandlers[knownProperty].PropertySetter(val, this.UnderlyingControl);
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
            _propertyHandlers = new Dictionary<string, PropertyDealer>()
            {
                { 
                    CommonProperties.Top,
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<int>(() => c.Top = AsInt(o));
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return c.Top;
                        })
                    }
            },
                { 
                    CommonProperties.Left, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<int>(() => c.Left = AsInt(o));
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return c.Left;
                        })
                    }
                },
                { 
                    CommonProperties.Height, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<int>(() => c.Height = AsInt(o));
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return c.Height;
                        })
                    }
                },
                { 
                    CommonProperties.Width, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<int>(() => c.Width = AsInt(o));
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return c.Width;
                        })
                    }
                },
                { 
                    CommonProperties.Text, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<string>(() => c.Text = (string) o);
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return c.Text;
                        })
                    }
                },
                { 
                    CommonProperties.Name, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<string>(() => c.Name = (string) o);
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return c.Name;
                        })
                    }
                },
                { 
                    CommonProperties.OnClick, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<string>(() => ((ITampaControl)c).OnClickHandler = (string) o);
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return ((ITampaControl)c).OnClickHandler;
                        })
                    }
                },
                { 
                    CommonProperties.OnKeyPressHandler, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<string>(() => ((ITampaControl)c).OnKeyPressHandler = (string) o);
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return ((ITampaControl)c).OnKeyPressHandler;
                        })
                    }
                },
                { 
                    CommonProperties.OnMouseDownHandler, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<string>(() => ((ITampaControl)c).OnMouseDownHandler = (string) o);
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return ((ITampaControl)c).OnMouseDownHandler;
                        })
                    }
                },
                { 
                    CommonProperties.OnMouseMoveHandler, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<string>(() => ((ITampaControl)c).OnMouseMoveHandler = (string) o);
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return ((ITampaControl)c).OnMouseMoveHandler;
                        })
                    }
                },
                { 
                    CommonProperties.OnMouseUpHandler, 
                    new PropertyDealer
                    {
                        PropertySetter =
                        (o, c) =>
                        { 
                            return Handle<string>(() => ((ITampaControl)c).OnMouseUpHandler = (string) o);
                        },
                        PropertyGetter =
                        ((c) =>
                        {
                            return ((ITampaControl)c).OnMouseUpHandler;
                        })
                    }
                }

            };
        }

        private static int AsInt(object o) { return (o is int ? (int)o : int.Parse(o.ToString())); }
        protected static Dictionary<string, PropertyDealer> _propertyHandlers;
        private Dictionary<string, object> _props;

        internal void UpdateProperties(int x, int y, int w, int h)
        {
            Debug.WriteLine(String.Format("Setting to {0}, {1}, {2}, {3}", x, y, w, h));

            Point clientPoint = ParentControl.PointToClient(new Point { X = x, Y = y });
            this.Properties[CommonProperties.Left] = clientPoint.X;
            this.Properties[CommonProperties.Top] = clientPoint.Y;
            this.Properties[CommonProperties.Width] = w;
            this.Properties[CommonProperties.Height] = h;
            this.Update();
            this.ParentControl.Refresh();
        }

        public Control ParentControl
        {
            get
            {
                return (UnderlyingControl.Parent ?? (UnderlyingControl as Form).MdiParent) ?? TampaController.GetInstance().GetView();
            }
        }

        public void Serialize(System.Xml.XmlWriter writer)
        {
            Slurp();

            Type type = this.UnderlyingControl.GetType();

            writer.WriteStartElement(type.Name);

            foreach (string property in this.Properties.Keys)
            {
                if (this.Properties[property] != null)
                {
                    writer.WriteAttributeString(property, this.Properties[property].ToString());
                }
            }

            writer.WriteEndElement();
        }
    }
}
