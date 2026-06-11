using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    public static class JsonSerializerHelper<T> where T : class, new()
    {
        public static string FilePath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "Config", Name + ".json"); }
        }

        private static string Name { get { return typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1); } }

        public static bool Load(out T result, out string reason)
        {
            result = null;
            reason = string.Empty;
            if (!File.Exists(FilePath))
            {
                result = null;
                reason = "文件不存在";
                return false;
            }
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                    string strRead = string.Empty;

                    while (sr.EndOfStream != true)
                    {
                        strRead += sr.ReadLine();
                    }
                    sr.Close();
                    fs.Close();
                    result = JsonConvert.DeserializeObject<T>(strRead);
                    return true;
                }
            }
            catch (Exception ex)
            {
                result = null;
                reason = ex.Message;
                return false;
            }
        }

        public static bool Save(T value, out string reason)
        {
            reason = string.Empty;

            try
            {
                using (var fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    var strWrite = JsonConvert.SerializeObject(value);
                    var writeBytes = Encoding.UTF8.GetBytes(strWrite);
                    fileStream.Write(writeBytes, 0, writeBytes.Length);
                    fileStream.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                reason = ex.Message;
                return false;
            }
        }
    }
}
