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
            //获取目标图层；
            IFeatureLayer[] player=new IFeatureLayer[5];
            int counter=0;

            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                for (int j = 0; j < pmapcontrol.LayerCount; j++)
                {
                    if (checkedListBox1.CheckedItems[i].ToString()==pmapcontrol.get_Layer(j).Name)
                    {
                        player[counter++] = pmapcontrol.get_Layer(j) as IFeatureLayer;
                    }
                }
            }
         

            // 获取源图层；       
            IFeatureLayer pyuanLyaer=null;
            for (int k = 0; k < pmapcontrol.LayerCount; k++)
            {
                if (comboBox1.GetItemText(comboBox1.SelectedItem)==pmapcontrol.get_Layer(k).Name)
                {
                    pyuanLyaer = pmapcontrol.get_Layer(k) as IFeatureLayer;
                }
            }

            //空间参考
            esriSpatialRelEnum pesriSpatialRelEnum=SpatialRelConvert(comboBox2.GetItemText(comboBox2.SelectedItem));


            //空间位置判断
            if (pyuanLyaer!=null&&player.Length>=1)
            {           

                ISpatialFilter pISpatialFilter = new SpatialFilterClass();
                for (int i = 0; i <player.Length&&player[i]!=null; i++)
                {
                    IFeatureCursor pFeatureCursor = pyuanLyaer.FeatureClass.Search(null, true);
                    IFeature pfeature = pFeatureCursor.NextFeature();
                    IFeatureSelection pIFeatureSelection = player[i] as   IFeatureSelection;
                    while (pfeature!=null)
                    {
                        pISpatialFilter.Geometry = pfeature.ShapeCopy;
                        pISpatialFilter.SpatialRel = pesriSpatialRelEnum;
                        pIFeatureSelection.SelectFeatures(pISpatialFilter as IQueryFilter,esriSelectionResultEnum.esriSelectionResultAdd,true );
                        pfeature = pFeatureCursor.NextFeature();
                    }                 
                }
                pmapcontrol.Refresh();
                this.Close();
            }
            else
            {
                MessageBox.Show("请至少选择一个目标图层，原图层和空间关系方式！");
            }
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
