namespace DataFileServerUpdateController
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.业务配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logViewer1 = new LT.Common.Forms.Advance.LogViewer();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.show = new System.Windows.Forms.ToolStripMenuItem();
            this.hide = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkBoxPasue = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置ToolStripMenuItem,
            this.业务配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.配置ToolStripMenuItem.Text = "系统配置";
            this.配置ToolStripMenuItem.Click += new System.EventHandler(this.配置ToolStripMenuItem_Click);
            // 
            // 业务配置ToolStripMenuItem
            // 
            this.业务配置ToolStripMenuItem.Name = "业务配置ToolStripMenuItem";
            this.业务配置ToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.业务配置ToolStripMenuItem.Text = "业务配置";
            this.业务配置ToolStripMenuItem.Click += new System.EventHandler(this.业务配置ToolStripMenuItem_Click);
            // 
            // logViewer1
            // 
            this.logViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logViewer1.Location = new System.Drawing.Point(0, 24);
            this.logViewer1.Name = "logViewer1";
            this.logViewer1.Size = new System.Drawing.Size(823, 498);
            this.logViewer1.TabIndex = 1;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.show,
            this.hide,
            this.exit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(127, 70);
            // 
            // show
            // 
            this.show.Name = "show";
            this.show.Size = new System.Drawing.Size(126, 22);
            this.show.Text = "显示窗口";
            // 
            // hide
            // 
            this.hide.Name = "hide";
            this.hide.Size = new System.Drawing.Size(126, 22);
            this.hide.Text = "隐藏窗口";
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(126, 22);
            this.exit.Text = "退出";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "DFS";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // checkBoxPasue
            // 
            this.checkBoxPasue.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxPasue.Location = new System.Drawing.Point(748, 27);
            this.checkBoxPasue.Name = "checkBoxPasue";
            this.checkBoxPasue.Size = new System.Drawing.Size(63, 29);
            this.checkBoxPasue.TabIndex = 3;
            this.checkBoxPasue.Text = "暂停运行";
            this.checkBoxPasue.UseVisualStyleBackColor = true;
            this.checkBoxPasue.CheckedChanged += new System.EventHandler(this.checkBoxPasue_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 522);
            this.Controls.Add(this.checkBoxPasue);
            this.Controls.Add(this.logViewer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ICW DFS数据上传辅助工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 业务配置ToolStripMenuItem;
        private LT.Common.Forms.Advance.LogViewer logViewer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem show;
        private System.Windows.Forms.ToolStripMenuItem hide;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox checkBoxPasue;
    }
}

