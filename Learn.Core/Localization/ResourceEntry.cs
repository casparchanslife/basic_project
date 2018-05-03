using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Core.Localization
{
    public class ResourceEntry
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Culture { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }

        public ResourceEntry()
        {
            Type = "string";
        }
    }
}