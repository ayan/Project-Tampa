using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tampa.Interfaces;

namespace Tampa.Controls
{
    /// <summary>
    /// Class that manages all the controls that can be added to a canvas
    /// </summary>
    public class ControlManager
    {
        public ControlManager()
        {
            _controls = new Dictionary<string, IControl>();
        }

        public void AddControl(IControl control, string underlyingType)
        {
            _controls[underlyingType] = control;
        }

        public IControl GetControlForType(string underlyingType)
        {
            return _controls[underlyingType];
        }

        private Dictionary<string, IControl> _controls;
        public List<IControl> Controls
        {
            get { return _controls.Values.ToList(); }
        }
    }
}
