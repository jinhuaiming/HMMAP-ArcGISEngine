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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace HmMap
{
    public partial class Attribute : Form
    {
        private ILayer player;
        private AxMapControl mapcontrol;
        private ToolStripMenuItem toolstripmenuitem4;

        public Attribute(AxMapControl mapcontrol, ILayer player, ToolStripMenuItem toolstripmenuitem4)
        {
            InitializeComponent();
            this.mapcontrol = mapcontrol;
            this.player = player;
            this.toolstripmenuitem4 = toolstripmenuitem4;
        }
        
        //属性form关闭事件，恢复属性按钮；
        private void Attribute_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.toolstripmenuitem4.Enabled = true;
        }

    }
}
