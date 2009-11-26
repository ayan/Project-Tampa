using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tampa.Interfaces
{
    public class ControlInstance
    {
        public ControlInstance(IControl control)
        {
            this.UnderlyingControl = control.AssociatedUserControlType.GetConstructor(new Type[0]).Invoke(new object[0]) as Control;
            this.UnderlyingControl.Visible = true;            
            _props = new Dictionary<string, object>();
        }

        public Control UnderlyingControl
        {
            get;
            private set;
        }

        public Dictionary<string, object> Properties
        {
            get
            {
                return _props;
            }
        }

        string GetMarkup()
        {
            // TEMPTEMP: Return the markup for the control here
            return "";
        }

        private Dictionary<string, object> _props;
    }
}
