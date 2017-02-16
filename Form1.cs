using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XMLParse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var Doc = new XmlDocument();
            Doc.Load("config.xml");
            XmlElement root = Doc.DocumentElement;

            var tagsForm = root.GetElementsByTagName("Form");
            var tagsFormElem = tagsForm[0] as XmlElement;
            GoThroughAllValues(this, tagsFormElem);

            createControl(root.GetElementsByTagName("button"), typeof(Button));
        }

        private void GoThroughAllValues(Control target, XmlElement xmlElement)
        {
            var children = xmlElement.ChildNodes;
            foreach (XmlElement child in children)
            {
                var childValue = child.InnerText;

                switch (child.Name)
                {
                    case "name":
                        target.Name = childValue;
                        break;
                    case "top":
                        target.Top = int.Parse(childValue);
                        break;
                    case "left":
                        target.Left = int.Parse(childValue);
                        break;
                    case "width":
                        target.Width = int.Parse(childValue);
                        break;
                    case "height":
                        target.Height = int.Parse(childValue);
                        break;
                    case "RGBcolor":
                        target.ForeColor = ParseRGBColor(childValue);
                        break;
                }
            }
        }

        private void createControl(XmlNodeList value, Type ControlType)
        {
            foreach (XmlElement child in value)
            {
                var control = Activator.CreateInstance(ControlType) as Control;
                this.Controls.Add(control);
                GoThroughAllValues(control, child);
            }
        }

        private Color ParseRGBColor(string rgbStr)
        {
            try
            {
                var rgbParts = rgbStr.Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);
                return Color.FromArgb(int.Parse(rgbParts[0]), int.Parse(rgbParts[1]), int.Parse(rgbParts[2]));
            }
            catch
            {
                return Color.Black;
            }
        }
    }
}