using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Tampa.Common;
using Tampa.Controls.WinForms;

namespace Tampa.InterfaceCompiler.Java
{
    class CanvasWriter
    {
        private Dictionary<string, string> _supportedControlProperties = new Dictionary<string,string>()
        {
            { CommonProperties.Name, "String" },
            { CommonProperties.Text, "String"}        
        };

        private Dictionary<string, string> _controlMapping = 
            new Dictionary<string,string>()
            {
                { 
                    (new SelectableButton()).GetType().Name, "JButton"
                },
                { 
                    (new SelectableLabel()).GetType().Name, "JLabel"
                },
                { 
                    (new SelectableTextBox()).GetType().Name, "JTextBox"
                },
                {
                    "Canvas", "JFrame"
                }
            };

        int _uniqueIndex = 0;
        private void SerializeControl(XmlNode controlNode, string parent)
        {
            string javaControl = _controlMapping[controlNode.Name];
            string name = javaControl + _uniqueIndex++;
            
            XmlAttribute attr = (XmlAttribute) controlNode.Attributes.GetNamedItem(CommonProperties.Name);

            if (attr != null)
            {
                name = attr.Value.Replace(" ", "_");
            }
            name = "_" + name.ToLower();

            CreateVar(name, javaControl);
            Write(name + " = new " + javaControl + "();");

            if (parent != String.Empty)
            {
                Write(parent + ".add(" + name + ");");
            }

            SerializeControlProperties(name, controlNode);

            Write(name + ".setBounds(" +
                controlNode.Attributes[CommonProperties.Left].Value + ", " +
                controlNode.Attributes[CommonProperties.Top].Value + ", " +
                controlNode.Attributes[CommonProperties.Width].Value + ", " +
                controlNode.Attributes[CommonProperties.Height].Value + ");");

        }

        private void SerializeControlProperties(string name, XmlNode controlNode)
        {
            foreach (string prop in _supportedControlProperties.Keys)
            {
                XmlAttribute attr = (XmlAttribute)controlNode.Attributes.GetNamedItem(prop);

                if (attr != null)
                {
                    Write(name + "." + prop + " = " + GetTypedValue(attr.Value, _supportedControlProperties[prop]) + ";");
                }                
            }

        }

        private string GetTypedValue(string value, string valueType)
        {
            if (valueType == "String")
            {
                return ("\"" + value + "\"");
            }
            else
            {
                return (value);
            }
        }

        public CanvasWriter(XmlNode canvas, int depth)
        {
            _canvasNode = canvas;
            _origDepth = _depth = depth;
        }

        public void CreateVar(string varName, string varType)
        {
            Write(_varBuffer, varType + " " + varName + ";");
        }

        public void Write(StringBuilder functions, StringBuilder vars)
        {
            _buffer = functions;
            _varBuffer = vars;

            CreateVar("_canvas", "JFrame");

            Write("private JFrame ShowCanvas() {");

            _depth++;
            Write("_canvas = new JFrame();");
            Write("canvas.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);");
            Write("Insets insets = canvas.getInsets()");
            Write("canvas.setSize(" + _canvasNode.Attributes[CommonProperties.Width].Value + ", " + _canvasNode.Attributes[CommonProperties.Height].Value + ");");
            Write("Container pane = canvas.getContentPane()");
            SerializeControlProperties("canvas", _canvasNode);

            foreach (XmlNode childControl in _canvasNode.ChildNodes)
            {
                SerializeControl(childControl, "pane");
            }

            Write("");            
            Write("return canvas;");

            _depth--;
            Write("}");

            StartActionListener();
            GenerateEventHandlers(_canvasNode);
            foreach (XmlNode childControl in _canvasNode.ChildNodes)
            {
                GenerateEventHandlers(childControl);
            }
            //EndActionListener();
        }

        private void EndActionListener()
        {
            Write(_actionHandler, "}");
            Write(_actionHandler, "}");

            Write(_actionHandler.ToString());
        }

        private void GenerateEventHandlers(XmlNode _canvasNode)
        {
            //throw new NotImplementedException();
        }

        private void StartActionListener()
        {
            //Write(_actionHandler, "public void actionPerformed(ActionEvent event){");
            //Write(_actionHandler, "    switch (event
        }


        void Write(string line) { Write(line, _depth); }
        void Write(StringBuilder sb, string line) { Write(sb, line, _origDepth); }
        void Write(string line, int depth) { Write(_buffer, line, depth); }

        void Write(StringBuilder buffer, string line, int depth)
        {
            for (int i = _depth; i < _depth + depth; i++)
            {
                buffer.Append("    ");
            }
            buffer.AppendLine(line);
        }

        StringBuilder _actionHandler = new StringBuilder();
        XmlNode _canvasNode;
        int _depth;
        int _origDepth;
        StringBuilder _buffer;
        StringBuilder _varBuffer;
    }
}
