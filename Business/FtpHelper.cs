using Amazon.S3;
using Amazon.S3.Transfer;
using FluentFTP;
using LT.Common.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business
{
    public /*static*/ class FtpHelper
    {
        #region AmazonS3Transfer
        //public static bool AmazonS3Transfer(this IAmazonS3 s3Client, string fullPath, out string reason)
        //{
        //    reason = string.Empty;
        //    try
        //    {
        //        var bucketName = $"{DateTime.Now:yyyymmdd}";
        //        if (!s3Client.DoesS3BucketExist(bucketName))
        //        {
        //            reason = $"{bucketName}不存在";
        //            Logger.Log("Debug", reason);
        //            return false;
        //        }

        //        var fileInfo = new FileInfo(fullPath);
        //        var filePath = fileInfo.DirectoryName;
        //        var key = fileInfo.Name.Split('.')[0];
        //        var transferUtility = new TransferUtility(s3Client);
        //        if (File.Exists(fullPath))
        //        {
        //            transferUtility.Upload(filePath, bucketName, key);
        //        }
        //        else
        //        {
        //            Logger.Log("Debug", $"{fullPath}不存在");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log("Debug", $"{fullPath}上传失败，{ex.Message}");
        //        reason = ex.Message;
        //    }

        //    return string.IsNullOrEmpty(reason);
        //}
        #endregion

        #region 
        public static string username;
        public static string password;
        public static string host;
        #endregion

        #region Old
        /// <summary>
        /// 在远程服务器上创建文件并将数据流写入
        /// </summary>
        /// <param name="uri">远程服务器文件的绝对路径</param>
        /// <param name="memoryStream">本地文件流</param>
        /// <param name="username">远程服务器登录账号</param>
        /// <param name="password">远程服务器登录密码</param>
        /// <param name="reason">上传结果反馈</param>
        /// <returns>上传成功与否</returns>
        public static bool Upload(string uri, Stream memoryStream, bool isFTPS/*, string username, string password,*/, out string reason)
        {
            reason = string.Empty;
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(uri);//new Uri(uri));
                //request.Credentials = new NetworkCredential(username, password);
                request.Credentials = new NetworkCredential(username, password);
                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                request.ContentLength = memoryStream.Length;
                request.UsePassive = true;// 设置使用被动模式 (通常更适合客户端)
                if (isFTPS)
                {
                    request.EnableSsl = true;// 启用SSL/TLS (FTPS)
                    ServicePointManager.ServerCertificateValidationCallback =
                        new System.Net.Security.RemoteCertificateValidationCallback(
                            (sender, certificate, chain, sslPolicyErrors) =>
                            {
                                // 这里可以添加自定义证书验证逻辑
                                // 对于测试环境，可以简单返回true接受所有证书
                                // 生产环境应验证证书有效性
                                return true;
                            });// 处理服务器证书验证
                }

                using (var stream = request.GetRequestStream())
                {
                    int length;
                    var buffer = new byte[10240];
                    memoryStream.Position = 0;
                    while ((length = memoryStream.Read(buffer, 0, 10240)) > 0)
                    {
                        stream.Write(buffer, 0, length);
                    }

                    memoryStream.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Debug", $"{uri}上传失败，{ex.Message}");
                reason = ex.Message;
            }

            return string.IsNullOrEmpty(reason);
        }

        /// <summary>
        /// 获取本地绝对路径文件并上传至远程服务器
        /// </summary>
        /// <param name="root">远程服务器已创建存在的根目录</param>
        /// <param name="uri">远程服务器文件的绝对路径</param>
        /// <param name="path">上传文件在本地的绝对路径</param>
        /// <param name="username">远程服务器登录账号</param>
        /// <param name="password">远程服务器登录密码</param>
        /// <param name="reason">上传结果反馈</param>
        /// <returns>上传成功与否</returns>
        public static bool CreateFileWithPath(string root, string uri, string path, bool isFTPS,/*string username, string password,*/ out string reason)
        {
            try
            {
                var subDir = uri.Replace($"{root}/", "")
                    .Replace($"{uri.Split('/').Last()}", "");
                if (string.IsNullOrEmpty(subDir) == false)
                    MakeDirs(root, subDir, isFTPS);
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    Upload(uri, stream, isFTPS, out reason);
                }
                Logger.Log("Debug", $"{uri}上传成功，上传文件：{path}");
            }
            catch (Exception ex)
            {
                Logger.Log("Debug", $"{uri}上传失败，{ex.Message}，本地路径：{path}");
                reason = ex.Message;
            }

            return string.IsNullOrEmpty(reason);
        }

        /// <summary>
        /// 在远程服务器上创建文件并写入value
        /// </summary>
        /// <param name="root">远程服务器已创建存在的根目录</param>
        /// <param name="uri">远程服务器文件的绝对路径</param>
        /// <param name="value">写入远程文件的信息</param>
        /// <param name="isFTPS">协议是否加密</param>
        /// <param name="username">远程服务器登录账号</param>
        /// <param name="password">远程服务器登录密码</param>
        /// <param name="reason">上传结果反馈</param>
        /// <returns>上传成功与否</returns>
        public static bool CreateFileWithValue(string root, string uri, string value, bool isFTPS/*,string username, string password,*/, out string reason)
        {
            try
            {
                var subDir = uri.Replace($"{root}/", "")
                    .Replace($"{uri.Split('/').Last()}", "");
                if (string.IsNullOrEmpty(subDir) == false)
                    MakeDirs(root, subDir, isFTPS);

                var request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                request.Credentials = new NetworkCredential(username, password);
                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                request.ContentLength = value.Length;
                request.UsePassive = true;// 设置使用被动模式 (通常更适合客户端)
                if (isFTPS)
                {
                    request.EnableSsl = true;// 启用SSL/TLS (FTPS)
                    ServicePointManager.ServerCertificateValidationCallback =
                        new System.Net.Security.RemoteCertificateValidationCallback(
                            (sender, certificate, chain, sslPolicyErrors) =>
                            {
                                // 这里可以添加自定义证书验证逻辑
                                // 对于测试环境，可以简单返回true接受所有证书
                                // 生产环境应验证证书有效性
                                return true;
                            });// 处理服务器证书验证
                }

                var bufferLength = 10240;
                var buffer = Encoding.UTF8.GetBytes(value);
                using (var stream = request.GetRequestStream())
                {
                    var count = buffer.Length;
                    int offsetlen = 0;
                    while (count > 0)
                    {
                        //if (buffer.Length < bufferLength)
                        //{
                        //    bufferLength = buffer.Length;
                        //}
                        if (count < bufferLength)
                        {
                            bufferLength = count;
                        }

                        stream.Write(buffer, offsetlen, bufferLength);
                        count -= bufferLength;
                        offsetlen = buffer.Length - count;
                    }
                }

                reason = string.Empty;
                Logger.Log("Debug", $"{uri}上传成功，内容：{value}");
            }
            catch (Exception ex)
            {
                Logger.Log("Debug", $"{uri}上传失败，{ex.Message}，内容：{value}");
                reason = ex.Message;
            }

            return string.IsNullOrEmpty(reason);
        }

        /// <summary>
        /// 在远程服务器上创建文件夹
        /// </summary>
        /// <param name="rootUri">远程服务器已创建存在的根目录</param>
        /// <param name="subUri">远程服务器需要创建的路径</param>   //"data/remote/path/"
        /// <param name="username">远程服务器登录账号</param>
        /// <param name="password">远程服务器登录密码</param>
        public static void MakeDirs(string rootUri, string subUri, bool isFTPS/*, string username, string password*/)
        {
            var dirs = subUri.Split('/').ToList();
            for (var i = 1; i <= dirs.Count; i++)
            {
                var tmpDirs = string.IsNullOrEmpty(rootUri)
                    ? new List<string>()
                    : new List<string> { rootUri };
                tmpDirs.AddRange(dirs.GetRange(0, i));
                var tmpUri = string.Join("/", tmpDirs);
                MakeDir(tmpUri, isFTPS);
            }
        }

        public static void MakeDir(string uri, bool isFTPS/*, string username, string password*/)
        {
            try
            {
                //LogHelper.DebugLog($"当前makedir_URI:{uri}");
                if (ExistDir(uri, isFTPS))
                {
                    Logger.Log("Debug", $"URI{uri}已经存在,创建下一目录");
                    return;
                }

                var request = (FtpWebRequest)WebRequest.Create(uri);//new Uri(uri));
                request.Credentials = new NetworkCredential(username, password);
                request.KeepAlive = false;
                request.UsePassive = true;
                request.Method = WebRequestMethods.Ftp.MakeDirectory;

                if (isFTPS)
                {
                    request.EnableSsl = true;// 启用SSL/TLS (FTPS)
                    ServicePointManager.ServerCertificateValidationCallback =
                        new System.Net.Security.RemoteCertificateValidationCallback(
                            (sender, certificate, chain, sslPolicyErrors) =>
                            {
                                // 这里可以添加自定义证书验证逻辑
                                // 对于测试环境，可以简单返回true接受所有证书
                                // 生产环境应验证证书有效性
                                return true;
                            });// 处理服务器证书验证
                }
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    var success = response.StatusCode == FtpStatusCode.PathnameCreated;
                    Logger.Log("Debug", $"{uri}创建uri{(success ? "成功" : "失败")}，{response.StatusDescription}");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Debug", $"{uri}创建uri失败，{ex.Message}");
            }
        }

        /// <summary>
        /// 判断远程服务器是否存在文件路径
        /// </summary>
        /// <param name="uri">远程服务器文件路径</param>
        /// <param name="username">远程服务器登录账号</param>
        /// <param name="password">远程服务器登录密码</param>
        public static bool ExistDir(string uri, bool isFTPS/*, string username, string password*/)
        {
            try
            {
                var uris = new Uri(uri);
                var request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                request.Credentials = new NetworkCredential(username, password);
                request.KeepAlive = true;
                request.UsePassive = true;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                if (isFTPS)
                {
                    request.EnableSsl = true;// 启用SSL/TLS (FTPS)
                    ServicePointManager.ServerCertificateValidationCallback =
                        new System.Net.Security.RemoteCertificateValidationCallback(
                            (sender, certificate, chain, sslPolicyErrors) =>
                            {
                                // 这里可以添加自定义证书验证逻辑
                                // 对于测试环境，可以简单返回true接受所有证书
                                // 生产环境应验证证书有效性
                                return true;
                            });// 处理服务器证书验证
                }
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    var success = response.StatusCode == FtpStatusCode.DataAlreadyOpen;
                    Logger.Log("Debug", $"{uri}打开{(success ? "成功" : "失败")}，{response.StatusDescription}");
                    return success;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Debug", $"{uri}打开失败，{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 从ftp服务器下载文件的功能
        /// </summary>
        /// <param name="uri">FTP地址</param>//ftp://XX.XX.XX.XX
        /// <param name="remoteFilePath">ftp下载的地址</param>//"data\\PVD镀膜(3)\\e\\X46E\\M02\\1ED\\X46EM021ED_PVD镀膜(3)_E_20241026_114510_PNL_DEF.PNL"
        /// <param name="localFilePath">存放到本地的路径</param>//"E:\\ftpDownloadTest\\X46EM021ED_PVD镀膜(3)_E_20241026_114510_PNL_DEF.PNL"
        /// <returns></returns>
        public static bool DownloadFile(string uri, string remoteFilePath, string localFilePath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri + "/" + remoteFilePath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    FileStream localFileStream = new FileStream(localFilePath, FileMode.Create);

                    byte[] buffer = new byte[2048];
                    int bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    // Write the downloaded data to a local file.
                    while (bytesRead > 0)
                    {
                        localFileStream.Write(buffer, 0, bytesRead);
                        bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                    }

                    // Close the response and stream objects
                    response.Close();
                    localFileStream.Close();
                }
                #region
                //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Stream responseStream = response.GetResponseStream();
                //FileStream localFileStream = new FileStream(localFilePath, FileMode.Create);
                //byte[] buffer = new byte[2048];
                //int bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                //// Write the downloaded data to a local file.
                //while (bytesRead > 0)
                //{
                //    localFileStream.Write(buffer, 0, bytesRead);
                //    bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                //}
                //// Close the response and stream objects
                //response.Close();
                //localFileStream.Close();
                #endregion
                bool success = Directory.Exists(localFilePath);
                Logger.Log("Debug", $"{uri}/{remoteFilePath}下载{(success ? "成功" : "失败")}");
                return success;
            }
            catch (Exception ex)
            {
                Logger.Log("Debug", $"{uri}/{remoteFilePath}下载失败，{ex.Message}");
                return false;
            }
        }
        #endregion

        #region fluentFTP
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="filePathRemote"></param>
        /// <param name="filePathLocal"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UploadFileWithPath(string filePathRemote, string filePathLocal, out string reason)
        {
            reason = string.Empty;

            FtpStatus uploadResult = FtpStatus.Failed;
            try
            {
                Uri ftpUri = new Uri(filePathRemote);
                string ftpHost = ftpUri.Host;
                int ftpPort = ftpUri.Port > 0 ? ftpUri.Port : 21;
                string remoteFilePath = ftpUri.AbsolutePath;
                string remoteDirectory = Path.GetDirectoryName(remoteFilePath).Replace('\\', '/');
                string remoteFileName = Path.GetFileName(remoteFilePath);

                NetworkCredential ftpCredentials = new NetworkCredential(username, password);
                // 初始化FtpClient
                var ftpClient = new FtpClient();
                ftpClient.Host = ftpHost;
                ftpClient.Port = ftpPort;
                ftpClient.Credentials = new NetworkCredential(username, password);
                ftpClient.Encoding = Encoding.UTF8;
                ftpClient.Config.DataConnectionType = FtpDataConnectionType.AutoPassive;
                ftpClient.Config.ValidateAnyCertificate = true;

                ftpClient.Connect();
                if (!ftpClient.IsConnected)
                {
                    reason = "FTP服务器连接失败（主机/端口/账号密码错误）";
                    Logger.Log("Debug", reason);
                    return false;
                }

                // 文件已存在 → 跳过
                if (ftpClient.FileExists(remoteFileName))
                {
                    reason = "文件已存在，跳过上传";
                    Logger.Log("Process", $"{filePathRemote} {reason}");
                    ftpClient.Disconnect();
                    ftpClient.Dispose();
                    return true;
                }

                if (!string.IsNullOrEmpty(remoteDirectory))
                {
                    bool dirCreated = ftpClient.CreateDirectory(remoteDirectory, true);
                    Logger.Log("Debug", dirCreated ? $"创建目录 {remoteDirectory} 成功" : $"目录 {remoteDirectory} 已存在");
                    ftpClient.SetWorkingDirectory(remoteDirectory);
                }

                // 上传内容
                // 错误写法：把 filePathLocal 字符串转成字节
                //using (var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(filePathLocal)))
                //{
                //    uploadResult = ftpClient.UploadStream(contentStream, remoteFileName, FtpRemoteExists.OverwriteInPlace);
                //}

                // 正确写法：读取本地图片文件流！！！
                using (var fileStream = new FileStream(filePathLocal, FileMode.Open, FileAccess.Read))
                {
                    uploadResult = ftpClient.UploadStream(fileStream, remoteFileName, FtpRemoteExists.Skip);
                }                

                ftpClient.Disconnect();
                ftpClient.Dispose();

                // 校验上传结果
                if (uploadResult == FtpStatus.Success)
                {
                    Logger.Log("Debug", $"{remoteFileName} 上传 {filePathRemote} 执行成功");
                    return true;
                }
                else
                {
                    reason = $"上传失败，FTP 返回状态：{uploadResult}";
                    Logger.Log("Debug", reason);
                    return false;
                }
            }
            catch (UriFormatException ex)
            {
                reason = $"FTP 地址格式错误：{ex.Message}（正确格式示例：ftp://192.168.1.100:21/test.txt）";
                Logger.Log("Debug", reason);
                return false;
            }
            catch (Exception ex)
            {
                reason = $"上传执行异常：{ex.Message}";
                Logger.Log("Debug", $"{filePathRemote} 上传执行异常，{ex}");
                return false;
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filePathRemote"></param>
        /// <param name="content"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static bool UploadFileWithValue(string filePathRemote, string content, out string reason)
        {
            reason = string.Empty;

            FtpStatus uploadResult = FtpStatus.Failed;
            try
            {
                // 解析 FTP 地址/端口/目录/文件名 
                Uri ftpUri = new Uri(filePathRemote);
                // 提取核心信息（自动兼容 ftp:// 前缀、端口、路径）
                string ftpHost = ftpUri.Host;
                int ftpPort = ftpUri.Port > 0 ? ftpUri.Port : 21;   // 优先用URL中的端口，无则用默认21（修复硬编码21013的问题）
                string remoteFilePath = ftpUri.AbsolutePath;        // 完整路径（含文件名）
                string remoteDirectory = Path.GetDirectoryName(remoteFilePath).Replace('\\', '/'); // 纯目录路径（修复反斜杠解析bug）
                string remoteFileName = Path.GetFileName(remoteFilePath); // 纯文件名

                // 实例化 FtpClient（动态端口）
                // 把字符串型的 username/password 包装成 NetworkCredential
                NetworkCredential ftpCredentials = new NetworkCredential(username, password);
                // 初始化FtpClient
                var ftpClient = new FtpClient();
                ftpClient.Host = ftpHost;
                ftpClient.Port = ftpPort;
                ftpClient.Credentials = new NetworkCredential(username, password);
                ftpClient.Encoding = Encoding.UTF8;
                ftpClient.Config.DataConnectionType = FtpDataConnectionType.AutoPassive;
                ftpClient.Config.ValidateAnyCertificate = true;// 忽略证书错误（可选）

                // 连接服务器
                ftpClient.Connect();
                if (!ftpClient.IsConnected)
                {
                    reason = "FTP服务器连接失败（主机/端口/账号密码错误）";
                    Logger.Log("Debug", reason);
                    return false;
                }

                // 文件已存在 → 跳过
                if (ftpClient.FileExists(remoteFileName))
                {
                    reason = "文件已存在，跳过上传";
                    Logger.Log("Process", $"{filePathRemote} {reason}");
                    ftpClient.Disconnect();
                    ftpClient.Dispose();
                    return true;
                }

                // 创建目录
                if (!string.IsNullOrEmpty(remoteDirectory))
                {
                    bool dirCreated = ftpClient.CreateDirectory(remoteDirectory, true);
                    Logger.Log("Debug", dirCreated ? $"创建目录 {remoteDirectory} 成功" : $"目录 {remoteDirectory} 已存在");
                    ftpClient.SetWorkingDirectory(remoteDirectory);
                }

                // 上传内容
                using (var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
                {
                    // 新版 UploadStream 无需指定参数名，按顺序传参即可
                    uploadResult = ftpClient.UploadStream(contentStream, remoteFileName, FtpRemoteExists.OverwriteInPlace);
                }

                ftpClient.Disconnect();
                ftpClient.Dispose(); // 显式释放资源

                // 校验上传结果
                if (uploadResult == FtpStatus.Success)
                {
                    Logger.Log("Debug", $"{remoteFileName} 上传 {filePathRemote} 执行成功");
                    return true;
                }
                else
                {
                    reason = $"上传失败，FTP 返回状态：{uploadResult}";
                    Logger.Log("Debug", reason);
                    return false;
                }
            }
            catch (UriFormatException ex)
            {
                reason = $"FTP 地址格式错误：{ex.Message}（正确格式示例：ftp://192.168.1.100:21/test.txt）";
                Logger.Log("Debug", reason);
                return false;
            }
            catch (Exception ex)
            {
                reason = $"上传执行异常：{ex.Message}";
                Logger.Log("Debug", $"{filePathRemote} 上传执行异常，{ex}");
                return false;
            }
        }
        #endregion

        #region WinSCP

        #endregion

    }


}
