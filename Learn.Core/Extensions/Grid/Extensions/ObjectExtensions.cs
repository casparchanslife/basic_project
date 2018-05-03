using Learn.Lib.Helpers.Grid.Utility;
using System.Web.Script.Serialization;

namespace Learn.Lib.Helpers.Grid.Extensions
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
