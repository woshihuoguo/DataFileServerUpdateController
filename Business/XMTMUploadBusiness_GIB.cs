using Business;
using Frame;
using LT.Common.Logger;
using LT.Common.Net;
using LT.SuperTranCockpit.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Business
{
    public class XMTMUploadBusiness_GIB : UpLoadBusiness
    {
        private bool running = true;
        private XMTMConfig config;

        // 业务运行变量
        private string PannelID = "";
        private int _imageUploadFailCount = 0;// 上传次数最大限制

        // 业务配置缓存
        private string _eqpUnitId = "";
        private string _lineId = "";
        private string _recipeName = "";
        private string _procId = "";
        private string _operId = "";         // 操作员ID（HEADER用）
        private string _operatorLa = "";     // 操作员名称（BODY/AP_INFO用）
        private int _barCodeLength = 0;
        private int _maxTry = 0;
        private int _defectUploadMaxCount = 0;
        private bool _isUploadFileLocal = false;
        private string _localImageRoot = "";
        private string _imgUploadRootPath = "";
        private int _maxImageUploadFailCount = 0;

        ICW_LCD_SurfaceResultEntity surfaceResult_GIB;

        /// <summary>
        /// 初始化配置
        /// </summary>
        public override void InitConfig()
        {
            // FTP
            FtpHelper.username = config.Username;
            FtpHelper.password = config.Password;
            FtpHelper.host = config.RootPath;

            // 设备配置
            _eqpUnitId = config.GIB_EQPUNIT_ID;
            _lineId = config.LINE_ID;
            _recipeName = config.RECIPE_NAME;
            _procId = config.PROC_ID;

            // 操作员配置（从配置读取）
            _operId = config.OPER_ID;
            _operatorLa = config.OPERATOR_LA;

            // 业务规则
            _barCodeLength = config.BarCodeLength;
            _maxTry = config.MaxRetry;
            _defectUploadMaxCount = config.DefectUploadMaxCount;
            _isUploadFileLocal = config.IsUploadFileLocal;
            _localImageRoot = config.LocalImageRoot;
            _imgUploadRootPath = config.ImgUploadRootPath;
            _maxImageUploadFailCount = config.MaxImageUploadFailCount;

            Logger.Log("Process", "配置初始化完成");
        }

        /// <summary>
        /// 启动上传业务逻辑
        /// </summary>CreateFolders
        public override void Start()
        {
            // 始化配置 本类变量赋值
            InitConfig();

            #region 查询开始,输出日志
            Logger.Log("Process", $"\r\n----------------------------------------------------");
            Logger.Log("Process", $"【复判】开始查询SendFlag==0 MES更新的记录");
            #endregion

            // 循环执行上传逻辑
            while (running)
            {
                Thread.Sleep(500);
                if (pauseFlag) continue;

                try
                {
                    #region 数据查询
                  
                    #region GIB 查询表CellDFSInfoEntity 查询表CellDFSInfoEntity.SendFlag==0的最新一条
                    //读取summary最新一条  // 读取最新检测汇总记录
                    if (!DateBaseOperation.DateBaseOperation.GetLastCellDFSInfoEntity(this.Frame, 0, out CellDFSInfoEntity cellDFSInfoEntity, out string reason))
                    {
                        //读取失败，不进行上传
                        //Logger.Log("Process", $"【复判】未找到SendFlag==0更新的检测记录");
                        continue;
                    }
                    #endregion

                    #region 判断UniqueId,GetPanelInfo,PannelID是否为空
                    if (string.IsNullOrEmpty(cellDFSInfoEntity.UniqueID))
                    {
                        //数据异常，不进行上传
                        Logger.Log("Process", "【复判】SendFlag==0的UniqueID为空，无法上报");
                        Save();
                        if (!DateBaseOperation.DateBaseOperation.SetLastCellDFSInfoEntity(this.Frame, 1, cellDFSInfoEntity, out reason))
                        {
                            Logger.Log("Process", $"【复判】SetLastCellDFSInfoEntity，REASON:{reason}");
                        }
                        continue;
                    }

                    GetPanelInfo(cellDFSInfoEntity.UniqueID, out string pannelID);

                    PannelID = pannelID.Trim();
                    if (string.IsNullOrEmpty(PannelID))
                    {
                        //数据异常，不进行上传
                        if (!DateBaseOperation.DateBaseOperation.SetLastCellDFSInfoEntity(this.Frame, 1, cellDFSInfoEntity, out reason))
                        {
                            Logger.Log("Process", $"【复判】SetLastCellDFSInfoEntity，REASON:{reason}");
                        }
                        Logger.Log("Process", $"【复判】UniqueId:{cellDFSInfoEntity.UniqueID}的PannelID为空，无法上报");
                        Save();
                        continue;
                    }

                    if ((cellDFSInfoEntity.UniqueID != null) && (PannelID != null))
                    {
                        Logger.Log("Process", $"【复判】复判：PannelID为:{PannelID},UniqueId为:{cellDFSInfoEntity.UniqueID}");
                    }
                    else
                    {
                        Logger.Log("Process", $"【复判】未找到MES SendFlag==0更新的检测记录");
                        continue;
                    }
                    #endregion

                    #region 判断 产品条码 是否 符合设置要求
                    // 条件：不是指定长度  或者  不包含字母 → 不上报
                    if ((PannelID.Length != _barCodeLength) || (!PannelID.Any(char.IsLetter)))
                    {
                        //数据异常，不进行上传
                        Logger.Log("Process", $"【复判】找到PannelID:{PannelID},不符合PannelID标准{(PannelID.Length != _barCodeLength) || (!PannelID.Any(char.IsLetter))},DFS不上报");
                        Save();
                        continue;
                    }
                    //Logger.Log("Process", $"找到产品,码为:{PannelID},符合设定PannelID标准,正常上报");
                    #endregion

                    #region 根据 UniqueId 去数据表 ivs_lcd_surfaceresult 中查询数据  Remark：确认数据库是否存在UniqueId索引 提高查询效率
                    if (DateBaseOperation.DateBaseOperation.GetICW_LCD_SurfaceResultEntityByUniqueID(this.Frame, cellDFSInfoEntity.UniqueID,
                       out ICW_LCD_SurfaceResultEntity surfaceResult, out reason) == false)
                    {
                        surfaceResult = new ICW_LCD_SurfaceResultEntity();
                        Logger.Log("Process", $"【复判】找不到UniqueId:{cellDFSInfoEntity.UniqueID}的ICW_LCD_SurfaceResultEntity结果,REASON:{reason}");
                    }
                    else
                    {
                        surfaceResult_GIB = surfaceResult;
                    }
                    #endregion

                    #region 根据 UniqueId 去数据表 ivs_lcd_surfacedefect 中查询所有的主检缺陷数据信息 和 主检缺陷图片  Remark：确认数据库是否存在UniqueId索引 提高查询效率
                    if (DateBaseOperation.DateBaseOperation.GetICW_LCD_SurfaceDefectEntityByUniqueID(this.Frame, cellDFSInfoEntity.UniqueID,
                      out List<ICW_LCD_SurfaceDefectEntity> surfaceDects, out reason) == false)
                    {
                        surfaceDects = new List<ICW_LCD_SurfaceDefectEntity>();
                        Logger.Log("Process", $"【复判】找不到产品{PannelID}的ICW_LCD_SurfaceDefectEntity结果,REASON:{reason}");
                    }
                    #endregion
                    #endregion

                    #region 2. 构建上传数据模型
                    string panelId = PannelID;
                    string endTime = surfaceResult.StopTime_ManualReview?? string.Empty;
                    string currentDate = DateTime.Now.ToString("yyyyMMdd");
                    string currentMonth = DateTime.Now.ToString("MM");
                    string currentDay = DateTime.Now.ToString("dd");

                    // 找不到缺陷时，给一个空列表，不给他为null
                    if (surfaceDects == null)
                        surfaceDects = new List<ICW_LCD_SurfaceDefectEntity>();
                    // 构建PNL XML模型
                    PANEL panelModel = BuildPanelModel(surfaceResult, surfaceDects);
                    string PNLContext = SerializePanelToXml(panelModel);
                    #endregion

                    #region 3. 路径与文件名构建
                    // 3.1 XML文件路径配置
                    //string xmlFileName = $"{panelId}_{_procId}_{endTime}.{_eqpUnitId}".ToUpper();// 只改这一行
                    string xmlFileName = $"{panelId}_{_procId}_{currentDate}_{(endTime == string.Empty ? string.Empty : endTime.Substring(8))}.{_eqpUnitId}".ToUpper();

                    //A路径 FTP 协议统一使用：/data0h2/HN_DFS/DEFECT 例如： ftp://localhost/data0h2/HN_DFS/DEFECT
                    string IndexChangePathA = "HOST";
                    string[] xmlFolderLevels_HOST = { _recipeName };
                    string xmlRemoteDirA = Path.Combine(config.RootPath, IndexChangePathA, Path.Combine(xmlFolderLevels_HOST)).Replace("\\", "/");
                    string XMLRemoteFilePathA = $"{xmlRemoteDirA}/{xmlFileName}";
                    //B路径
                    string IndexChangePathB = "INSPECTOR";
                    string[] xmlFolderLevels_INSPECTOR = { currentMonth, currentDay, _recipeName, _eqpUnitId };
                    string xmlRemoteDirB = Path.Combine(config.RootPath, IndexChangePathB, Path.Combine(xmlFolderLevels_INSPECTOR)).Replace("\\", "/");
                    string XMLRemoteFilePathB = $"{xmlRemoteDirB}/{xmlFileName}";

                    // 本地备份路径（双位置）
                    string defectLocalPathA = Path.Combine("C:\\DEFECT\\HOST\\RECIPE", xmlFileName).ToUpper();
                    string defectLocalPathB = Path.Combine("C:\\DEFECT\\INSPECTOR", currentMonth, currentDay, _recipeName, _eqpUnitId, xmlFileName).ToUpper();

                    // 图片根目录判断
                    string imgRootDir = surfaceDects.Any(d => !string.IsNullOrEmpty(d.Code_AOI)) ? "DEFECT_IMAGE" : "DEFECT_IMAGE2";

                    string[] imgFolderLevels = { "IMAGE", imgRootDir, currentMonth, currentDay, _recipeName, _eqpUnitId, panelId };
                    string imgRemoteDir = Path.Combine(_imgUploadRootPath, Path.Combine(imgFolderLevels)).Replace("\\", "/");
                    #endregion

                    #region 4. 生成XML内容并本地备份
                    //string PNLContext = SerializePanelToXml(panelModel);

                    // 本地文件备份（双位置）
                    if (_isUploadFileLocal)
                    {
                        // 确保目录存在
                        Directory.CreateDirectory(Path.GetDirectoryName(defectLocalPathA));
                        Directory.CreateDirectory(Path.GetDirectoryName(defectLocalPathB));

                        // 写入本地文件
                        BusinessHelper.Instance.WriteLocalFile(_maxTry, defectLocalPathA, PNLContext, out reason);
                        BusinessHelper.Instance.WriteLocalFile(_maxTry, defectLocalPathB, PNLContext, out reason);
                    }
                    #endregion

                    #region 5. 上传 Panel INDEX 文件到FTP
                    // 创建XML远程目录
                    if (!string.IsNullOrEmpty(xmlRemoteDirA))
                    {
                        bool result = FtpHelper.MakeDirs(config.RootPath, xmlRemoteDirA, false);
                        if (result == false)
                        {
                            Logger.Log("Error", $"【复判】远程服务器创建目录：{xmlRemoteDirA} 目录失败！！！");
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(xmlRemoteDirB))
                    {
                        bool result = FtpHelper.MakeDirs(config.RootPath, xmlRemoteDirB, false);
                        if (result == false)
                        {
                            Logger.Log("Error", $"【复判】远程服务器创建目录：{xmlRemoteDirB} 目录失败！！！");
                            continue;
                        }
                    }

                    if (!BusinessHelper.Instance.UploadFTPFile(_maxTry, XMLRemoteFilePathA, PNLContext, out reason))
                    {
                        Logger.Log("Error", $"【复判】XML文件上传失败，终止当前批次处理，原因：{reason}");
                        continue;
                    }
                    if (!BusinessHelper.Instance.UploadFTPFile(_maxTry, XMLRemoteFilePathB, PNLContext, out reason))
                    {
                        Logger.Log("Error", $"【复判】PanelINDEX_INSPECTOR文件上传失败，终止当前批次处理，原因：{reason}");
                        continue;
                    }
                    #endregion

                    #region 6. 如果存在缺陷，上传图片文件
                    bool imageUploadOk = true;

                    if (surfaceDects.Count > 0)  //暂不上传图像
                    {
                        // 创建图片远程目录
                        bool result = FtpHelper.MakeDirs(config.ImgUploadRootPath, imgRemoteDir, false);
                        if (result == false)
                        {
                            Logger.Log("Error", $"【复判】远程服务器创建目录：{imgRemoteDir} 目录失败！！！");
                            continue;
                        }

                        // 上传缺陷小图(CropImage)
                        //string testFileName = @"\\192.168.100.101\LCDSystemTS\20260506\LG_Check_正面擦前线扫_PC2\2026_05_06_13_56_25_000001_20260506135657AA_2026-05-06 13.58.23.928_产品0_治具0_SurImg_不良品\DefectImage\擦前线光_0[3153_382_3253_490]_BOE-05.bmp";

                        string testFileName = @"D:\123.jpg";
                        if (!UploadDefectImages(surfaceResult, surfaceDects, imgRemoteDir, endTime, ref reason, testFileName))
                        {
                            Logger.Log("Error", $"【复判】缺陷小图(CropImage)上传失败：{reason}");
                            imageUploadOk = false;
                        }


                        // 原图(ORGImage)
                        if (!UploadOrgImage(surfaceResult, imgRemoteDir, endTime, imgFolderLevels, ref reason, testFileName))
                        {
                            Logger.Log("Error", $"【复判】原图(ORGImage)上传失败：{reason}");
                            imageUploadOk = false;
                        }
                    }

                    if (!imageUploadOk)
                    {
                        _imageUploadFailCount++;
                        Logger.Log("Process", $"【复判】图片上传失败次数：{_imageUploadFailCount}/{_maxImageUploadFailCount}");

                        if (_imageUploadFailCount >= _maxImageUploadFailCount)
                        {
                            Logger.Log("Process", $"【复判】达到最大失败次数，自动跳过产品 {PannelID}，SysId 前进");
                            _imageUploadFailCount = 0;

                            if (!DateBaseOperation.DateBaseOperation.SetLastCellDFSInfoEntity(this.Frame, 1, cellDFSInfoEntity, out reason))
                            {
                                Logger.Log("Process", $"【复判】SetLastCellDFSInfoEntity，REASON:{reason}");
                            }
                            Save();
                        }
                        continue;
                    }

                    // 上传成功，重置计数
                    _imageUploadFailCount = 0;
                    #endregion

                    #region SendFlag=1
                    Logger.Log("Process", $"【复判】产品{panelId}上传流程全部完成");
                    if (!DateBaseOperation.DateBaseOperation.SetLastCellDFSInfoEntity(this.Frame, 1, cellDFSInfoEntity, out reason))
                    {
                        Logger.Log("Process", $"【复判】SetLastCellDFSInfoEntity，REASON:{reason}");
                    }
                    Save();
                    #endregion
                }
                catch (Exception ex)
                {
                    Logger.Log("Error", "【复判】XMTMUploadBusiness_GIB执行流程出现异常：" + ex.Message, ex);
                }
            }
        }

        #region 基础业务控制
        public override void Stop()
        {
            running = false;
        }

        private bool pauseFlag = false;
        public override void Pause()
        {
            pauseFlag = true;
        }

        public override void Restart()
        {
            pauseFlag = false;
        }

        private void Save()
        {
            JsonSerializerHelper<XMTMConfig>.Save(config, out string reason);
        }

        public override BusinessConfig GetConfig()
        {
            return config;
        }

        public override void SetConfig(BusinessConfig businessConfig)
        {
            if (businessConfig is XMTMConfig xmtmConfig)
            {
                config = xmtmConfig;
            }
        }
        #endregion

        #region 构建PNL XML模型
        /// <summary>
        /// 构建PNL XML模型
        /// </summary>
        private PANEL BuildPanelModel(ICW_LCD_SurfaceResultEntity surfaceResultEntity, List<ICW_LCD_SurfaceDefectEntity> gibDefects)
        {
            string panelId = PannelID;
            string startTime = surfaceResultEntity.StartTime_ManualReview/*.ToString("yyyyMMddHHmmss")*/;
            string endTime = surfaceResultEntity.StopTime_ManualReview/*.ToString("yyyyMMddHHmmss")*/;
            string judgeResult = (surfaceResultEntity.ReviewResult_Worker == "OK") ? "G" : "N";
            string defectReason = surfaceResultEntity.Code_ManualReview ?? string.Empty;

            var panel = new PANEL
            {
                HEADER = new HEADER
                {
                    KEY_ID = $"{panelId}_{startTime}",
                    PANEL_ID = panelId,
                    SERIAL_NO = panelId,
                    TOTAL_INPUT = (gibDefects?.Count ?? 0).ToString(),
                    LINE_ID = _lineId,
                    MACHINE_ID = _eqpUnitId,
                    UNIT_ID = _eqpUnitId,
                    OPER_ID = _operId,
                    PROC_ID = _procId,
                    RECIPE_ID = _recipeName,
                    JUDGE = judgeResult,
                    INSP_TIME = new INSP_TIME { START = startTime, END = endTime }
                },

                BODY = new BODY
                {
                    AP_INFO = new AP_INFO
                    {
                        PROC = new PROC { FA = _procId },
                        MACHINE = new MACHINE { FA = _eqpUnitId },
                        TIME = new TIME { FA = startTime },
                        OPERATOR = new OPERATOR { FA = _operatorLa },
                        JUDGE = new JUDGE { FA = judgeResult },

                        REASON = new REASON
                        {
                            FA_FINAL = judgeResult == "G" ? "OK" : (defectReason ?? "UNKNOWN")
                        },

                        WORKTABLE = new WORKTABLE { FA = (judgeResult == "G") ? "1" : "7" },
                        TYPE = new TYPE()
                    },
                    JUDGE_INFO = new JUDGE_INFO
                    {
                        LATEST = new LATEST
                        {
                            JUDGE = judgeResult,
                            REASON = defectReason
                        }
                    },
                    DEFECT_NO = new DEFECT_NO { TOTAL = gibDefects.Count }
                },

                DEFECT_INFO = new DEFECT_INFO
                {
                    DEFECT = new List<DEFECT>()
                }
            };

            /*
            // FA_R 动态无限生成
            if (gibDefects != null && gibDefects.Count > 0)
            {
                var doc = new XmlDocument();
                var attributes = new List<XmlAttribute>();

                for (int i = 0; i < gibDefects.Count; i++)
                {
                    string code = gibDefects[i].Code_ManualReview ?? "";
                    var attr = doc.CreateAttribute($"FA_{i + 1}");
                    attr.Value = code;
                    attributes.Add(attr);
                }

                panel.BODY.AP_INFO.REASON.DynamicAttributes = attributes.ToArray();
            }
            */

            // 生成缺陷（每个缺陷 1 张图 ）
            foreach (var d in gibDefects)
            {
                var imgList = new List<IMG>();

                int imageCount = 1;
                for (int seq = 1; seq <= imageCount; seq++)
                {
                    string imgName = $"{panelId}_{_eqpUnitId}_{_procId}_{d.Pos_x:0}_{d.Pos_y:0}_{judgeResult}_{(gibDefects[seq].Code_ManualReview ?? string.Empty)}_{d.DefIndex + 1}_{seq}.JPG";
                    imgList.Add(new IMG { SEQ = seq, NAME = imgName });
                }

                var defect = new DEFECT
                {
                    SHOP = "FA",
                    DEF_NO = d.DefIndex + 1,
                    PNL_START_TIME = startTime,
                    PNL_END_TIME = endTime,
                    PROC_ID = _procId,
                    MACHINE_ID = _eqpUnitId,
                    JUDGE = judgeResult,
                    REASON = d.Code_AOI ?? (surfaceResultEntity.Code_ManualReview ?? string.Empty),
                    CLASSIFY = d.CusDefName ?? (surfaceResultEntity.Code_ManualReview ?? string.Empty),
                    IMAGE_FILE_NO = 1,
                    IMG = imgList
                };

                // 缺陷属性
                if (surfaceResult_GIB != null && !string.IsNullOrEmpty(surfaceResult_GIB.XMLInfo))
                {
                    FillFullDefectData(defect, d);
                }

                panel.DEFECT_INFO.DEFECT.Add(defect);
            }

            return panel;
        }

        /// <summary>
        /// 序列化PNL模型为XML字符串（已修复：utf-8 + standalone="yes"）
        /// </summary>
        private string SerializePanelToXml(PANEL panel)
        {
            string endTime = panel.HEADER.INSP_TIME.END;
            string datePart = endTime.Substring(0, 8);    // YYYYMMDD
            string timePart = endTime.Substring(8, 6);    // HHMMSS
            string fileName = $"{panel.HEADER.PANEL_ID}_{_procId}_{datePart}_{timePart}.{_eqpUnitId}".ToUpper();

            var doc = new XmlDocument();
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            doc.AppendChild(xmlDeclaration);

            var root = doc.CreateElement("DEFECT_FILE");
            XmlAttribute attr = doc.CreateAttribute("NAME");
            attr.Value = fileName;
            root.Attributes.Append(attr);
            doc.AppendChild(root);

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var serializer = new XmlSerializer(typeof(PANEL));

            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, panel, ns);
                ms.Position = 0;
                var panelDoc = new XmlDocument();
                panelDoc.Load(ms);
                doc.DocumentElement.AppendChild(doc.ImportNode(panelDoc.DocumentElement, true));
            }

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                Encoding = Encoding.UTF8
            };

            using (var writer = XmlWriter.Create(sb, settings))
            {
                doc.WriteTo(writer);
            }

            return $"<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>{Environment.NewLine}{sb}";
        }

        /// <summary>
        /// 获取面板扩展PannelID信息
        /// </summary>
        private void GetPanelInfo(string uniqueId, out string pannelID)
        {
            pannelID = "";
            string reason = "";

            if (DateBaseOperation.DateBaseOperation.GetPanelInfoByUniqueId(this.Frame, uniqueId, out List<PanelInfoEntity> entities, out reason))
            {
                if (entities?.Count > 0)
                {
                    // 过滤出 ParamName 为 PANEL_ID 的行，再取 ParamValue
                    pannelID = entities.FirstOrDefault(x => x.ParamName == "PanelId")?.ParamValue ?? "";
                }
            }
        }
        #endregion

        #region ICW_LCD_SurfaceDefects具体内容
        /// <summary>
        /// 填充 DEFECT 所有字段（按最新规则：有值赋值，无值填0，两次GRAYLEVELL_RATE=0）
        /// </summary>
        private void FillFullDefectData(DEFECT defect, ICW_LCD_SurfaceDefectEntity d)
        {
            // 缺陷坐标
            defect.G_DFTSX = (int)d.Pos_x;
            defect.G_DFTSY = (int)d.Pos_y;
            defect.G_DFTEX = (int)(d.Pos_x + d.Pos_width);
            defect.G_DFTEY = (int)(d.Pos_y + d.Pos_height);
        }
        #endregion

        #region 上传 方法
        /// <summary>
        /// 上传缺陷图片
        /// </summary>
        private bool UploadDefectImages(ICW_LCD_SurfaceResultEntity result, List<ICW_LCD_SurfaceDefectEntity> defects,
            string imgRemoteDir, string endTime, ref string reason, string imgLocalPath)
        {
            int uploadCount = 0;
            string panelId = PannelID;
            string judgeResult = result.ReviewResult_Worker == "OK" ? "G" : "N";

            foreach (var defect in defects)
            {
                if (uploadCount >= _defectUploadMaxCount) break;

                int defectSerialNo = defect.DefIndex + 1;
                string defectCode = defect.Code_AOI ?? result.Code_ManualReview ?? "";
                //string imgFileName = $"{panelId}_{_eqpUnitId}_{_procId}_{DateTime.Now.ToString("yyyyMMdd")}_gate_{judgeResult}_{defectCode}_{defectSerialNo}.JPG".ToUpper();
                // 和 XML内命名改为一致
                string imgFileName = $"{panelId}_{_eqpUnitId}_{_procId}_{defect.Pos_x:0}_{defect.Pos_y:0}_{judgeResult}_{defectCode}_{defectSerialNo}_{this.config.Lane}.JPG".ToUpper();

                imgLocalPath = imgLocalPath != null ? imgLocalPath : Path.Combine(_localImageRoot, defect.ImagePath ?? "").ToUpper();
                string imgRemotePath = $"{imgRemoteDir}/{imgFileName}";

                if (!BusinessHelper.Instance.UploadFTPFileWithPath(_maxTry, imgRemotePath, imgLocalPath, out reason))
                {
                    Logger.Log("Error", $"缺陷小图{imgFileName}上传失败：{reason}");
                    return false;
                }

                uploadCount++;
                Thread.Sleep(5);
            }

            return true;
        }


        /// <summary>
        /// 上传原图
        /// </summary>
        private bool UploadOrgImage(ICW_LCD_SurfaceResultEntity result,
            string imgRemoteDir, string endTime, string[] imgFolderLevels, ref string reason, string imgLocalPath)
        {
            string markImgPath = GetMarkOrgPath(result);
            //if (string.IsNullOrEmpty(markImgPath))
            //{
            //    Logger.Log("Warning", "未解析到原图路径，跳过上传");
            //    return true;
            //}

            string imageFileName_MarkImg = $"{PannelID}_{_eqpUnitId}_{endTime}_{this.config.Lane}.jpg".ToUpper();
            imgLocalPath = imgLocalPath != null ? imgLocalPath : Path.Combine(_localImageRoot, markImgPath).ToUpper();
            string imgRemotePath = $"{imgRemoteDir}/{imageFileName_MarkImg}";

            if (!BusinessHelper.Instance.UploadFTPFileWithPath(_maxTry, imgRemotePath, imgLocalPath, out reason))
            {
                Logger.Log("Error", $"原图上传失败：{reason}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 解析原图路径
        /// </summary>
        private string GetMarkOrgPath(ICW_LCD_SurfaceResultEntity result)
        {
            try
            {
                if (result == null || string.IsNullOrEmpty(result.XMLInfo)) return "";
                XElement root = XElement.Parse(result.XMLInfo);
                XElement markImgElement = root.Element("ImgPath");
                return markImgElement?.Value ?? "";
            }
            catch (XmlException ex)
            {
                Logger.Log("Error", $"XML解析错误：{ex.Message}");
                return "";
            }
        }
        #endregion
    }
}