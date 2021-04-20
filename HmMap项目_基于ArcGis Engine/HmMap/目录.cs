using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
namespace HmMap
{
    public partial class 目录 : Form
    {
      
        AxMapControl mapcontrol;//index界面，窗体的地图控件；
        esriTOCControlItem item;
        ToolStripButton toolstripbutton;//index界面，窗体的目录按钮；
        ILayer layer_Toccontrol;//目录界面，目录界面；
        IBasicMap map;//目录界面，数据框；
        object other;
        object index;
        esriGeometryType pesriGeometryTyp;//判断图层的点线面类型；
        //点符号类型；
       public  static esriSimpleMarkerStyle pesriSimpleMarkerStyle;
       public static IColor pcolor;
       public static double size;
       public static double angle;

        public 目录(AxMapControl mapcontrol,ToolStripButton  toolstripbutton)
        {
            InitializeComponent();
            this.mapcontrol = mapcontrol;
            this.toolstripbutton = toolstripbutton;
        }

        //窗体加载事件
        private void 目录_Load(object sender, EventArgs e)
        {
            axTOCControl1.SetBuddyControl(mapcontrol);           
        }

        //窗体关闭事件；
        private void 目录_FormClosed(object sender, FormClosedEventArgs e)
        {
            toolstripbutton.Enabled = true;
        }


        //ACTOOCONTROL右键功能；
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {           
                axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer_Toccontrol, ref other, ref index);
                if (item == esriTOCControlItem.esriTOCControlItemLayer&&layer_Toccontrol!=null)
                {
                    IFeatureLayer pfeature = layer_Toccontrol as IFeatureLayer;
                    this.pesriGeometryTyp = pfeature.FeatureClass.ShapeType;
                    contextMenuStrip1.Show(Control.MousePosition);
                }
            }
        }
       
        //移除图层
        private void remov_layer(object sender, EventArgs e)
        {
            mapcontrol.Map.DeleteLayer(layer_Toccontrol);
            mapcontrol.ActiveView.Refresh();
        }

        //图层全图显示
        private void all_picture(object sender, EventArgs e)
        {
            MessageBox.Show("功能尚未完成");
        }

        //打开属性表
        private void show_sheet(object sender, EventArgs e)
        {
            Form property_sheet = new Property_sheet(layer_Toccontrol, layer_Toccontrol.Name, toolStripMenuItem2);
            property_sheet.Show();
            toolStripMenuItem2.Enabled = false;
        }

        //缩放至图层；
        private void 缩放至ToolStripMenuItem_Click(object sender, EventArgs e)
        {                   
            if (layer_Toccontrol == null) return;
            (mapcontrol.Map as IActiveView).Extent = layer_Toccontrol.AreaOfInterest;
            mapcontrol.Refresh();
        }

        //打开属性
        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (layer_Toccontrol == null) return;
            Form  pattribute = new Attribute(mapcontrol, layer_Toccontrol, ToolStripMenuItem4);
            ToolStripMenuItem4.Enabled = false;
            pattribute.ShowDialog();      
        }

        //打开符号系统界面；
        private void 修改符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (pesriGeometryTyp)
            {
                case esriGeometryType.esriGeometryPoint:
                    ToolStripMenuItem5.Enabled = false;
                    IGeoFeatureLayer pGeoFeatureLayer = layer_Toccontrol as IGeoFeatureLayer;
                    ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();

                    ISimpleRenderer ppSimpleRenderer = pGeoFeatureLayer.Renderer as ISimpleRenderer;
                     ISimpleMarkerSymbol ppSymbol = ppSimpleRenderer.Symbol as ISimpleMarkerSymbol;
                     size = ppSymbol.Size;
                     pcolor = ppSymbol.Color;
                     angle = ppSymbol.Angle;

                    //选择符号的大小，颜色，符号类型
                    Form symbol = new symbol(ToolStripMenuItem5,pcolor,size,angle);
                    symbol.ShowDialog();

                    pSimpleMarkerSymbol.Size = size;
                    pSimpleMarkerSymbol.Color = pcolor;
                    pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    pSimpleMarkerSymbol.Angle = angle;

                    ISimpleRenderer pSimpleRenderer = new SimpleRenderer();
                    pSimpleRenderer.Symbol = pSimpleMarkerSymbol as ISymbol;

                    pGeoFeatureLayer.Renderer = pSimpleRenderer as IFeatureRenderer;

                    mapcontrol.Refresh();
                    axTOCControl1.Update();
                    break;
                default: break;
            }


           
           
        }     
    }
}
