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
using ESRI.ArcGIS.Display;

namespace HmMap
{
    public partial class symbol : Form
    {
        ToolStripMenuItem pToolStripMenuItem;
         IColor pcolor;
        double size;
        double angle;
        public symbol(ToolStripMenuItem pToolStripMenuItem, IColor pcolor, double size, double angle)
        {
            InitializeComponent();
            this.pToolStripMenuItem = pToolStripMenuItem;
            this.size = size;
            this.angle = angle;
            this.pcolor = pcolor;
        }

        private void symbol_FormClosed(object sender, FormClosedEventArgs e)
        {
            pToolStripMenuItem.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button1.BackColor = colorDialog1.Color;
            IColor ppcolor = new RgbColor();
            ppcolor.RGB = RGB(colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);
            目录.pcolor = ppcolor;   
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value<-360||numericUpDown2.Value>360)
            {
                numericUpDown2.Value = 0;
                MessageBox.Show("请输入-360度-360度");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            目录.size =Convert.ToDouble(numericUpDown1.Value);
            目录.angle = Convert.ToDouble(numericUpDown2.Value);
            this.Close();
        }

       //简单RGB转256级模型
        private int RGB(byte R,byte G ,byte B){
            return ((R & 0xc0) | (G & 0x38) | (B & 0x07));
        }

        private void symbol_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Convert.ToDecimal(size);
            numericUpDown2.Value = Convert.ToDecimal(angle);
        }
    }
}
