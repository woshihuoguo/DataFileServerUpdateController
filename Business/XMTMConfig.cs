using Frame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    #region 配置实体类
    public class XMTMConfig : BusinessConfig
    {
        #region 01. 设备配置
        [Category("01. 设备配置")]
        [DisplayName("LINE_ID")]
        [Description("产线ID")]
        public string LINE_ID { get; set; } = "*";

        [Category("01. 设备配置")]
        [DisplayName("AOI_EQPUNIT_ID")]
        [Description("AOI_EQPUNIT_ID 设备唯一标识")]
        public string AOI_EQPUNIT_ID { get; set; } = "*";

        [Category("01. 设备配置")]
        [DisplayName("GIB_EQPUNIT_ID")]
        [Description("GIB_EQPUNIT_ID 设备唯一标识")]
        public string GIB_EQPUNIT_ID { get; set; } = "*";

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
        #endregion
    }
    #endregion
}
