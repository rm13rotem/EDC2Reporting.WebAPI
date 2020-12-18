using System;
using System.Text.Json;

namespace Utilities
{
    public static class JsonFormatter
    {
        public static  T Deserialize<T>(string jsonString)
            where T : class
        {
            T t = default;
            
            try
            {
                t = JsonSerializer.Deserialize<T>(jsonString);
            }
            catch (Exception)
            {
                return null;
            }

            return t;
        }

        public static string Serialize(object obj)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true, IgnoreNullValues = true
            };

            try
            {
                return JsonSerializer.Serialize(obj, options);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
