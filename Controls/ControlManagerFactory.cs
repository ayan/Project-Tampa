using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tampa.Controls
{
    public static class ControlManagerFactory
    {
        public static ControlManager GetControlManager()
        {
            lock (_lock)
            {
                return _manager ?? CreateControlManager();
            }
        }

        private static ControlManager CreateControlManager()
        {
            lock (_lock)
            {
                _manager = new ControlManager();

                _manager.Controls.Add(new ButtonControl());
                _manager.Controls.Add(new LabelControl());
                _manager.Controls.Add(new TextBoxControl());

                return _manager;
            }
        }

        private static object _lock = new object();
        private static ControlManager _manager;
    }
}
