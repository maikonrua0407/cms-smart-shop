using System.Xml;
namespace SmartShop.Utilities.Helper
{
    public class XmlHelper
    {

        protected string mstrXMLFilePath;

        ///************************************************************************
        /// <summary>
        /// Create new instance
        /// </summary>
        /// <remarks></remarks>
        ///************************************************************************
        public XmlHelper()
        {
        }

        ///************************************************************************
        /// <summary>
        /// Create new instance with XML file path
        /// </summary>
        /// <param name="strXMLFilePath">XML file path</param>
        ///************************************************************************
        public XmlHelper(string strXMLFilePath)
        {
            mstrXMLFilePath = strXMLFilePath;
        }

        ///************************************************************************
        /// <summary>
        /// Set value for XML file path
        /// </summary>
        /// <param name="strXMLFilePath">XML file path</param>
        ///************************************************************************
        public void SetXMLFilePath(string strXMLFilePath)
        {
            mstrXMLFilePath = strXMLFilePath;
        }

        ///************************************************************************
        /// <summary>
        /// Read data from XML file
        /// </summary>
        /// <param name="strElement1">XML parent Node</param>
        /// <param name="strElement2">XML child Node</param>
        /// <returns>Value of child Node</returns>
        ///************************************************************************
        public string ReadXmlFile(string strElement1, string strElement2)
        {

            XmlNode node = default(XmlNode);
            XmlDocument xmlDoc = default(XmlDocument);

            xmlDoc = new XmlDocument();
            xmlDoc.Load(mstrXMLFilePath);

            node = xmlDoc.SelectSingleNode(strElement1 + "/" + strElement2);

            if ((node != null))
            {
                return node.InnerText;
            }
            else
            {
                return string.Empty;

            }
        }

        ///************************************************************************
        /// <summary>
        /// Write data to XML file
        /// </summary>
        /// <param name="strElement">XML Node</param>
        /// <param name="strValue">Value to write</param>
        ///************************************************************************
        public void WriteXmlFile(string strElement, string strValue)
        {

            XmlElement xmlRoot = default(XmlElement);
            XmlElement xmlElement = default(XmlElement);
            XmlText xmlValue = default(XmlText);
            XmlDocument xmlDoc = default(XmlDocument);

            xmlDoc = new XmlDocument();
            xmlDoc.Load(mstrXMLFilePath);

            xmlRoot = xmlDoc.DocumentElement;

            xmlElement = xmlDoc.CreateElement(strElement);

            xmlValue = xmlDoc.CreateTextNode(strValue);

            xmlElement.AppendChild(xmlValue);
            xmlRoot.AppendChild(xmlElement);


            xmlDoc.Save(mstrXMLFilePath);
        }

        ///************************************************************************
        /// <summary>
        /// Update data to XML file
        /// </summary>
        /// <param name="strElement">XML Node</param>
        /// <param name="strValue">Value to write</param>
        ///************************************************************************
        public void UpDateXMLFile(string strElement, string strValue)
        {

            XmlDocument xmlDoc = default(XmlDocument);
            XmlNodeList xmlnodelist = default(XmlNodeList);
            XmlElement xmlelem = default(XmlElement);

            xmlDoc = new XmlDocument();
            xmlDoc.Load(mstrXMLFilePath);

            xmlnodelist = xmlDoc.GetElementsByTagName(strElement);

            if (xmlnodelist.Count == 0)
            {
                xmlelem = xmlDoc.CreateElement(strElement);
                xmlelem.InnerText = strValue;
                xmlDoc.DocumentElement.AppendChild(xmlelem);
            }
            else
            {
                xmlnodelist[0].InnerText = strValue;
            }


            xmlDoc.Save(mstrXMLFilePath);
        }

        ///************************************************************************
        /// <summary>
        /// Create XML file
        /// </summary>
        /// <param name="strFileNM">File name</param>
        /// <param name="strElement1">Root element</param>
        ///************************************************************************
        public void CreateXMLFile(string strFileNM, string strElement1)
        {

            System.IO.StreamWriter sw = default(System.IO.StreamWriter);

            sw = System.IO.File.CreateText(strFileNM);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sw.WriteLine("<" + strElement1 + ">");
            sw.WriteLine("</" + strElement1 + ">");

            sw.Close();
        }

    }
}