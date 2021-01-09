using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HmMap
{
    public partial class speck : Form
    {
        public speck()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "帅")
            {
                注册.lable = true;
                this.Close();
            }
            else {
                label2.Text = "回答错误欧！";
            }
                 
        }
    }
}
