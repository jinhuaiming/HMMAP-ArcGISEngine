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

namespace HmMap
{
    public partial class EagleEye : Form
    {
       AxMapControl pMapControl;
       Form IndexForm;
        public EagleEye(AxMapControl pMapControl1,Form pform)
        {            
            InitializeComponent();
            this.pMapControl = pMapControl1;
            this.IndexForm = pform;
        }

          


    }
}
