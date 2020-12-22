using Newtonsoft.Json;

namespace Utilities
{
    public static class JsonFormatter
    {


        /// <summary>
        /// Deserialize from json to object
        /// </summary>
        /// <typeparam name="T">Type for deserialization</typeparam>
        /// <param name="json">json as string</param>
        /// <returns>Deserialized object</returns>
        public static T FromJson<T>(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
        /// <summary>
        /// Serialize object to json
        /// </summary>
        /// <typeparam name="T">Type for serialization</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Json string</returns>
        public static string ToJson<T>(T obj)
        {
            if (obj != null)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Objects
                };
                return JsonConvert.SerializeObject(obj, settings);
            }
            return string.Empty;
        }
        /// <summary>
        /// Serialize to pretty json
        /// </summary>
        /// <typeparam name="T">Type for serialization</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Json string</returns>
        public static string ToPrettyJson<T>(T obj)
        {
            if (obj != null)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None,
                    Formatting = Formatting.Indented
                };
                return JsonConvert.SerializeObject(obj, settings);
            }
            return string.Empty;
        }

    }

}
