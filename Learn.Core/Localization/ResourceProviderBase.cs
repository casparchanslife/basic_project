using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Core.Localization
{
    public abstract class ResourceProviderBase : IResourceProvider
    {
        // Cache list of resources
        private static Dictionary<string, ResourceEntry> resources = null;
        private static object lockResources = new object();

        public ResourceProviderBase()
        {
            Cache = true; // By default, enable caching for performance
        }

        protected bool Cache { get; set; } // Cache resources ?

        /// <summary>
        /// Returns a single resource for a specific culture
        /// </summary>
        /// <param name="name">Resorce name (ie key)</param>
        /// <param name="culture">Culture code</param>
        /// <returns>Resource</returns>
        public object GetResource(string name, string culture, string region)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Resource name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(culture))
                throw new ArgumentException("Culture name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException("Region name cannot be null or empty.");

            if (Cache && resources == null)
            {
                // Fetch all resources
                lock (lockResources)
                {
                    if (resources == null)
                    {
                        var checkDuplicateKeyList = ReadResources().Select(r => string.Format("{0}.{1}.{2}", r.Culture.ToLowerInvariant(), r.Name.ToLowerInvariant(), r.Region.ToLowerInvariant()));
                        var q = (
                            from r in checkDuplicateKeyList
                            orderby r
                            group r by r into grp
                            select new { key = grp.Key, cnt = grp.Count() }
                        );
                        var samekeyStringException = "";
                        foreach(var obj in q.Where(o=>o.cnt > 1))
                        {
                            samekeyStringException += obj.key + ",";
                        }
                        if (!String.IsNullOrEmpty(samekeyStringException))
                        {
                            throw new Exception("Duplicated key in Resources.xml file:" + samekeyStringException);
                        }

                        resources = ReadResources().ToDictionary(r => string.Format("{0}.{1}.{2}", r.Culture.ToLowerInvariant(), r.Name.ToLowerInvariant(), r.Region.ToLowerInvariant()));
                    }
                }
            }

            // normalize
            name = name.ToLowerInvariant();
            culture = culture.ToLowerInvariant();
            region = region.ToLowerInvariant();

            if (Cache)
            {
                var key = string.Format("{0}.{1}.{2}", culture, name, region);
                if (resources.ContainsKey(key))
                {
                    return resources[key].Value;
                }
                else
                {
                    key = string.Format("{0}.{1}.{2}", culture, name, Area.TW);
                    return resources.ContainsKey(key) ? resources[key].Value : key;
                }
            }

            return ReadResource(name, culture, region).Value;

        }


        /// <summary>
        /// Returns all resources for all cultures. (Needed for caching)
        /// </summary>
        /// <returns>A list of resources</returns>
        protected abstract IList<ResourceEntry> ReadResources();


        /// <summary>
        /// Returns a single resource for a specific culture
        /// </summary>
        /// <param name="name">Resorce name (ie key)</param>
        /// <param name="culture">Culture code</param>
        /// <returns>Resource</returns>
        protected abstract ResourceEntry ReadResource(string name, string culture, string region);

    }
}