using System.Web.Script.Serialization;
using Eslite.Lib.Helpers.Grid.Utility;

namespace Eslite.Lib.Helpers.Grid.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}
