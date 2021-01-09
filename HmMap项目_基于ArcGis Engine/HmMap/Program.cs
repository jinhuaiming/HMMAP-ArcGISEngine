using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HmMap
{
   
    static class Program
    {
        public static int Login=0;
        private static bool lable=true;
        private static int compare;
        private static Form form;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.Desktop);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //登录界面；
            Application.Run(new 登录());

            Timer time = new Timer();
            Form index = new index();
            form = index;
            time.Interval = 40000;
            time.Enabled = true;


            switch (Login)
            {
                //登录成功，进入主界面；
                case 1: Form index_form = new index();
                    index_form.ShowDialog();
                   ; break;
                //点击游客模式，就如游客模式界面，体验一分钟；
                case 2:                                   
                    MessageBox.Show("游客模式只有五分钟体验时间！");
                    time.Tick += time_Tick; 
                    Application.Run(index);                   
                    break;
                default:
                    break;
            }         
        }
        #region 自定义方法

        //时间处理方法>>>游客模式一分钟限定方法；
        static void time_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            if (lable)
            {
                lable = false;
                compare = Convert.ToInt32((time.Minute * 60 + time.Second));
            }
            if (Convert.ToInt32(time.Minute * 60 + time.Second)-compare == 100)
            {
                MessageBox.Show("时间已过一分钟40秒S,还剩三分钟20秒");
            }
            else if (Convert.ToInt32(time.Minute * 60 + time.Second) -compare == 200)
            {
                MessageBox.Show("时间已过三分钟20秒,还剩一分钟40秒");
            }
            else if (Convert.ToInt32(time.Minute * 60 + time.Second) - compare ==300)
            {
                MessageBox.Show("时间到了，体验结束，请注册在使用");
                System.Environment.Exit(0);
            }
        }
       
        #endregion
    }
}
