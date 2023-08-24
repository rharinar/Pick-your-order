using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TryItPage
{
    [XmlType]
    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ItemType { get; set; }

        public string NutritionalInformation { get; set; }
    }
}