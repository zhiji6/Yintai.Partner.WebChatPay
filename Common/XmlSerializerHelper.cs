// -----------------------------------------------------------------------
// <copyright file="XmlSerializerHelper.cs" company="Yintai">
// 序列化对象和xml序列化为对象
// todo:注意内存泄露的问题http://support.microsoft.com/kb/886385/en-us
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Yintai.Partner.WebChatPay.Common
{
    /// <summary>
    /// 序列化对象 
    /// </summary>
    public class XmlSerializerHelper
    {
        /// <summary>
        /// 序列号器存储
        /// </summary>
        private static Dictionary<string, XmlSerializer> serializers = new Dictionary<string, XmlSerializer>();

        /// <summary>
        /// the xml convert to object
        /// </summary>
        /// <param name="filename">xml文件路径</param>
        /// <returns></returns>
        public T DeserializeToObject<T>(string filename) where T : class
        {
            T targetObject;
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                var objectType = typeof (T);
                var key = objectType.ToString();
                XmlSerializer ser = null;
                if (serializers.ContainsKey(key) && serializers[key] != null)
                {
                    ser = serializers[key];
                }
                else
                {
                    ser = new XmlSerializer(objectType);
                    serializers.Add(key, ser);
                }

                targetObject = ser.Deserialize(fs) as T; //// (T)formatter.Deserialize(fs);
                fs.Close();
                fs.Dispose();
            }
            return targetObject;
        }

        /// <summary>
        /// the xml convert to object
        /// </summary>
        /// <param name="xmlStr">xml文件路径</param>
        /// <returns></returns>
        public T DeserializeStrToObject<T>(string xmlStr) where T : class
        {
            T targetObject;
            using (var reader = new StringReader(xmlStr))
            {
                var objectType = typeof(T);
                var key = objectType.ToString();
                XmlSerializer ser = null;
                if (serializers.ContainsKey(key) && serializers[key] != null)
                {
                    ser = serializers[key];
                }
                else
                {
                    ser = new XmlSerializer(objectType);
                    serializers.Add(key, ser);
                }

                targetObject = ser.Deserialize(reader) as T; //// (T)formatter.Deserialize(fs);
                reader.Close();
                reader.Dispose();
            }
            return targetObject;
        }

        /// <summary>
        /// the xml convert to object
        /// </summary>
        /// <param name="xml">要序列化的xml  </param>
        /// <param name="objectType">序列化对象类型 objectType</param>
        /// <param name="rootName">如果是list，可用此参数指定list名字，而不是ArrayOfClassName</param>
        /// <returns></returns>
        public static object XmlSerializerToObject(string xml, Type objectType, XmlRootAttribute rootName)
        {
            object convertedObject = null;

            if (!string.IsNullOrEmpty(xml))
            {
                using (var reader = new StringReader(xml))
                {
                    var key = objectType.ToString() + rootName.ElementName;
                    XmlSerializer ser = null;
                    if (serializers.ContainsKey(key) && serializers[key] != null)
                    {
                        ser = serializers[key];
                    }
                    else
                    {
                        ser = new XmlSerializer(objectType, rootName);
                        serializers.Add(key, ser);
                    }

                    convertedObject = ser.Deserialize(reader);
                    reader.Close();
                }
            }

            return convertedObject;
        }
    }
}
