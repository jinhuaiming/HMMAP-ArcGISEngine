using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HmMap
{
    public partial class mysql账号密码 : Form
    {
        public static string use;
        public static string password;

        public mysql账号密码()
        {
            InitializeComponent();
          
        }   
        private void button1_Click(object sender, EventArgs e)
        {
            注册 aa = new 注册();
            try
            {
                if (aa.check_textbox(textBox1, textBox2))
                {
                    use = textBox1.Text.Trim();
                    password = textBox2.Text.Trim();

                    string conn = string.Format("server=localhost;port=3306;database=text_database;user={0};password={1};charset=utf8;", use, password);
                    MySqlConnection lianjie_state = new MySqlConnection(conn);
                    lianjie_state.Open();
                    if (lianjie_state.State == ConnectionState.Open)
                    {
                        label3.Text = "数据库连接成功";
                    }
                    else
                    {
                        label3.Text = "数据库连接失败！";
                    }

                }
            }
          catch(Exception arr){
               label3.Text = "数据库连接失败！";                   
              MessageBox.Show("错误:"+arr.Message);
          }
        }
    }
}
