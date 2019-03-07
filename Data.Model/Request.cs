using Newtonsoft.Json;
using System;
using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
//using System.Xml.Serialization;

namespace Data.Model
{
    /// <summary>
    /// TODO: refactor
    /// https://github.com/mjoniec/MarJonDemo/issues/4
    /// </summary>
    [Table("request")]
    public class Request
    {
        //Can not switch auto EF db generator to use Index as primary key and at the same time not to automatically overwrite it with generated value
        //[JsonIgnore]
        [JsonProperty("id")]
        public int Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[System.ComponentModel.DataAnnotations.Key/*, DatabaseGenerated(DatabaseGeneratedOption.None), JsonProperty("ix")*/]
        [JsonProperty("ix")]
        public int Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("visits")]
        //[XmlAttribute("visits")]
        public int? Visits { get; set; }

        [JsonProperty("date")]
        //[XmlAttribute("dateRequested")]
        public DateTime Date { get; set; }

        public string ToXML()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateNode(XmlNodeType.Element, GetType().Name, string.Empty);
            doc.AppendChild(root);
            XmlNode childNode;

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Request));

            var dateRequested = properties.Find("Date", false);
            childNode = doc.CreateNode(XmlNodeType.Element, "dateRequested", string.Empty);
            childNode.InnerText = dateRequested.GetValue(this).ToString();
            root.AppendChild(childNode);

            XmlNode content = doc.CreateNode(XmlNodeType.Element, "content", string.Empty);
            root.AppendChild(content);

            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.GetValue(this) != null &&
                    !string.Equals(prop.Name, "Date") &&
                    !string.Equals(prop.Name, "Id"))
                {
                    childNode = doc.CreateNode(XmlNodeType.Element, GetNameForXmlAttribute(prop.Name), string.Empty);
                    childNode.InnerText = prop.GetValue(this).ToString();
                    content.AppendChild(childNode);
                }
            }

            return doc.OuterXml;
        }

        private string GetNameForXmlAttribute(string propertyName)
        {
            if (string.Equals(propertyName, "Visits")) return "visits";
            if (string.Equals(propertyName, "Name")) return "name";
            if (string.Equals(propertyName, "Index")) return "ix";

            return propertyName;
        }
    }
}
