using LT.Common.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class LocalFileOperate
    {
        /// <summary>
        /// 得到指定目录下的数据量大小
        /// </summary>
        /// <param name="dirPath">文件目录</param>
        /// <returns></returns>
        public static long GetDirectoryLength(string dirPath)
        {
            //判断给定的路径是否存在,如果不存在则退出
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;

            //定义一个DirectoryInfo对象
            DirectoryInfo di = new DirectoryInfo(dirPath);

            //通过GetFiles方法,获取di目录中的所有文件的大小
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }

            //获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetDirectoryLength(dis[i].FullName);
                }
            }
            return len;
        }

        /// <summary>
        /// 获取指定文件的大小
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static long GetFileLength(string filePath)
        {
            //判断当前路径所指向的是否为文件
            if (System.IO.File.Exists(filePath))
            {
                //定义一个FileInfo对象,使之与filePath所指向的文件向关联,
                //以获取其大小
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取此目录下的所有文件个数
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static long FileOrDirectory(/*DirectoryInfo*/string path)
        {
            long count = 0;
            //统计文件的个数
            try
            {
                var files = Directory.GetFiles(path); //String数组类型
                count += files.Length;

                //遍历文件夹
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    count += FileOrDirectory(dir);
                }

            }
            catch (Exception e) { Logger.Log("Debug", "获取" + path + "目录下所有文件个数出错：" + e.Message); }
            return count;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="logContext">日志内容</param>
        /// <param name="filepath">文件路径（含文件名及后缀）</param>
        /// <returns></returns>
        public static bool WriteFile(string logContext, string filepath,out string reason)
        {
            reason = null;
            try
            {
                string folder = filepath.Substring(0, filepath.LastIndexOf('\\'));
                // 创建目录
                if (Directory.Exists(folder) == false)
                {
                    Directory.CreateDirectory(folder);
                }
                // 当文件不存在时创建文件
                if (File.Exists(filepath) == false)
                {
                    FileStream fs = File.Create(filepath);
                    fs.Close();
                }
                // 写入文件内容
                File.AppendAllText(filepath, logContext + "\r\n", Encoding.Default);
                Logger.Log("Content", $"文件{filepath}写入内容：\r\n\"{logContext}");
                return true;
            }
            catch(Exception ex)
            {
                reason = ex.ToString();
                return false;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件路径（含文件名及后缀）</param>
        /// <returns></returns>
        public static bool DeleteFile(string filePath)
        {
            try
            {
                // 1、首先判断文件或者文件路径是否存在
                if (File.Exists(filePath))
                {
                    // 2、根据路径字符串判断是文件还是文件夹
                    FileAttributes attr = File.GetAttributes(filePath);
                    // 3、根据具体类型进行删除
                    if (attr == FileAttributes.Directory)
                    {
                        // 3.1、删除文件夹
                        Directory.Delete(filePath, true);
                    }
                    else
                    {
                        // 3.2、删除文件
                        File.Delete(filePath);
                    }
                    File.Delete(filePath);
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

      
    }
}
