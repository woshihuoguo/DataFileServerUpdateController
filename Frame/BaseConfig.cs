using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frame
{
    [Serializable]
    public class BaseConfig
    {
        [Category("系统配置")]
        [DisplayName("操作用户")]
        public string UserName { get; set; } = "admin";

        [Category("系统配置")]
        [DisplayName("验证密码")]
        public string Password { get; set; } = "admin";

        [Category("系统配置")]
        [DisplayName("PLC心跳开关")]
        public bool PlcHeartBeatEnable { get; set; } = false;

        [Category("系统配置")]
        [DisplayName("PLC通信IP地址")]
        public string PlcIp { get; set; }

        [Category("系统配置")]
        [DisplayName("PLC通信端口")]
        public int PlcPort { get; set; }

        [Category("系统配置")]
        [DisplayName("PLC通信协议")]
        public string PlcProtocol { get; set; }

        [Category("系统配置")]
        [DisplayName("客户协议")]
        public string CustomBusiness { get; set; }
    }

    [Serializable]
    public class BusinessConfig
    {

    }
}
