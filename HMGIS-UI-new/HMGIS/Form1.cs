using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using DevExpress.XtraBars.Ribbon;


namespace HMGIS
{
    public partial class Form1 : RibbonForm
    {
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                xtraOpenFileDialog1.Title = "添加数据";
                xtraOpenFileDialog1.Filter = "shapefile文件(*.shp)|*.shp";
                xtraOpenFileDialog1.Multiselect = true;
                if (xtraOpenFileDialog1.ShowDialog()==DialogResult.OK)
                {
                    string[] filenames= xtraOpenFileDialog1.FileNames;
                    string Directorypath = Path.GetDirectoryName(filenames[0]);
                    for (int i = 0; i <filenames.Length; i++)
                    {
                        string filename = Path.GetFileName(filenames[i]);
                        axMapControl1.AddShapeFile(Directorypath,filename);
                    }
                }
                axMapControl1.Refresh();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
