using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using HZH_Controls.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DataSourcesRaster;

namespace HmMap
{
    public partial class index : Form
    {
        private  ButtonFunction x;
        private  ButtonFunction tool_lable//工具箱属性标识；
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public index()
        {
            InitializeComponent();
        }

        public enum ButtonFunction
        {
            roam = 1,//漫游；
            measure = 2,//测量；
            distinguish = 3,//识别；
            TrackCircle = 4,//按圆选择；
            TrackLine = 5,//按线路选择;
            TrackRectangle = 6,//按矩形选择;
            TrackPolygon = 7,//按多边形选择；
            enlarge = 8,//局部放大功能
            narrow=9,//局部缩小功能
        }

        #region 事件
        //主页加载函数；
        private void index_Load(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.roam;
            Form content = new 目录(axMapControl1, toolStripButton6);
            content.Show();
            toolStripButton6.Enabled = false;
        }
        //退出;
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //修改数据;
        private void 数据修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aa = new 显示();
            aa.ShowDialog();
        }

        //窗体关闭时确认事件;
        private void index_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定关闭HmMap吗？", "提示", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        //添加数据工具;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            this.tool_lable =ButtonFunction.roam;

            openFileDialog1.Title = "打开文件";
            openFileDialog1.Filter = "矢量文件(*.shp)|*.shp|ArcMap工程文件(*.mxd)|*.mxd|栅格影像文件|*.bmp;*.tif;*.img;*.jpg";
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string [] filename = openFileDialog1.FileNames;
                //添加矢量文件;
             
