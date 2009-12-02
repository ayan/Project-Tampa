using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Tampa.Interfaces
{
    public interface ICompiler
    {
        string Compile(XmlDocument canvasXml, string className);
    }
}
