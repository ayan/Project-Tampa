using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tampa.Interfaces;
using System.Windows.Forms;

namespace Tampa.Controls
{
    abstract class BaseControl: IControl
    {
        public BaseControl()
        {
        }

        #region IControl Members

        public abstract string Icon { get; }
        public abstract Type AssociatedUserControlType { get; }

        public ControlInstance InstantiateUserControl()
        {
            return new ControlInstance(this);
        }

        #endregion
    }
}
