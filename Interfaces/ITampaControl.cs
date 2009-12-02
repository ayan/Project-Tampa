using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Tampa.Interfaces
{
    interface ITampaControl: ISelectableControl
    {
        string OnClickHandler { get; set; }
        string OnKeyPressHandler { get; set; }
        string OnMouseMoveHandler { get; set; }
        string OnMouseUpHandler { get; set; }
        string OnMouseDownHandler { get; set;  }
    }
}
