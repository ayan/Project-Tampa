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
            this.Controls = new List<IControl>();
        }

        public List<IControl> Controls { get; set; }
    }
}
