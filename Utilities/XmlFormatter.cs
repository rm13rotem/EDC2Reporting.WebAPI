using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace Utilities
{
    public static class XmlFormatter
    {
        /// <summary>
        /// Deserialize from xml to object
        /// </summary>
        /// <typeparam name="T">Type for deserialization</typeparam>
        /// <param name="xml">xml as string</param>
        /// <returns>Deserialized object</returns>
        public static T FromXml<T>(string xml)
        {
            T res = default(T);
            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                res = (T)ser.Deserialize(reader);
            }
            return res;
        }
        /// <summary>
        /// Serialize object to xml
        /// </summary>
        /// <typeparam name="T">Type for serialization</typeparam>
        /// <param name="obj">Object for serialization</param>
        /// <returns>xml as string</returns>
        public static string ToXML<T>(T obj)
        {
            if (obj != null)
            {
                StringWriter writer = new Utf8StringWriter();
                XmlSerializer ser = new XmlSerializer(typeof(T));
                ser.Serialize(writer, obj);
                return writer.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// Class for UTF-8 encoding
        /// </summary>
        public class Utf8StringWriter : StringWriter
        {
            /// <summary>
            /// Encoding field
            /// </summary>
            public override Encoding Encoding => Encoding.UTF8;
        }

        /// <summary>
        /// Compress object to string
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">Object to compress</param>
        /// <returns>Compressed string</returns>
        public static string ToCompressedString<T>(T obj)
        {
            string str = ToXML(obj);
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            MemoryStream memoryStream = new MemoryStream();
            using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            byte[] compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            byte[] gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompress string back to an object
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="str">String to decompress</param>
        /// <returns>Original object</returns>
        public static T FromCompressedString<T>(string str)
        {
            byte[] gZipBuffer = Convert.FromBase64String(str);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                byte[] buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                string xml = Encoding.UTF8.GetString(buffer);
                T obj = FromXml<T>(xml);
                return obj;
            }
        }


    }
}
