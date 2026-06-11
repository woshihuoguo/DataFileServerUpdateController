using Frame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataFileServerUpdateController
{
    public partial class ServiceConfigForm : Form
    {
        public BusinessConfig Config;

        public ServiceConfigForm()
        {
            InitializeComponent();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Config = (BusinessConfig)propertyGrid1.SelectedObject;
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            if (Config != null)
            {
                this.propertyGrid1.SelectedObject = Config;
            }
            else
            {
                MessageBox.Show("缺少配置文件");
            }
        }
    }
}
