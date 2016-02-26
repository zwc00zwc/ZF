using SharpCompress.Archive;
using SharpCompress.Common;
using SharpCompress.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.DeCompression
{
    public class DeCompressionHelper
    {
        /// <summary>
        /// 通用解压 支持rar,zip
        /// </summary>
        /// <param name="compressfilepath">压缩包路径</param>
        /// <param name="uncompressdir">解压路径</param>
        public static void UnCompress(string compressfilepath, string uncompressdir)
        {
            string ext = Path.GetExtension(compressfilepath).ToLower();
            if (ext == ".rar")
                UnRar(compressfilepath, uncompressdir);
            else if (ext == ".zip")
                UnZip(compressfilepath, uncompressdir);
        }
        /// <summary>
        /// 解压rar
        /// </summary>
        /// <param name="compressfilepath"></param>
        /// <param name="uncompressdir"></param>
        private static void UnRar(string compressfilepath, string uncompressdir)
        {
            using (Stream stream = File.OpenRead(compressfilepath))
            {
                using (var reader = ReaderFactory.Open(stream))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            reader.WriteEntryToDirectory(uncompressdir, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 解压zip
        /// </summary>
        /// <param name="compressfilepath"></param>
        /// <param name="uncompressdir"></param>
        private static void UnZip(string compressfilepath, string uncompressdir)
        {
            using (var archive = ArchiveFactory.Open(compressfilepath))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        entry.WriteToDirectory(uncompressdir, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                    }
                }
            }
        }
    }
}
