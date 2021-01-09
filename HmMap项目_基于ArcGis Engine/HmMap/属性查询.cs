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
            for (int i = 0; i < axmapcontrol.LayerCount; i++)
            {
                if (axmapcontrol.get_Layer(i).Name == comboBox1.SelectedText)
                {
                    ITable table = axmapcontrol.get_Layer(i) as ITable;
                    for (int j = 0; j < table.Fields.FieldCount; j++)
                    {
                        listView1.Items.Add(table.Fields.Field[i].Name);
                    }
                }
            }
        }
    }
}
