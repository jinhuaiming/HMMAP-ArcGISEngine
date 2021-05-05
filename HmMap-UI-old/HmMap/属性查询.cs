using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HZH_Controls.Controls;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace HmMap
{
    public partial class 属性查询 : Form
    {
        private AxMapControl axmapcontrol;
        ILayer SelectLayer;
        public 属性查询(AxMapControl axmapcontrol)
        {
            this.axmapcontrol = axmapcontrol;
            InitializeComponent();
        }

        private void 属性查询_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < axmapcontrol.LayerCount; i++)
            {
                comboBox1.Items.Add(axmapcontrol.get_Layer(i).Name);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            for (int i = 0; i < axmapcontrol.LayerCount; i++)
            {
                if (axmapcontrol.get_Layer(i).Name ==comboBox1.SelectedItem.ToString())
                {
                    //选中的图层
                    SelectLayer = axmapcontrol.get_Layer(i);

                    //图层的字段遍历到listview1中；
                    ITable table = SelectLayer as ITable;
                    for (int j = 0; j < table.Fields.FieldCount; j++)
                    {
                         listView1.Items.Add(table.Fields.Field[j].Name);
                    }
                    break;
                }
            }
        }
    }
}
