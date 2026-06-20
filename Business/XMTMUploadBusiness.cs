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
    public class XMTMUploadBusiness : UpLoadBusiness
    {
        private bool running = true;
        private long currentSysId = 0;
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

        ICW_LCD_SurfaceResultEntity surfaceResult_AOI;

        /// <summary>
        /// 初始化配置
        /// </summary>
        private void InitConfig()
        {
            // FTP
            FtpHelper.username = config.Username;
            FtpHelper.password = config.Password;
            FtpHelper.host = config.RootPath;

            // 设备配置
            _eqpUnitId = config.EQPUNIT_ID;
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

            // 进度
            currentSysId = config.CurrentSysId;

            Logger.Log("Process", "配置初始化完成");
        }

        /// <summary>
        /// 启动上传业务逻辑
        /// </summary>CreateFolders
        public override void Start()
        {
            // 始化配置 本类变量赋值
            InitConfig();

            // 循环执行上传逻辑
            while (running)
            {
                Thread.Sleep(500);
                if (pauseFlag) continue;

                try
                {
                    #region 1. 数据查询

                    #region 查询开始,输出日志

                    Logger.Log("Process", $"\r\n----------------------------------------------------");
                    Logger.Log("Process", $"开始查询比{currentSysId}更新的检测记录");
                    #endregion

                    #region 查询表inspectionresult 通过 sysid 获取 uniqueID，PanelID  最新一条
                    //读取summary最新一条
                    // 读取最新检测汇总记录
                    if (!DateBaseOperation.DateBaseOperation.GetLastInspectSummaryEntity(currentSysId, out InspectSummaryEntity inspectSummary, out string reason))
                    {
                        //读取失败，不进行上传
                        Logger.Log("Process", $"未找到比{currentSysId}更新的检测记录");
                        continue;
                    }
                    #endregion

                    #region 判断UniqueId,GetPanelInfo,PannelID是否为空
                    if (string.IsNullOrEmpty(inspectSummary.UniqueId))
                    {
                        //数据异常，不进行上传
                        Logger.Log("Process", $"SysId:{currentSysId}的UniqueId为空，无法上报");
                        currentSysId = inspectSummary.SysId;
                        Save();
                        continue;
                    }

                    GetPanelInfo(inspectSummary.UniqueId, out string pannelID);
                    PannelID = pannelID.Trim();
                    if (string.IsNullOrEmpty(PannelID))
                    {
                        //数据异常，不进行上传
                        Logger.Log("Process", $"UniqueId:{inspectSummary.UniqueId}的PannelID为空，无法上报");
                        currentSysId = inspectSummary.SysId;
                        Save();
                        continue;
                    }

                    if ((inspectSummary.SysId != null) && (inspectSummary.Barcode != null) && (inspectSummary.UniqueId != null) && (PannelID != null))
                    {
                        Logger.Log("Process", $"当前查询出的SysId为:{inspectSummary.SysId},产品码为:{inspectSummary.Barcode},PannelID为:{PannelID},UniqueId为:{inspectSummary.UniqueId}");
                    }
                    else
                    {
                        Logger.Log("Process", $"未找到比{currentSysId}更新的检测记录");
                        continue;
                    }
                    //Logger.Log("Process", $"找到产品{PannelID}的UniqueId为:{inspectSummary.UniqueId},不为空,正常上报");
                    #endregion

                    #region 判断 产品条码 是否 符合设置要求
                    // 条件：不是指定长度  或者  不包含字母 → 不上报
                    if ((PannelID.Length != _barCodeLength) || (!PannelID.Any(char.IsLetter)))
                    {
                        //数据异常，不进行上传
                        Logger.Log("Process", $"找到PannelID:{PannelID},不符合PannelID标准{(PannelID.Length != _barCodeLength) || (!PannelID.Any(char.IsLetter))},DFS不上报");
                        currentSysId = inspectSummary.SysId;
                        Save();
                        continue;
                    }
                    //Logger.Log("Process", $"找到产品,码为:{PannelID},符合设定PannelID标准,正常上报");
                    #endregion

                    #region 根据 UniqueId 去数据表 ivs_lcd_surfaceresult 中查询数据  Remark：确认数据库是否存在UniqueId索引 提高查询效率
                    if (DateBaseOperation.DateBaseOperation.GetICW_LCD_SurfaceResultEntityByUniqueID(inspectSummary.UniqueId,
                       out ICW_LCD_SurfaceResultEntity surfaceResult, out reason) == false)
                    {
                        surfaceResult = new ICW_LCD_SurfaceResultEntity();
                        Logger.Log("Process", $"找不到UniqueId:{inspectSummary.UniqueId}的ICW_LCD_SurfaceResultEntity结果,REASON:{reason}");
                    }
                    else
                    {
                        surfaceResult_AOI = surfaceResult;
                    }
                    #endregion

                    #region 根据 UniqueId 去数据表 ivs_lcd_surfacedefect 中查询所有的主检缺陷数据信息 和 主检缺陷图片  Remark：确认数据库是否存在UniqueId索引 提高查询效率
                    if (DateBaseOperation.DateBaseOperation.GetICW_LCD_SurfaceDefectEntityByUniqueID(inspectSummary.UniqueId,
                      out List<ICW_LCD_SurfaceDefectEntity> surfaceDects, out reason) == false)
                    {
                        surfaceDects = new List<ICW_LCD_SurfaceDefectEntity>();
                        Logger.Log("Process", $"找不到产品{PannelID}的ICW_LCD_SurfaceDefectEntity结果,REASON:{reason}");
                    }
                    #endregion

                    #region 完成数据查询,输出日志
                    Logger.Log("Process", $"完成产品{PannelID}的数据查询，检测结果{inspectSummary.Result}，缺陷数量{surfaceDects.Count}");
                    #endregion

                    #endregion

                    #region 2. 构建上传数据模型
                    string panelId = PannelID;
                    string endTime = inspectSummary?.StopTime.Value.ToString("yyyyMMddHHmmss") ?? "";
                    string currentDate = DateTime.Now.ToString("yyyyMMdd");
                    string currentMonth = DateTime.Now.ToString("MM");
                    string currentDay = DateTime.Now.ToString("dd");

                    // 找不到缺陷时，给一个空列表，不给他为null
                    if (surfaceDects == null)
                        surfaceDects = new List<ICW_LCD_SurfaceDefectEntity>();
                    // 构建PNL XML模型
                    PANEL panelModel = BuildPanelModel(inspectSummary, surfaceDects, endTime);
                    string PNLContext = SerializePanelToXml(panelModel);
                    #endregion

                    #region 3. 路径与文件名构建
                    // 3.1 XML文件路径配置
                    //string xmlFileName = $"{panelId}_{_procId}_{endTime}.{_eqpUnitId}".ToUpper();// 只改这一行
                    string xmlFileName = $"{panelId}_{_procId}_{currentDate}_{endTime.Substring(8)}.{_eqpUnitId}".ToUpper();

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
                        WriteLocalFile(_maxTry, defectLocalPathA, PNLContext, out reason);
                        WriteLocalFile(_maxTry, defectLocalPathB, PNLContext, out reason);
                    }
                    #endregion

                    #region 5. 上传 Panel INDEX 文件到FTP
                    // 创建XML远程目录
                    if (!string.IsNullOrEmpty(xmlRemoteDirA))
                    {
                        bool result = FtpHelper.MakeDirs(config.RootPath, xmlRemoteDirA, false);
                        if (result == false)
                        {
                            Logger.Log("Error", $"远程服务器创建目录：{xmlRemoteDirA} 目录失败！！！");
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(xmlRemoteDirB))
                    {
                        bool result = FtpHelper.MakeDirs(config.RootPath, xmlRemoteDirB, false);
                        if (result == false)
                        {
                            Logger.Log("Error", $"远程服务器创建目录：{xmlRemoteDirB} 目录失败！！！");
                            continue;
                        }
                    }

                    if (!UploadFTPFile(_maxTry, XMLRemoteFilePathA, PNLContext, out reason))
                    {
                        Logger.Log("Error", $"XML文件上传失败，终止当前批次处理，原因：{reason}");
                        continue;
                    }
                    if (!UploadFTPFile(_maxTry, XMLRemoteFilePathB, PNLContext, out reason))
                    {
                        Logger.Log("Error", $"PanelINDEX_INSPECTOR文件上传失败，终止当前批次处理，原因：{reason}");
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
                            Logger.Log("Error", $"远程服务器创建目录：{imgRemoteDir} 目录失败！！！");
                            continue;
                        }

                        // 上传缺陷小图(CropImage)
                        string testFileName = @"\\192.168.100.101\LCDSystemTS\20260506\LG_Check_正面擦前线扫_PC2\2026_05_06_13_56_25_000001_20260506135657AA_2026-05-06 13.58.23.928_产品0_治具0_SurImg_不良品\DefectImage\擦前线光_0[3153_382_3253_490]_BOE-05.bmp";

                        if (!UploadDefectImages(inspectSummary, surfaceDects, imgRemoteDir, endTime, ref reason,testFileName))
                        {
                            Logger.Log("Error", $"缺陷小图(CropImage)上传失败：{reason}");
                            imageUploadOk = false;
                        }
                        

                        // 原图(ORGImage)
                        if (!UploadMarkImage(inspectSummary, surfaceResult, imgRemoteDir, endTime, imgFolderLevels, ref reason, testFileName))
                        {
                            Logger.Log("Error", $"原图(ORGImage)上传失败：{reason}");
                            imageUploadOk = false;
                        }
                    }

                    if (!imageUploadOk)
                    {
                        _imageUploadFailCount++;
                        Logger.Log("Process", $"图片上传失败次数：{_imageUploadFailCount}/{_maxImageUploadFailCount}");

                        if (_imageUploadFailCount >= _maxImageUploadFailCount)
                        {
                            Logger.Log("Process", $"达到最大失败次数，自动跳过产品 {PannelID}，SysId 前进");
                            _imageUploadFailCount = 0;

                            currentSysId = inspectSummary.SysId;
                            Save();
                        }
                        continue;
                    }

                    // 上传成功，重置计数
                    _imageUploadFailCount = 0;
                    #endregion

                    #region SysId更新
                    Logger.Log("Process", $"产品{panelId}上传流程全部完成");
                    currentSysId = inspectSummary.SysId;
                    //currentSysId++;
                    Save();
                    #endregion
                }
                catch (Exception ex)
                {
                    Logger.Log("Error", "XMTMUploadBusiness执行流程出现异常：" + ex.Message, ex);
                }
            }
        }

        #region 构建PNL XML模型
        /// <summary>
        /// 构建PNL XML模型
        /// </summary>
        private PANEL BuildPanelModel(InspectSummaryEntity inspectSummary, List<ICW_LCD_SurfaceDefectEntity> aoiDefects, string endTime)
        {
            string panelId = PannelID;
            string startTime = inspectSummary.StartTime.Value.ToString("yyyyMMddHHmmss");
            string judgeResult = (inspectSummary.Result == "OK") ? "G" : "N";
            string defectReason = inspectSummary.Code ?? string.Empty;

            var panel = new PANEL
            {
                HEADER = new HEADER
                {
                    KEY_ID = $"{panelId}_{startTime}",
                    PANEL_ID = panelId,
                    SERIAL_NO = panelId,
                    TOTAL_INPUT = (aoiDefects?.Count ?? 0).ToString(),
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
                    RECIPE=new RECIPE
                    {
                        CLASSIFY=new CLASSIFY
                        {
                            FA=this._procId,
                        },
                        RECIPE_NODE=new RECIPE_NODE
                        {
                            FA=_eqpUnitId,
                            FA_GIB=""
                        }
                    },
                    DEFECT_NO = new DEFECT_NO { TOTAL = aoiDefects.Count }
                },

                DEFECT_INFO = new DEFECT_INFO
                {
                    DEFECT = new List<DEFECT>()
                }
            };

            // FA_R 动态无限生成
            if (aoiDefects != null && aoiDefects.Count > 0)
            {
                var doc = new XmlDocument();
                var attributes = new List<XmlAttribute>();

                for (int i = 0; i < aoiDefects.Count; i++)
                {
                    string code = aoiDefects[i].Code_AOI ?? inspectSummary.Code ?? "";
                    var attr = doc.CreateAttribute($"FA_{i + 1}");
                    attr.Value = code;
                    attributes.Add(attr);
                }

                panel.BODY.AP_INFO.REASON.DynamicAttributes = attributes.ToArray();
            }


            // 生成缺陷（每个缺陷 1 张图 ）
            foreach (var d in aoiDefects)
            {
                var imgList = new List<IMG>();

                int imageCount = 1;
                for (int seq = 1; seq <= imageCount; seq++)
                {
                    string imgName = $"{panelId}_{_eqpUnitId}_{_procId}_{d.Pos_x:0}_{d.Pos_y:0}_{judgeResult}_{(inspectSummary.Code ?? string.Empty)}_{d.DefIndex + 1}_{seq}.JPG";
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
                    REASON = d.Code_AOI ?? (inspectSummary.Code ?? string.Empty),
                    CLASSIFY = d.CusDefName ?? (inspectSummary.Code ?? string.Empty),
                    IMAGE_FILE_NO = imageCount,
                    IMG = imgList
                };

                // 缺陷属性
                if (surfaceResult_AOI != null && !string.IsNullOrEmpty(surfaceResult_AOI.XMLInfo))
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

            if (DateBaseOperation.DateBaseOperation.GetPanelInfoByUniqueId(uniqueId, out List<PanelInfoEntity> entities, out reason))
            {
                if (entities?.Count > 0)
                {
                    // 过滤出 ParamName 为 PANEL_ID 的行，再取 ParamValue
                    pannelID = entities.FirstOrDefault(x => x.ParamName == "PanelId")?.ParamValue ?? "";
                }
            }
        }
        #endregion

        #region aoiDefects具体内容
        /// <summary>
        /// 填充 DEFECT 所有字段（按最新规则：有值赋值，无值填0，两次GRAYLEVELL_RATE=0）
        /// </summary>
        private void FillFullDefectData(DEFECT defect, ICW_LCD_SurfaceDefectEntity d)
        {
            #region 从AoiDefect表取
            defect.G_POSX = (int)d.Pos_x;
            defect.G_POSY = (int)d.Pos_y;
            defect.G_WIDTH = d.Pos_width;
            defect.G_HEIGHT = d.Pos_height;
            defect.G_GRAYMAX = 0;
            defect.G_GRAYMAX_ORG = 0;
            defect.G_DEFECTSIZE = 0;
            defect.G_DEFECTID = d.DefIndex + 1;
            defect.G_PTNNO = d.PatternID;

            // 缺陷坐标
            defect.G_DFTSX = (int)d.Pos_x;
            defect.G_DFTSY = (int)d.Pos_y;
            defect.G_DFTEX = (int)(d.Pos_x + d.Pos_width);
            defect.G_DFTEY = (int)(d.Pos_y + d.Pos_height);

            // 颜色类型
            switch (d.PatternName)
            {
                case "Red": defect.G_PTNTYPE = 1; break;
                case "Green": defect.G_PTNTYPE = 2; break;
                case "Blue": defect.G_PTNTYPE = 3; break;
                default: defect.G_PTNTYPE = 0; break;
            }
            #endregion

            #region 从 ivs_lcd_inspectionresult 拿      
            if (surfaceResult_AOI != null)
            {
                // 直接拿面板物理尺寸
                defect.G_REALPANELX = surfaceResult_AOI.PanelPhysicalXLen;
                defect.G_REALPANELY = surfaceResult_AOI.PanelPhysicalYLen;

                //缺陷物理 坐标(计算 G_REALX 和 G_REALY)
                //double gridX = surfaceResult_AOI.GridImageXLen;
                //double gridY = surfaceResult_AOI.GridImageYLen;

                double gridX = 0;
                double gridY = 0;
                // 防止除以0
                if (gridX > 0)
                {
                    defect.G_REALX = (d.Pos_x / gridX) * defect.G_REALPANELX;
                }
                else
                {
                    defect.G_REALX = 0;
                }
                if (gridY > 0)
                {
                    defect.G_REALY = (d.Pos_y / gridY) * defect.G_REALPANELY;
                }
                else
                {
                    defect.G_REALY = 0;
                }
            }
            else
            {
                // 如没查到数据，保持0
                defect.G_REALPANELX = 0;
                defect.G_REALPANELY = 0;
                defect.G_REALX = 0;
                defect.G_REALY = 0;
            }
            #endregion

            #region 以下：没有值就 = 0 
            // G 组剩余
            defect.G_AREAOVERTHRES = 0;
            defect.G_AVGGRAYLEVELH = 0;
            defect.G_AVGGRAYLEVELH_ORG = 0;
            defect.G_AVGGRAYLEVELL = 0;
            defect.G_AVGGRAYLEVELL_ORG = 0;
            defect.G_DEFECTTYPE = 0;
            defect.G_GRAYMIN = 0;
            defect.G_GRAYMIN_ORG = 0;
            defect.G_SHOTNO = 0;
            defect.G_VPNO = 0;
            defect.G_ZONENO = 0;
            defect.G_CAMNO = 0;
            defect.G_CELLNO = 0;
            defect.G_GRIDNUM = 0;
            defect.G_OPTICTYPE = 0;
            defect.GRAYLEVELL_RATE = 0;

            // L
            defect.L_LDPEAKDIFFERENCE = 0;
            defect.L_LDWIDTH = 0;

            // M
            defect.M_COMPRESSION = 0;
            defect.M_DEFOCUS_INDEX = 0;
            defect.M_INTENSITY = 0;
            defect.M_LEVEL_DATA = 0;
            defect.M_N1 = 0;
            defect.M_N2 = 0;
            defect.M_N3 = 0;
            defect.M_N3_NEW = 0;
            defect.M_N4 = 0;
            defect.M_N5 = 0;
            defect.M_NLD_AREA = 0;
            defect.M_NLD_AVG = 0;
            defect.M_NLD_AVG_DIFF = 0;
            defect.M_NLD_DIFF = 0;
            defect.M_NLD_REF = 0;
            defect.M_NLD_SHOT2_AVG_DIFF = 0;
            defect.M_NLD_SHOT2_NLD_VAR_DST = 0;
            defect.M_NLD_SHOT2_VAR = 0;
            defect.M_NLD_TAR = 0;
            defect.M_NLD_VAR = 0;
            defect.M_ROI_TYPE = 0;
            defect.M_BVALUEH = 0;
            defect.M_BVALUEL = 0;
            defect.M_BOX40_AVGGRAYLEVEL = 0;
            defect.M_BOX40_MAXVAL = 0;
            defect.M_BOX40_MINVAL = 0;
            defect.M_BOX40_STDDEVIATION = 0;
            defect.M_GVALUEH = 0;
            defect.M_GVALUEL = 0;
            defect.M_LVALUEH = 0;
            defect.M_LVALUEL = 0;
            defect.M_POCB_DEFECT = 0;
            defect.M_RVALUEH = 0;
            defect.M_RVALUEL = 0;
            defect.M_UVALUEH = 0;
            defect.M_UVALUEL = 0;
            defect.M_VVALUEH = 0;
            defect.M_VVALUEL = 0;

            // P
            defect.P_AVGGRAYLEVELH_148 = 0;
            defect.P_AVGGRAYLEVELH_B = 0;
            defect.P_AVGGRAYLEVELH_G = 0;
            defect.P_AVGGRAYLEVELH_R = 0;
            defect.P_AVGGRAYLEVELL_108 = 0;
            defect.P_AVGGRAYLEVELL_B = 0;
            defect.P_AVGGRAYLEVELL_G = 0;
            defect.P_AVGGRAYLEVELL_PRC = 0;
            defect.P_AVGGRAYLEVELL_R = 0;
            defect.P_BRIGHT_M_LINK_CNT = 0;
            defect.P_DARK_M_LINK_RGB = 0;
            defect.P_DARK_M_LINK_CNT = 0;
            defect.P_NEAR_PIXEL_VAL = 0;
            defect.P_OMIT_AVG_ORG = 0;
            defect.P_OMIT_AVG_PRC = 0;
            defect.P_OMIT_MAX_ORG = 0;
            defect.P_OMIT_MAX_PRC = 0;
            defect.P_PIXELINFO = 0;
            defect.P_PIXEL_PITCH = 0;
            defect.P_PIXEL_SIZE_X = 0;
            defect.P_PIXEL_SIZE_Y = 0;
            defect.P_SHARE_AVGGRAYLEVELL_ORG_B = 0;
            defect.P_SHARE_AVGGRAYLEVELL_ORG_G = 0;
            defect.P_SHARE_AVGGRAYLEVELL_ORG_R = 0;
            defect.P_SHARE_DARK_M_LINK_CNT_B = 0;
            defect.P_SHARE_DARK_M_LINK_CNT_G = 0;
            defect.P_SHARE_DARK_M_LINK_CNT_R = 0;
            defect.P_SHARE_NEAR_PIXEL_VAL_B = 0;
            defect.P_SHARE_NEAR_PIXEL_VAL_G = 0;
            defect.P_SHARE_NEAR_PIXEL_VAL_R = 0;
            defect.P_SHARE_POSX_B = 0;
            defect.P_SHARE_POSX_G = 0;
            defect.P_SHARE_POSX_R = 0;
            defect.P_SHARE_POSY_B = 0;
            defect.P_SHARE_POSY_G = 0;
            defect.P_SHARE_POSY_R = 0;
            defect.P_HIGH_AREA_GRAYLEVEL_COUNT = 0;
            defect.P_LOW_AREA_GRAYLEVEL_COUNT = 0;

            // Q
            defect.Q_DISPLAY_ROI = 0;
            defect.Q_ZONE1_MIN = 0;
            defect.Q_ZONE2_MIN = 0;
            defect.Q_ZONE3_MIN = 0;

            // T
            defect.T_ASYNC_1 = 0;
            defect.T_ASYNC_10 = 0;
            defect.T_ASYNC_2 = 0;
            defect.T_ASYNC_3 = 0;
            defect.T_ASYNC_4 = 0;
            defect.T_ASYNC_5 = 0;
            defect.T_ASYNC_6 = 0;
            defect.T_ASYNC_7 = 0;
            defect.T_ASYNC_8 = 0;
            defect.T_ASYNC_9 = 0;
            defect.T_ASYNC_INDEX = 0;
            defect.T_ASYNC_STD1 = 0;
            defect.T_ASYNC_STD10 = 0;
            defect.T_ASYNC_STD2 = 0;
            defect.T_ASYNC_STD3 = 0;
            defect.T_ASYNC_STD4 = 0;
            defect.T_ASYNC_STD5 = 0;
            defect.T_ASYNC_STD6 = 0;
            defect.T_ASYNC_STD7 = 0;
            defect.T_ASYNC_STD8 = 0;
            defect.T_ASYNC_STD9 = 0;

            // U
            defect.U1_OMIT_AREA_PRC = 0;
            defect.U2_OMIT_AREA_PRC = 0;
            defect.U3_ASYNC_INDEX2 = 0;
            defect.U4_OMIT_WID = 0;
            defect.U5_OMIT_HGT = 0;
            defect.U_ANOTHER_OMIT_AREA = 0;
            defect.U_ANOTHER_OMIT_AVG_PRC = 0;
            defect.U_ANOTHER_OMIT_HGT = 0;
            defect.U_ANOTHER_OMIT_MAX_PRC = 0;
            defect.U_ANOTHER_OMIT_MIN_PRC = 0;
            defect.U_ANOTHER_OMIT_WID = 0;
            defect.U_OMIT_AREA = 0;
            defect.U_OMIT_HGT = 0;
            defect.U_OMIT_WID = 0;

            // X 全部 0
            defect.X001_INSPTYPE = 0;
            defect.X002_OMIT_BLOB_AREA = 0;
            defect.X003_OMIT_BLOB_LX = 0;
            defect.X004_OMIT_BLOB_LY = 0;
            defect.X005_OMIT_BLOB_BOX = 0;
            defect.X008_NLD_LEFTNOTCHDIFF = 0;
            defect.X009_NLD_RIGHTNOTCHDIFF = 0;
            defect.X065_LABAREA = 0;
            defect.X066_LDGRAYMAX = 0;
            defect.X067_LDGRAYMIN = 0;
            defect.X106_FMM_STRENGTH = 0;
            defect.X107_FMM_AVGR = 0;
            defect.X108_FMM_AVGG = 0;
            defect.X109_FMM_AVGB = 0;
            defect.X110_FMM_L = 0;
            defect.X111_FMM_U = 0;
            defect.X112_FMM_V = 0;
            defect.X113_FMM_BASEL = 0;
            defect.X114_FMM_BASEU = 0;
            defect.X115_FMM_BASEV = 0;
            defect.X116_FMM_DEG = 0;
            defect.X117_FMM_SIZE = 0;
            defect.X118_FMM_HEI = 0;
            defect.X119_FMM_WID = 0;
            defect.X120_FMM_U_DIFF = 0;
            defect.X121_FMM_V_DIFF = 0;
            defect.X122_FMM_U_DIFF_GR = 0;
            defect.X123_FMM_U_DEV = 0;
            defect.X124_FMM_V_DEV = 0;
            defect.X125_FMM_U_DEV_GR = 0;
            defect.X126_FMM_STRENGTH = 0;
            defect.X127_FMM_U_DUV = 0;
            defect.X128_FMM_V_DUV = 0;
            defect.X129_FMM_U_COLORANGLE = 0;
            defect.X130_FMM_V_COLORANGLE = 0;
            defect.X137_CHANENL = 0;
            defect.X138_REDPRC = 0;
            defect.X139_GREENPRC = 0;
            defect.X140_BLUEPRC = 0;
            defect.X141_COLORDEG = 0;
            defect.X142_REDDIFF = 0;
            defect.X143_GREENDIFF = 0;
            defect.X144_BLUEDIFF = 0;
            defect.X145_FMM_T_REMAIN = 0;
            defect.X146_FMM_T_INTERSECTION = 0;
            defect.X147_FMM_MAXDIFFUV = 0;
            defect.X158_IQ_PROB1 = 0;
            defect.X159_IQ_PROB2 = 0;
            defect.X160_IQ_FLAG = 0;
            defect.X161_FMM_LINE_PROJ_MAX = 0;
            defect.X162_FMM_LINE_PROJ_MEAN = 0;
            defect.X163_FMM_STRENGTH_MAX10 = 0;
            defect.X164_FMM_STRENGTH_MIN10 = 0;
            defect.X165_FMM_U_DIFF_MAX = 0;
            defect.X166_FMM_U_DIFF_MIN = 0;
            defect.X167_FMM_V_DIFF_MAX = 0;
            defect.X168_FMM_V_DIFF_MIN = 0;
            defect.X200_SIYA_HORI_LD_GRIDMIN_CENTER = 0;
            defect.X201_SIYA_HORI_LD_GRIDMIN_DFT = 0;
            defect.X202_INSP_PROB_C1 = 0;
            defect.X203_INSP_PROB_C2 = 0;
            defect.X204_INSP_PROB_C3 = 0;
            defect.X205_INSP_PROB_C4 = 0;
            defect.X206_INSP_PROB_C5 = 0;
            defect.X207_INSP_PROB_C6 = 0;
            defect.X208_EDGE_GROUPING = 0;
            #endregion
        }
        #endregion

        #region 上传 方法
        /// <summary>
        /// 上传缺陷图片
        /// </summary>
        private bool UploadDefectImages(InspectSummaryEntity inspectSummary, List<ICW_LCD_SurfaceDefectEntity> aoiDefects,
            string imgRemoteDir, string endTime, ref string reason,string imgLocalPath)
        {
            int uploadCount = 0;
            string panelId = PannelID;
            string judgeResult = inspectSummary.Result == "OK" ? "G" : "N";

            foreach (var defect in aoiDefects)
            {
                if (uploadCount >= _defectUploadMaxCount) break;

                int defectSerialNo = defect.DefIndex + 1;
                string defectCode = defect.Code_AOI ?? inspectSummary.Code ?? "";
                //string imgFileName = $"{panelId}_{_eqpUnitId}_{_procId}_{DateTime.Now.ToString("yyyyMMdd")}_gate_{judgeResult}_{defectCode}_{defectSerialNo}.JPG".ToUpper();
                // 和 XML内命名改为一致
                string imgFileName = $"{panelId}_{_eqpUnitId}_{_procId}_{defect.Pos_x:0}_{defect.Pos_y:0}_{judgeResult}_{defectCode}_{defectSerialNo}_{this.config.Lane}.JPG".ToUpper();

                imgLocalPath= imgLocalPath!=null? imgLocalPath: Path.Combine(_localImageRoot, defect.ImagePath ?? "").ToUpper();
                string imgRemotePath = $"{imgRemoteDir}/{imgFileName}";

                if (!UploadFTPFileWithPath(_maxTry, imgRemotePath, imgLocalPath, out reason))
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
        private bool UploadMarkImage(InspectSummaryEntity inspectSummary, ICW_LCD_SurfaceResultEntity inspection,
            string imgRemoteDir, string endTime, string[] imgFolderLevels, ref string reason,string imgLocalPath)
        {
            string markImgPath = GetMarkImagePath(inspection);
            //if (string.IsNullOrEmpty(markImgPath))
            //{
            //    Logger.Log("Warning", "未解析到原图路径，跳过上传");
            //    return true;
            //}

            string imageFileName_MarkImg = $"{PannelID}_{_eqpUnitId}_{endTime}_{this.config.Lane}.jpg".ToUpper();
            imgLocalPath = imgLocalPath!=null? imgLocalPath: Path.Combine(_localImageRoot, markImgPath).ToUpper();
            string imgRemotePath = $"{imgRemoteDir}/{imageFileName_MarkImg}";

            if (!UploadFTPFileWithPath(_maxTry, imgRemotePath, imgLocalPath, out reason))
            {
                Logger.Log("Error", $"原图上传失败：{reason}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 解析原图路径
        /// </summary>
        private string GetMarkImagePath(ICW_LCD_SurfaceResultEntity inspection)
        {
            try
            {
                if (inspection==null||string.IsNullOrEmpty(inspection.XMLInfo)) return "";
                XElement root = XElement.Parse(inspection.XMLInfo);
                XElement markImgElement = root.Element("MarkImg");
                return markImgElement?.Value ?? "";
            }
            catch (XmlException ex)
            {
                Logger.Log("Error", $"XML解析错误：{ex.Message}");
                return "";
            }
        }
        #endregion

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
            config.CurrentSysId = currentSysId;
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

        #region FTP与本地文件操作
        [Obsolete("此方法已废弃，请使用 MakeDirs 疑似有Bug")]
        void CreateFolders(string baseDir, IEnumerable<string> folderLevels)
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

    #region 配置实体类
    public class XMTMConfig : BusinessConfig
    {
        #region 01. 设备配置
        [Category("01. 设备配置")]
        [DisplayName("LINE_ID")]
        [Description("产线ID")]
        public string LINE_ID { get; set; } = "*";

        [Category("01. 设备配置")]
        [DisplayName("EQP UNIT_ID")]
        [Description("EQP UNIT_ID 设备唯一标识")]
        public string EQPUNIT_ID { get; set; } = "*";

        [Category("01. 设备配置")]
        [DisplayName("RECIPE_NAME")]
        [Description("配方名称")]
        public string RECIPE_NAME { get; set; } = "*";

        [Category("01. 设备配置")]
        [DisplayName("PROC_ID")]
        [Description("流程ID")]
        public string PROC_ID { get; set; } = "*";
        #endregion

        #region 02. 操作员配置
        [Category("02. 操作员配置")]
        [DisplayName("OPER_ID")]
        [Description("操作员ID (HEADER)")]
        public string OPER_ID { get; set; } = "OP001";

        [Category("02. 操作员配置")]
        [DisplayName("OPERATOR_LA")]
        [Description("操作员名称 (BODY/AP_INFO)")]
        public string OPERATOR_LA { get; set; } = "OPERATOR";
        #endregion

        #region 03. FTP配置
        [Category("03. FTP配置")]
        [DisplayName("文件DFS根目录RootPath")]
        [Description("FTP根路径")]
        public string RootPath { get; set; } = "";

        [Category("03. FTP配置")]
        [DisplayName("Username")]
        [Description("FTP用户名")]
        public string Username { get; set; } = "";

        [Category("03. FTP配置")]
        [Description("FTP密码")]
        public string Password { get; set; } = "";

        [Category("03. FTP配置")]
        [DisplayName("缺陷图DFS根目录ImageUploadRootPath")]
        [Description("图片文件上传根路径")]
        public string ImgUploadRootPath { get; set; } = "ftp://10.119.211.166/IMAGE/";
        #endregion

        #region 04. 业务配置
        [Category("04. 业务配置")]
        [DisplayName("PannelID Length")]
        [Description("条码最大长度")]
        public int BarCodeLength { get; set; } = 20;

        [Category("04. 业务配置")]
        [DisplayName("MaxRetry")]
        [Description("最大重试次数")]
        public int MaxRetry { get; set; } = 3;

        [Category("04. 业务配置")]
        [DisplayName("DefectUploadMaxCount")]
        [Description("最大缺陷上传数量")]
        public int DefectUploadMaxCount { get; set; } = 50;

        [Category("04. 业务配置")]
        [DisplayName("IsUploadFileLocal")]
        [Description("是否本地备份文件")]
        public bool IsUploadFileLocal { get; set; } = true;

        [Category("04. 业务配置")]
        [DisplayName("LocalImageRoot")]
        [Description("本地图片根目录")]
        public string LocalImageRoot { get; set; } = "C:\\InspectImages";

        [Category("04. 业务配置")]
        [DisplayName("CurrentSysId")]
        [Description("当前处理的SysId")]
        public long CurrentSysId { get; set; } = 0;

        [Category("04. 业务配置")]
        [DisplayName("图片上传失败最大重试次数ImageUploadMaxCount")]
        [Description("超过此次数自动跳过该产品")]
        public int MaxImageUploadFailCount { get; set; } = 30;

        [Category("04. 业务配置")]
        [DisplayName("设备位置通道(FA_LANE)")]
        [Description("A面或者B面")]
        public string Lane { get; set; } = "A";

        [Category("04. 业务配置")]
        [DisplayName("工位(FA_STAGE)")]
        [Description("")]
        public int Stage { get; set; } = 1;
        #endregion
    }
    #endregion
}