using Business;
using DataFileServer;
using Frame;
using LT.Common.Logger;
using LT.Common.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataFileServerUpdateController
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.Text += " v" + Application.ProductVersion;
            this.Text = string.Format(@"DFS v" + Application.ProductVersion); 
            Logger.Log("Process", $"DFS软件v{Application.ProductVersion}启动");

            if (Initial(out string reason) == false)
            {
                MessageBox.Show($"系统初始化发生异常，即将自动退出，原因：{reason}","错误");
                Application.Exit();
            }
            logViewer1.Start("Process");
        }

        public PlcCommunicator plcCommunicator = new PlcCommunicator();

        private BaseConfig baseConfig;
        private XMTMConfig serviceConfig;
        private Thread serviceThread;
        private UpLoadBusiness business;

        //系统初始化
        private bool Initial(out string reason)
        {
            reason = string.Empty;
            try
            {
                //尝试连接数据库
                if (Frame.Frame.Instance.Start(out reason) == false)
                {
                    return false;
                }

                //加载系统配置,确认加载业务
                if (JsonSerializerHelper<BaseConfig>.Load(out baseConfig, out reason) == false)
                {
                    baseConfig = new BaseConfig();
                    JsonSerializerHelper<BaseConfig>.Save(baseConfig, out reason);
                    MessageBox.Show("缺少系统配置数据，无法启动业务，请先进行配置，并重启");
                }
                else
                {
                    var type = $"Business.{baseConfig.CustomBusiness}UploadBusiness";
                    business = (UpLoadBusiness)Assembly.Load("Business").CreateInstance(type);

                    //加载业务配置
                    //var serviceConfigType = business.GetConfig();
                    //var type1 = $"Business.{baseConfig.CustomBusiness}Config";
                    serviceConfig = new XMTMConfig();
                    if (JsonSerializerHelper<XMTMConfig>.Load(out serviceConfig, out reason) == false)
                    {
                        serviceConfig = new XMTMConfig();
                        JsonSerializerHelper<XMTMConfig>.Save(serviceConfig, out reason);
                        MessageBox.Show("缺少业务，请先进行配置，并重启");
                    }
                    else
                    {
                        business.SetConfig(serviceConfig);
                    }

                    var serviceThread = new Thread(() =>
                    {
                        if (string.IsNullOrEmpty(baseConfig.PlcProtocol) == false &&
                            string.IsNullOrEmpty(baseConfig.PlcIp) == false && baseConfig.PlcPort > 0)
                        {
                            if (Enum.TryParse(baseConfig.PlcProtocol, out PLCProtocolType protocolType))
                            {
                                if (business.PlcCommunicator.Initial(protocolType, baseConfig.PlcIp, baseConfig.PlcPort, out string reason1) == false)
                                {
                                    Logger.Log("Debug", "PLC初始化发生异常," + reason1);
                                }
                                if (business.PlcCommunicator.Start(out reason1) == false)
                                {
                                    Logger.Log("Debug", "PLC启动发生异常," + reason1);
                                }
                            }
                            else
                            {
                                Logger.Log("Debug", "PLC类型配置错误");
                            }
                        }
                        
                        business.Start();
                    });
                    serviceThread.Start();
                }

                Frame.Frame.Instance.SystemConfig = baseConfig;
                //如果PLC心跳开启，尝试连接PLC
                if (baseConfig.PlcHeartBeatEnable && string.IsNullOrEmpty(baseConfig.PlcProtocol) == false &&
                    string.IsNullOrEmpty(baseConfig.PlcIp) == false && baseConfig.PlcPort > 0)
                {
                    if (Enum.TryParse(baseConfig.PlcProtocol, out PLCProtocolType protocolType))
                    {
                        if(plcCommunicator.Initial(protocolType, baseConfig.PlcIp, baseConfig.PlcPort, out reason) == false)
                        {
                            return false;
                        }
                        if(plcCommunicator.Start(out reason) == false)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        reason = "PLC类型配置错误";
                        return false;
                    }
                }
                              

                return true;
            }
            catch (Exception ex)
            {
                reason = ex.ToString();
                return false;
            }
        }

        private bool Start(out string reason)
        {
            reason = string.Empty;

            return true;
        }

        private void 配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ConfigForm();
            form.Config = baseConfig;
            if (form.ShowDialog() == DialogResult.OK)
            {
                if(JsonSerializerHelper<BaseConfig>.Save(baseConfig, out string reason))
                {
                    MessageBox.Show("保存配置完成");
                }
                else
                {
                    MessageBox.Show("保存配置失败，" + reason);
                }
            }
        }

        private void 业务配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ServiceConfigForm();
            form.Config = serviceConfig;
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (JsonSerializerHelper<XMTMConfig>.Save(serviceConfig, out string reason))
                {
                    MessageBox.Show("保存配置完成");
                }
                else
                {
                    MessageBox.Show("保存配置失败，" + reason);
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (business != null)
            {
                business.Stop();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }

            using (var closeFrom = new PasswordForm(baseConfig.Password))
            {
                if (closeFrom.ShowDialog() != DialogResult.OK)
                {
                    e.Cancel = true;
                }
                else
                {
                    Logger.Log("Process", "DFS软件关闭");

                    if (business != null)
                    {
                        business.Stop();
                    }

                    Thread.Sleep(500);
                    Environment.Exit(0);
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {

        }

        private void checkBoxPasue_CheckedChanged(object sender, EventArgs e)
        {
            //checkBoxPasue.BackColor = checkBoxPasue.Checked ? Color.Green : Color.Red;
            checkBoxPasue.Text = checkBoxPasue.Checked ? "开始上传" : "停止上传";
            if (checkBoxPasue.Checked)
            {
                if (business != null)
                {
                    business.Pause();
                }
            }
            else
            {
                if (business != null)
                {
                    business.Restart();
                }
            }
            Logger.Log("Debug", "【按钮选择】" + (checkBoxPasue.Checked ? "开始上传" : "停止上传"));
        }


        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            // 判断只有最小化时，隐藏窗体
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 正常显示窗体
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }
    }
}
