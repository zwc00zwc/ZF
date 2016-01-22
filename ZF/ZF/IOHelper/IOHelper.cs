using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZF.IOHelper
{
    /// <summary>
    /// IO操作类
    /// </summary>
    public class IOHelper
    {
        /// <summary>
        /// 目录不存在创建
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        { 
            string dir= Path.GetDirectoryName(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void Write(string path,string content)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.Write(content);
                sw.Flush();
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Read(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            {
                StreamReader sr = new StreamReader(fs);
                string str = sr.ReadToEnd();
                return str;
            }
        }
    }
}
