using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Core.Localization
{
    public interface IResourceProvider
    {
        object GetResource(string name, string culture, string region);
    }
}