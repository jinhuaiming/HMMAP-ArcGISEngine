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
    public partial class 显示 : Form
    {
        public 显示()
        {
            InitializeComponent();
        }

      
       
        //查询
        private void button2_Click(object sender, EventArgs e)
        {           
            注册 a1 = new 注册();
            if (a1.check_textbox(textBox1, textBox2))
            {
                try
                {
                    string mystr = string.Format("select * from {0}", textBox2.Text.Trim());
                    string lian_jie = string.Format("server=localhost;port=3306;database={0};user={1};password={2};charset=utf8;", textBox1.Text.Trim(),mysql账号密码.use,mysql账号密码.password);
                    DataSet myds = new DataSet();
                    MySqlConnection conn = new MySqlConnection(lian_jie);
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        MySqlDataAdapter myad = new MySqlDataAdapter(mystr, conn);
                        myad.Fill(myds,textBox2.Text.Trim());
                        dataGridView1.DataSource = myds.Tables[textBox2.Text.Trim()];

                    }
                    else
                    {
                        MessageBox.Show("连接失败", "提示", MessageBoxButtons.OK);
                    }
                }
                catch(Exception err){
                    MessageBox.Show("错误为:"+err.Message,"提示",MessageBoxButtons.OK);                  
                }
             
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form a = new mysql账号密码();
            a.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("功能尚未完善！");
        }

    }
}
