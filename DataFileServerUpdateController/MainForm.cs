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

            this.checkBoxPasue.Location = new Point(this.ClientSize.Width - this.checkBoxPasue.Width - 20, 30);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.Text += " v" + Application.ProductVersion;
            this.Text = string.Format(@"DFS v" + Application.ProductVersion);
            Logger.Log("Process", $"DFS软件v{Application.ProductVersion}启动");

            if (Initial(out string reason) == false)
            {
                MessageBox.Show($"系统初始化发生异常，即将自动退出，原因：{reason}", "错误");
                Application.Exit();
            }
            logViewer1.Start("Process");
        }

        public PlcCommunicator plcCommunicator = new PlcCommunicator();

        private BaseConfig baseConfig;
        private XMTMConfig serviceConfig;
        private Thread serviceThread;

        private UpLoadBusiness AOIbusiness;
        private UpLoadBusiness GIBbusiness;



        //系统初始化
        private bool Initial(out string reason)
        {
            reason = string.Empty;
            try
            {
                //加载系统配置,确认加载业务
                if (JsonSerializerHelper<BaseConfig>.Load(out baseConfig, out reason) == false)
                {
                    baseConfig = new BaseConfig();
                    JsonSerializerHelper<BaseConfig>.Save(baseConfig, out reason);
                    MessageBox.Show("缺少系统配置数据，无法启动业务，请先进行配置，并重启");
                }
                else
                {
                    serviceConfig = new XMTMConfig();
                    if (JsonSerializerHelper<XMTMConfig>.Load(out serviceConfig, out reason) == false)
                    {
                        serviceConfig = new XMTMConfig();
                        JsonSerializerHelper<XMTMConfig>.Save(serviceConfig, out reason);
                        MessageBox.Show("缺少业务，请先进行配置，并重启");
                    }
                    else
                    {
                        var typeAOI = $"Business.{baseConfig.CustomBusiness}UploadBusiness_AOI";
                        AOIbusiness = (UpLoadBusiness)Assembly.Load("Business").CreateInstance(typeAOI);
                        AOIbusiness.Frame = new Frame.Frame("127.0.0.1");  //AOI数据库
                        AOIbusiness.SetConfig(serviceConfig);
                        
                        //尝试连AOI接数据库
                        if (AOIbusiness.Frame.Start(out reason) == false)
                        {
                            return false;
                        }
                        AOIbusiness.Frame.SystemConfig = baseConfig;
                        Task.Run(() =>
                        {
                            AOIbusiness.Start();
                        });

                        if (serviceConfig.Lane == "A")
                        {
                            var typeGIB = $"Business.{baseConfig.CustomBusiness}UploadBusiness_GIB";
                            GIBbusiness = (UpLoadBusiness)Assembly.Load("Business").CreateInstance(typeGIB);
                            GIBbusiness.Frame = new Frame.Frame("127.0.0.1");  //GIB数据库
                            GIBbusiness.SetConfig(serviceConfig);
                            
                            //尝试连接复判数据库
                            if (GIBbusiness.Frame.Start(out reason) == false)
                            {
                                return false;
                            }

                            GIBbusiness.Frame.SystemConfig = baseConfig;
                            Task.Run(() =>
                            {
                                GIBbusiness.Start();
                            });
                        }

                        var serviceThread = new Thread(() =>
                        {
                            if (baseConfig.PlcHeartBeatEnable && string.IsNullOrEmpty(baseConfig.PlcProtocol) == false &&
                                string.IsNullOrEmpty(baseConfig.PlcIp) == false && baseConfig.PlcPort > 0)
                            {
                                if (Enum.TryParse(baseConfig.PlcProtocol, out PLCProtocolType protocolType))
                                {
                                    if (AOIbusiness.PlcCommunicator.Initial(protocolType, baseConfig.PlcIp, baseConfig.PlcPort, out string reason1) == false)
                                    {
                                        Logger.Log("Debug", "PLC初始化发生异常," + reason1);
                                    }
                                    if (AOIbusiness.PlcCommunicator.Start(out reason1) == false)
                                    {
                                        Logger.Log("Debug", "PLC启动发生异常," + reason1);
                                    }
                                }
                                else
                                {
                                    Logger.Log("Debug", "PLC类型配置错误");
                                }
                            }

                        });
                        serviceThread.Start();
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
                if (JsonSerializerHelper<BaseConfig>.Save(baseConfig, out string reason))
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

            AOIbusiness.SetConfig(form.Config);
            GIBbusiness.SetConfig(form.Config);

            AOIbusiness.InitConfig();
            GIBbusiness.InitConfig();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (AOIbusiness != null)
            {
                AOIbusiness.Stop();
            }

            if (GIBbusiness != null)
            {
                GIBbusiness.Stop();
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

                    if (AOIbusiness != null)
                    {
                        AOIbusiness.Stop();
                    }

                    if (GIBbusiness != null)
                    {
                        GIBbusiness.Stop();
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
                if (AOIbusiness != null)
                {
                    AOIbusiness.Stop();
                }

                if (GIBbusiness != null)
                {
                    GIBbusiness.Stop();
                }
            }
            else
            {
                if (AOIbusiness != null)
                {
                    AOIbusiness.Stop();
                }

                if (GIBbusiness != null)
                {
                    GIBbusiness.Stop();
                }
            }
            Logger.Log("Debug", "【按钮选择】" + (checkBoxPasue.Checked ? "开始上传" : "停止上传"));
        }


        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            this.checkBoxPasue.Location = new Point(this.ClientSize.Width - this.checkBoxPasue.Width - 20, 30);
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
