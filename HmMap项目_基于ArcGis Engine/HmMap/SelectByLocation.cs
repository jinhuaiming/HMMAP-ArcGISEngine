using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
namespace HmMap
{
    public partial class SelectByLocation : Form
    {
        AxMapControl pmapcontrol;
        public SelectByLocation(AxMapControl paxmapcontrol)
        {
            this.pmapcontrol = paxmapcontrol;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("功能正在完善中>>>>>");
        }

        private void SelectByLocation_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <pmapcontrol.LayerCount; i++)
            {
                string LayerName=pmapcontrol.get_Layer(i).Name;
                checkedListBox1.Items.Add(LayerName);
                comboBox1.Items.Add(LayerName);
            }
        }

        //空间选择方式转化
        private esriSpatialRelEnum SpatialRelConvert(string Spatial)
        {
            esriSpatialRelEnum Spatial_lable=esriSpatialRelEnum.esriSpatialRelContains;
            switch (Spatial)
            {
                case "esriSpatialRelUndefined": Spatial_lable = esriSpatialRelEnum.esriSpatialRelUndefined; break;
                case "esriSpatialRelIntersects":Spatial_lable=esriSpatialRelEnum.esriSpatialRelIndexIntersects; break;
                case "esriSpatialRelEnvelopeIntersects": Spatial_lable = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects; break;
                case "esriSpatialRelIndexIntersects": Spatial_lable = esriSpatialRelEnum.esriSpatialRelIntersects; break;
                case "esriSpatialRelTouches": Spatial_lable = esriSpatialRelEnum.esriSpatialRelTouches; break;
                case "esriSpatialRelOverlaps": Spatial_lable = esriSpatialRelEnum.esriSpatialRelOverlaps; break;
                case "esriSpatialRelCrosses": Spatial_lable = esriSpatialRelEnum.esriSpatialRelCrosses; break;
                case "esriSpatialRelWithin": Spatial_lable = esriSpatialRelEnum.esriSpatialRelWithin; break;
                case "esriSpatialRelContains": Spatial_lable = esriSpatialRelEnum.esriSpatialRelContains; break;
                case "esriSpatialRelRelation": Spatial_lable = esriSpatialRelEnum.esriSpatialRelRelation; break;
                default:
                    break;
            }
            return Spatial_lable;
        }
    }
}
