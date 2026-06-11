using System;
using System.Windows.Forms;

namespace DataFileServer
{
    public partial class PasswordForm : Form
    {
        private readonly string operatorId;
        public PasswordForm(string operatorId)
        {
            InitializeComponent();
            this.operatorId = operatorId;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            if (textBox_Operator.Text.Trim() == operatorId)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(@"密码错误");
            }
        }
    }
}
