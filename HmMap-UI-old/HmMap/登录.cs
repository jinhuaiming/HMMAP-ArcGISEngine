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
using HZH_Controls.Controls;

namespace HmMap
{
    public partial class 登录 : Form
    {
        public 登录()
        {
            InitializeComponent();
        }

        #region 事件
        //登录
        private void ucBtnExt1_BtnClick(object sender, EventArgs e)
        {
            try
            {
                注册 aa = new 注册();
                if (check_textbox(ucTextBoxEx1,ucTextBoxEx2))
                {
                    string use = ucTextBoxEx1.InputText;
                    string password =ucTextBoxEx2.InputText;
                    string conn = "server=localhost;port=3306;database=hmmap;user=root;password=jhm19980613;charset=utf8;";
                    string sql = string.Format("select * from user where name='{0}' and password='{1}'", use, password);
                    MySqlConnection lianje = new MySqlConnection(conn);
                    lianje.Open();
                    MySqlCommand mcd = new MySqlCommand(sql, lianje);
                    MySqlDataReader mdr = mcd.ExecuteReader();
                    if (mdr.Read())
                    {
                        Program.Login = 1;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户名或者密码错误，请重新登陆。", "提示", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception arr)
            {

                MessageBox.Show("错误" + arr.Message);
            }

        }
        //注册
        private void label4_Click(object sender, EventArgs e)
        {
            Form region = new 注册();
            region.ShowDialog();
        }

        #region lable
        //lable控件触碰字体增大事件
        private void label4_MouseMove(object sender, MouseEventArgs e)
        {

            label4.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold)
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }

        //label控件恢复事件；
        private void label4_MouseLeave(object sender, EventArgs e)
        {

            label4.Font = new System.Drawing.Font("宋体", 10F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold )
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }
        //lable控件触碰字体增大事件
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold )
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));

        }

        //label控件恢复事件；
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new System.Drawing.Font("宋体", 10F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold )
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));

        }

        //游客模式
        private void label1_Click(object sender, EventArgs e)
        {                      
            Program.Login =2;
            this.Close();
        }
        #endregion

   

     





      

        #endregion
        #region 方法
        private bool check_textbox(params UCTextBoxEx[] textbox)
        {
            foreach (UCTextBoxEx a in textbox)
            {
                if (a.InputText.Trim() == string.Empty)
                {
                    MessageBox.Show("填写不完整", "提示", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }
       
        #endregion




    }
}
