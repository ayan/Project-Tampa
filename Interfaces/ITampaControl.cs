using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Tampa.Interfaces
{
    interface ITampaControl: ISelectableControl
    {
        string OnClickHandler { get; }
        string OnKeyPressHandler { get; }
        string OnMouseMoveHandler { get; }
        string OnMouseUpHandler { get; }
        string OnMouseDownHandler { get; }
    }
}
