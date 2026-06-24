using LT.Common.Logger;
using LT.Common.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Business
{
    internal class BusinessHelper
    {
        private static readonly BusinessHelper instance = new BusinessHelper();
        private BusinessHelper()
        {

        }

        public static BusinessHelper Instance
        {
            get { return instance; }
        }

        #region FTP与本地文件操作
        public void CreateFolders(string baseDir, IEnumerable<string> folderLevels)
        {
            string fullPath = baseDir;
            foreach (var folder in folderLevels)
            {
                fullPath = Path.Combine(fullPath, folder).Replace("\\", "/");
            }

            bool success = false;

            // 一次性创建
            try
            {
                FtpHelper.MakeDir(fullPath, false);
                success = true;
                Logger.Log("Process", $"[FTP目录创建成功] {fullPath}");
            }
            catch (Exception ex)
            {
                Logger.Log("Warn", $"[FTP目录创建][一次性创建失败，切换逐级] {fullPath} => {ex.Message}");
            }

            // 逐级创建
            if (!success)
            {
                try
                {
                    string subPath = string.Join("/", folderLevels);
                    FtpHelper.MakeDirs(baseDir, subPath, false);
                    Logger.Log("Process", $"[FTP目录创建成功] {fullPath}");
                }
                catch (Exception ex)
                {
                    Logger.Log("Error", $"[FTP目录创建][创建失败] {fullPath} => {ex.Message}");
                }
            }
        }

        /// <summary>
        /// FTP上传文本文件（XML）
        /// </summary>
        public bool UploadFTPFile(int maxTryCount, string uri, string content, out string reason)
        {
            reason = "";
            int retryCount = 1;

            do
            {
                try
                {
                    if (FtpHelper.UploadFileWithValue(uri, content, out reason))
                    {
                        Logger.Log("Process", $"文件{uri}上传第{retryCount}次成功");
                        return true;
                    }

                    Logger.Log("Process", $"文件{uri}上传第{retryCount}次失败：{reason}");
                    if (retryCount >= maxTryCount) break;

                    Thread.Sleep(3000);
                    retryCount++;
                }
                catch (Exception ex)
                {
                    reason = ex.Message;
                    Logger.Log("Error", $"文件{uri}上传异常：{ex}");
                    retryCount++;
                    if (retryCount > maxTryCount) break;
                    Thread.Sleep(3000);
                }
            } while (retryCount <= maxTryCount);

            return false;
        }

        /// <summary>
        /// FTP上传文件（图片）
        /// </summary>
        public bool UploadFTPFileWithPath(int maxTryCount, string uri, string fileLocalPath, out string reason)
        {
            reason = "";

            // 第一步：先判断【本地文件是否存在】，提前区分SKIP原因
            if (!File.Exists(fileLocalPath))
            {
                reason = $"上传失败：本地文件不存在，路径：{fileLocalPath}";
                Logger.Log("Error", $"【SKIP原因：本地文件不存在】{uri} 上传失败：{reason}");
                return false;
            }

            int retryCount = 1;
            do
            {
                try
                {
                    if (FtpHelper.UploadFileWithPath(uri, fileLocalPath, out reason))
                    {
                        Logger.Log("Process", $"文件{uri}上传第{retryCount}次成功");
                        return true;
                    }

                    // 第二步：上传失败，明确标记是【FTP问题】
                    Logger.Log("Error", $"【SKIP原因：FTP问题】文件{uri}上传第{retryCount}次失败：{reason}");
                    if (retryCount >= maxTryCount) break;

                    Thread.Sleep(3000);
                    retryCount++;
                }
                catch (Exception ex)
                {
                    reason = ex.Message;
                    Logger.Log("Error", $"文件{uri}上传异常：{ex}");
                    retryCount++;
                    if (retryCount > maxTryCount) break;
                    Thread.Sleep(3000);
                }
            } while (retryCount <= maxTryCount);

            // 最终失败：标记为【FTP问题】
            reason = $"上传失败：FTP服务器/路径/权限问题，已重试{maxTryCount}次";
            Logger.Log("Error", $"【SKIP原因：FTP问题（重试结束）】文件{uri}上传最终失败：{reason}");
            return false;
        }

        /// <summary>
        /// 写入本地文件
        /// </summary>
        public bool WriteLocalFile(int maxTryCount, string filePath, string content, out string reason)
        {
            reason = "";

            // 文件已存在 → 直接跳过
            if (File.Exists(filePath))
            {
                reason = "文件已存在，跳过重复写入";
                Logger.Log("Process", $"文件{filePath}已存在，{reason}");
                return true; // 跳过不算失败，直接返回成功
            }

            int retryCount = 1;

            do
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    if (LocalFileOperate.WriteFile(content, filePath, out reason))
                    {
                        Logger.Log("Process", $"文件{filePath}写入第{retryCount}次成功");
                        return true;
                    }

                    Logger.Log("Process", $"文件{filePath}写入第{retryCount}次失败：{reason}");
                    if (retryCount >= maxTryCount) break;

                    Thread.Sleep(3000);
                    retryCount++;
                }
                catch (Exception ex)
                {
                    reason = ex.Message;
                    Logger.Log("Error", $"文件{filePath}写入异常：{ex}");
                    retryCount++;
                    if (retryCount > maxTryCount) break;
                    Thread.Sleep(3000);
                }
            } while (retryCount <= maxTryCount);

            return false;
        }
        #endregion

        #region PLC告警相关
        public Action<string, int, string, PlcCommunicator> OnWarring;

        public void DoWarring(string address, int value, string name, PlcCommunicator plcCommunicator)
        {
            OnWarring?.Invoke(address, value, name, plcCommunicator);
        }

        private static readonly object objLock = new object();
        private void Dowarring(string address, int value, string name, PlcCommunicator plcCommunicator)
        {
            lock (objLock)
            {
                DoQualityWarning(address, value, name, plcCommunicator);
            }
        }

        public void DoQualityWarning(string address, int value, string name, PlcCommunicator plcCommunicator)
        {
            if (plcCommunicator == null)
            {
                Logger.Log("Warning", "PLC通信实例为空，无法写入告警");
                return;
            }

            if (plcCommunicator.Write(address, value, out string reason))
            {
                Logger.Log("QualityWarning", $"向PLC写入{name}告警,地址:{address},数值:{value}成功");
            }
            else
            {
                Logger.Log("QualityWarning", $"向PLC写入{name}告警,地址:{address},数值:{value}失败,原因:{reason}");
            }
        }
        #endregion

        #region 数据处理
        /// <summary>
        /// 获取转换原点后坐标X
        /// </summary>
        public double GetAoiDefectsPosX(double posX, double GridX, Location defectOriginOfCoordinates)
        {
            double pos_X = 0.0;
            switch (defectOriginOfCoordinates)
            {
                case Location.左下角:
                    pos_X = posX;
                    break;
                case Location.右上角:
                case Location.右下角:
                    pos_X = Math.Abs(GridX - posX);
                    break;
                default:
                    pos_X = posX;
                    break;
            }
            return pos_X;
        }

        /// <summary>
        /// 获取转换原点后坐标Y
        /// </summary>
        public double GetAoiDefectsPosY(double posY, double GridY, Location defectOriginOfCoordinates)
        {
            double pos_Y = 0.0;
            switch (defectOriginOfCoordinates)
            {
                case Location.左下角:
                case Location.右下角:
                    pos_Y = Math.Abs(GridY - posY);
                    break;
                case Location.右上角:
                    pos_Y = posY;
                    break;
                default:
                    pos_Y = posY;
                    break;
            }
            return pos_Y;
        }

        public double GetSurfaceDefectsPosX(double posLeft, double posRight, Location defectOriginOfCoordinates)
        {
            switch (defectOriginOfCoordinates)
            {
                case Location.右上角:
                case Location.右下角:
                    return posRight;
                default:
                    return posLeft;
            }
        }

        public double GetSurfaceDefectsPosY(double posTop, double posBottom, Location defectOriginOfCoordinates)
        {
            switch (defectOriginOfCoordinates)
            {
                case Location.左下角:
                case Location.右下角:
                    return posBottom;
                default:
                    return posTop;
            }
        }
        #endregion
    }


    public enum Location
    {
        正面,
        背面,
        侧边,
        左上角,
        左下角,
        右上角,
        右下角
    }
}
