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
    public partial class 注册 : Form
    {
        public static bool lable = false;
        public 注册()
        {
            InitializeComponent();
        }

        #region 事件

        //取消
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //注册
        private void button1_Click(object sender, EventArgs e)
        {
            if (check_textbox(textBox1, textBox2, textBox3, textBox4))
            {
                string use = textBox1.Text;
                string password = textBox2.Text;
                string QQ = textBox3.Text;
                string email = textBox4.Text;

                MySqlConnection lianjie;

                Form speck = new speck();
                speck.ShowDialog();

                try
                {
                    if (lable)
                    {
                        if (db_con(out lianjie))
                        {
                            string sql = string.Format("insert into user value('{0}','{1}','{2}','{3}')", use, password, QQ, email);
                            MySqlCommand mycmd = new MySqlCommand(sql, lianjie);
                            mycmd.ExecuteNonQuery();
                            lianjie.Close();
                            MessageBox.Show("恭喜，注册成功！");
                            this.Close();
                        }
                    }
                }
                catch (Exception arr)
                {
                    MessageBox.Show("错误:"+arr.Message);
                }
            }

        }
        #endregion
        #region 方法

        //检查文本框是非为空，返回值为布尔值；
        public bool check_textbox(params TextBox[] textbox)
        {
            foreach (TextBox a in textbox)
            {
                if (a.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("填写不完整", "提示", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }
        //连接mysql数据
        private bool db_con(out MySqlConnection a)
        {

            string conn_string = "server=localhost;port=3306;database=hmmap;user=root;password=jhm19980613;charset=utf8;";

            MySqlConnection lianjie = new MySqlConnection(conn_string);
            a = lianjie;
            lianjie.Open();
            if (lianjie.State == ConnectionState.Open)
            {
                return true;
            }
            MessageBox.Show("数据连接失败！");
            return false;
        }
        #endregion
    }
}
