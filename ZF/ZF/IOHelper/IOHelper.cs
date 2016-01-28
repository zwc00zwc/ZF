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

        /// <summary>
        /// 文件拷贝  不支持文件目录有子目录
        /// </summary>
        /// <param name="srcDir"></param>
        /// <param name="tgtDir"></param>
        public static void CopyDirectory(string srcDir, string tgtDir)
        {
            DirectoryInfo source = new DirectoryInfo(srcDir);
            DirectoryInfo target = new DirectoryInfo(tgtDir);

            if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("父目录不能拷贝到子目录！");
            }

            if (!source.Exists)
            {
                return;
            }

            if (!target.Exists)
            {
                target.Create();
            }

            FileInfo[] files = source.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(files[i].FullName, target.FullName + @"\" + files[i].Name, true);
            }
        }
    }
}
