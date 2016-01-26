using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ZF.Log;

namespace ZF.XML
{
    /// <summary>
    /// XML帮助类
    /// </summary>
    public class XMLHelper
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="filestream"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeXmlFile(FileStream filestream, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            return serializer.Deserialize(filestream);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="filestream"></param>
        /// <param name="type"></param>
        /// <param name="list"></param>
        public static void serializeXmlFile(FileStream filestream, Type type, object list)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            serializer.Serialize(filestream, list);
        }

        /// <summary>
        /// 创建xml
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <param name="list"></param>
        public static void Create(string path, Type type, object list)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                XMLHelper.serializeXmlFile(fs, type, list);
                LogHelper.WriteInfo("成功插入xml");
            }
        }
    }
}
