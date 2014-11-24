using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GSS
{
    /// <summary>
    /// JSON Serialization and Deserialization Assistant Class
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// JSON serializer
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="t">Object data</param>
        /// <returns>JSON string</returns>
        public static string Serialize<T>(T t) {
            string jsonString = "";

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream()) {
                ser.WriteObject(ms, t);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
            }

            return jsonString;
        }

        /// <summary>
        /// JSON deserializer
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="jsonString">JSON string</param>
        /// <returns>Object data</returns>
        public static T Deserialize<T>(string jsonString) {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);

            return obj;
        }

        /// <summary>
        /// Download JSON from url, return jsonData, and serialize to object T
        /// </summary>
        /// <typeparam name="T">Type to serialize object</typeparam>
        /// <param name="url">URL to request JSON data</param>
        /// <param name="jsonData">JSON string to return</param>
        /// <returns>Serialized object</returns>
        public static T DownloadAndSerializeJsonData<T>(string url, out string jsonData) where T : new() {
            jsonData = "";

            using (var web = new WebClient()) {
                // attempt to download JSON data as a string
                jsonData = web.DownloadString(url);

                // if string with JSON data is not empty, deserialize it to class and return its instance 
                if (!string.IsNullOrEmpty(jsonData)) {
                    return Deserialize<T>(jsonData);
                }
                else {
                    return new T();
                }
            }
        }
    }
}