                if (System.IO.Path.GetExtension(openFileDialog1.FileName) == ".shp")
                {
                    for (int i = 0; i < filename.Length; i++)
                    {
                        string path = System.IO.Path.GetDirectoryName(filename[i]);
                        string filename_1 = System.IO.Path.GetFileName(filename[i]);
                        axMapControl1.AddShapeFile(path, filename_1); 
                    }

                }
                //添加ArcMaP工程文件;
                else if (System.IO.Path.GetExtension(openFileDialog1.FileName) == ".mxd")
                {
                    axMapControl1.LoadMxFile(openFileDialog1.FileName);
                }
                    //加载栅格数据文件；
                else if (System.IO.Path.GetExtension(openFileDialog1.FileName) == ".tif" || System.IO.Path.GetExtension(openFileDialog1.FileName) == ".img" || System.IO.Path.GetExtension(openFileDialog1.FileName) == ".bmp" || System.IO.Path.GetExtension(openFileDialog1.FileName) == ".jpg") 
                {
                    for (int i = 0; i < filename.Length; i++)
                    {
                        string path = System.IO.Path.GetDirectoryName(filename[i]);
                        string filename_1 = System.IO.Path.GetFileName(filename[i]);

                        IWorkspaceFactory pWorkspaceFactory = new RasterWorkspaceFactory();
                        IWorkspace pworkspace = pWorkspaceFactory.OpenFromFile(path, 0);

                        IRasterWorkspace pRasterWorkspace = pworkspace as IRasterWorkspace;
                        IRasterDataset prasterdataset = pRasterWorkspace.OpenRasterDataset(filename_1);

                        IRasterPyramid3 jzt;
                        jzt = prasterdataset as IRasterPyramid3;
                        if (jzt != null)
                        {
                            if (!(jzt.Present))
                            {
                                jzt.Create();
                            }
                        }

                        IRaster raster;
                        raster = prasterdataset.CreateDefaultRaster();
                        IRasterLayer rasterLayer;
                        rasterLayer = new RasterLayerClass();
                        rasterLayer.CreateFromRaster(raster);
                        ILayer layer = rasterLayer as ILayer;
                        axMapControl1.AddLayer(layer);
                        axMapControl1.ActiveView.Refresh();


                    } 
                }
            }
            else
            {
                MessageBox.Show("文件打开失败!");
                return;
            }

        }

        //漫游工具;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.roam;
        }

        //地图控件鼠标点击事件；;
        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {

            switch (this.tool_lable)
            {
                //漫游
                case ButtonFunction.roam: axMapControl1.Pan(); break;
                //   测量
                case ButtonFunction.measure:
                    MessageBox.Show("测量功能尚未完善!"); break;
                //识别
                case ButtonFunction.distinguish:
                    if (e.button == 1)
                    {
                        IPoint pt = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                       
                        IGeometry Geometry = pt as IGeometry;

                        IMap map = axMapControl1.Map;

                        ISelectionEnvironment SelectionEnvironment = new SelectionEnvironment();
                        IRgbColor color = new RgbColor();
                        color.Red = 233;
                        SelectionEnvironment.DefaultColor = color;
                        map.SelectByShape(Geometry, SelectionEnvironment, false);
                        axMapControl1.ActiveView.Refresh();

                        if (axMapControl1.Map.SelectionCount <= 0) { return; }
                        Form distinguish = new Distinguish(CreateDataTable(axMapControl1));//显示识别窗体，并把选择属性创建成表；
                        distinguish.Location = Control.MousePosition;//;窗体位置
                        distinguish.ShowDialog();
                    }
                    break;
                //按位置选择
                case ButtonFunction.TrackCircle: HightRight(axMapControl1, axMapControl1.TrackCircle());
                    break;
                //按位置选择
                case ButtonFunction.TrackLine: HightRight(axMapControl1, axMapControl1.TrackLine());
                    break;
                //按位置选择
                case ButtonFunction.TrackRectangle: HightRight(axMapControl1, axMapControl1.TrackRectangle());
                    break;
                //按位置选择
                case ButtonFunction.TrackPolygon: HightRight(axMapControl1, axMapControl1.TrackPolygon());
                    break;
                //局部放大功能；
                case ButtonFunction.enlarge:
                    IEnvelope envelope = axMapControl1.TrackRectangle();
                    axMapControl1.ActiveView.Extent = envelope;
                    axMapControl1.ActiveView.Refresh();
                    break;
                case ButtonFunction.narrow:
                    IEnvelope envelope_1 = axMapControl1.TrackRectangle();
                    double dWidth = Math.Pow(axMapControl1.ActiveView.Extent.Width, 2) / envelope_1.Width;
                    double dHeight = Math.Pow(axMapControl1.ActiveView.Extent.Height,2)/envelope_1.Height;
                    double dXmin=axMapControl1.ActiveView.Extent.XMin-((envelope_1.XMin-axMapControl1.ActiveView.Extent.XMin)*axMapControl1.ActiveView.Extent.Width/envelope_1.Width);
                    double dYmin = axMapControl1.ActiveView.Extent.YMin - ((envelope_1.YMin - axMapControl1.ActiveView.Extent.YMin) * axMapControl1.ActiveView.Extent.Height / envelope_1.Height);
                    double dXMax=dWidth+dXmin;
                    double dYMax = dHeight + dYmin;
                    envelope_1.PutCoords(dXmin,dYmin,dXMax,dYMax);
                    axMapControl1.Extent = envelope_1;
                    axMapControl1.ActiveView.Refresh();
                    break;
            }
        }

        //x,y位置显示lable;
        private void axMapControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            toolStripLabel1.Text = Math.Round(e.mapX, 3).ToString() + ',' + Math.Round(e.mapY, 3).ToString() + "   单位:" + GetMapUnits(axMapControl1);
            toolStripLabel2.Text = "比例尺  " + "1:" + Math.Round(axMapControl1.MapScale, 0).ToString();
        }

        //全图显示工具；
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.roam;
            axMapControl1.Extent = axMapControl1.FullExtent;
        }

        //测量工具;
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.measure;
        }

        //保存工具;
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.roam;
            //检测是否加载图层；
            if (axMapControl1.LayerCount == 0)
            {
                MessageBox.Show("无保存内容!");
                return;
            }
            IMapDocument mapdocunment = new MapDocumentClass();
            mapdocunment.Open(axMapControl1.DocumentFilename);
            mapdocunment.ReplaceContents(axMapControl1.Map as IMxdContents);
            mapdocunment.Save();
            MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK);
        }

        //地图文档另存为；
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //保存地图文档.mxd
            try
            {
                string smxdfilename = axMapControl1.DocumentFilename;
                IMapDocument pmapdocument = new MapDocumentClass();
                if (smxdfilename != null && axMapControl1.CheckMxFile(smxdfilename))
                {
                    if (pmapdocument.get_IsReadOnly(smxdfilename))
                    {
                        MessageBox.Show("本地图为只读文档，不能修改保存！");
                        pmapdocument.Close();
                        return;
                    }

                }
                else
                {
                    saveFileDialog1.Title = "选择保存路径";
                    saveFileDialog1.OverwritePrompt = true;
                    saveFileDialog1.Filter = "ArcMap文档(*.mxd)|*.mxd";
                    saveFileDialog1.RestoreDirectory = true;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        smxdfilename = saveFileDialog1.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
                pmapdocument.ReplaceContents(axMapControl1.Map as IMxdContents);
                pmapdocument.New(smxdfilename);
                pmapdocument.Save(true, true);
                pmapdocument.Close();
                MessageBox.Show("地图保存成功!");

            }
            catch (Exception arr)
            {
                MessageBox.Show("错误:" + arr.Message, "提示", MessageBoxButtons.OK);
            }
        }

        //目录加载工具；
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Form content = new 目录(axMapControl1, toolStripButton6);
            toolStripButton6.Enabled = false;
            content.Show();
        }

        //要素识别事件
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.distinguish;
        }

        //清除地图控件选择的数据；
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.roam;
            IActiveView ActiveView = axMapControl1.ActiveView;
            ActiveView.FocusMap.ClearSelection();
            ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection,null,ActiveView.Extent);
        }

        //按位置选择
        private void 按圆选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.TrackCircle;

        }

        //按位置选择
        private void 按路径选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.TrackLine;
        }

        //按位置选择
        private void 按矩形框选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.TrackPolygon;
        }

        //按位置选择
        private void anToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.TrackRectangle;
        }

        //局部放大功能
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.enlarge;
        }

        //局部缩小功能
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            this.tool_lable = ButtonFunction.narrow;
        }

       //打开属性查询页面
        private void 按属性选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form attribute_find = new 属性查询(axMapControl1);
            attribute_find.ShowDialog();
        }
        #endregion
        #region 方法
        //获取当前地图单位；
        private string GetMapUnits(AxMapControl aa)
        {
            string map_unit = aa.Map.MapUnits.ToString();
            if (map_unit == "esriCentimeters")
            {
                return "厘米";
            }
            else if (map_unit == "esriDecimalDegrees")
            {
                return "十进制";
            }
            else if (map_unit == "esriMeters")
            {
                return "米";
            }
            else
            {
                return "未知";
            }
        }
        //高亮显示
        private void HightRight(AxMapControl axMapControl, IGeometry Geometry)
        {
            IMap map = axMapControl.Map;

            ISelectionEnvironment SelectionEnvironment = new SelectionEnvironment();
            IRgbColor color = new RgbColor();
            color.Red = 233;
            SelectionEnvironment.DefaultColor = color;

            map.SelectByShape(Geometry, SelectionEnvironment, false);
            axMapControl.ActiveView.Refresh();
        }
        //给选择的要素,创建表；
        private DataTable CreateDataTable(AxMapControl axMapControl)
        {
            DataTable datatable = new DataTable("属性表");
            DataRow row = null;
            DataColumn column;

            column = new DataColumn("属性");
            column.ColumnName = "属性";
            column.AllowDBNull = true;
            column.Caption = "属性";
            column.DataType = System.Type.GetType("System.String");
            column.DefaultValue = " ";
            datatable.Columns.Add(column);

            column = new DataColumn("值");
            column.ColumnName = "值";
            column.AllowDBNull = true;
            column.Caption = "值";
            column.DataType = System.Type.GetType("System.String");
            column.DefaultValue = " ";
            datatable.Columns.Add(column);


            IEnumFeatureSetup iEnumFeatureSetup = (IEnumFeatureSetup)axMapControl.Map.FeatureSelection;
            iEnumFeatureSetup.AllFields = true;
            IEnumFeature enumFeature = (IEnumFeature)iEnumFeatureSetup;
            enumFeature.Reset();
            IFeature feature = enumFeature.Next();

            while (feature != null)
            {
                for (int i = 0; i < feature.Fields.FieldCount; i++)
                {
                    string value = string.Empty;
                    if (feature.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        value = "polgony";
                    }
                    else if (feature.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeBlob)
                    {
                        value = "Element";
                    }
                    else
                    {
                        value = feature.get_Value(i).ToString();
                    }
                    row = datatable.NewRow();
                    row[0] = feature.Fields.Field[i].Name;
                    row[1] = value;
                    datatable.Rows.Add(row);
                }
                feature = enumFeature.Next();
            }

            return datatable;
        }
    }
        #endregion

















}
