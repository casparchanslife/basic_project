using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Learn.Core.Localization
{
    public class XmlResourceProvider : ResourceProviderBase
    {
        // File path
        private static string filePath = null;

        public XmlResourceProvider() { }
        public XmlResourceProvider(string filePath)
        {
            XmlResourceProvider.filePath = filePath;

            if (!File.Exists(filePath)) throw new FileNotFoundException(string.Format("XML Resource file {0} was not found", filePath));
        }

        protected override IList<ResourceEntry> ReadResources()
        {

            // Parse the XML file
            //return XDocument.Parse(File.ReadAllText(filePath))
            //    .Element("resources")
            //    .Elements("resource")
            //    .Select(e => new ResourceEntry
            //    {
            //        Name = e.Attribute("name").Value,
            //        Value = e.Attribute("value").Value,
            //        Culture = e.Attribute("culture").Value,
            //        Region = e.Attribute("region").Value
            //    }).ToList();
            var list = XDocument.Parse(File.ReadAllText(filePath))
                    .Element("resources")
                    .Elements("resource")
                     .Select(e => new ResourceEntry
                    {
                        Name = e.Attribute("name").Value,
                        Value = e.Elements("value").FirstOrDefault() != null ? e.Elements("value").FirstOrDefault().ToString() : e.Attribute("value").Value,
                        Culture = e.Attribute("culture").Value,
                        Region = e.Attribute("region").Value
                    }).ToList();
            return list;
        }

        protected override ResourceEntry ReadResource(string name, string culture, string region)
        {
            // Parse the XML file

            return XDocument.Parse(File.ReadAllText(filePath))
                .Element("resources")
                .Elements("resource")
                .Where(e => e.Attribute("name").Value == name && e.Attribute("culture").Value == culture && e.Attribute("region").Value == region)
                .Select(e => new ResourceEntry
                {
                    Name = e.Attribute("name").Value,
                    Value = e.Attribute("value").Value,
                    Culture = e.Attribute("culture").Value,
                    Region = e.Attribute("region").Value
                }).FirstOrDefault();
        }
    }
}