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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace HmMap
{
    public partial class Buffer : Form
    {
        private AxMapControl pmapcontrol;
        public Buffer(AxMapControl pMapControl)
        {
            this.pmapcontrol = pMapControl;
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            IActiveView pIActiveView = pmapcontrol.Map as IActiveView;
            IGraphicsContainer pIGraphicsContainer = pIActiveView.GraphicsContainer;
            pIGraphicsContainer.DeleteAllElements();

            IFeatureLayer pIFeatureLayer = pmapcontrol.get_Layer(0) as IFeatureLayer;
            IFeatureCursor pIFeatureCursor = pIFeatureLayer.FeatureClass.Search(null, false);

            IFeature pfeature = pIFeatureCursor.NextFeature();

            while (pfeature != null)
            {
                ITopologicalOperator pITopologicalOperator = pfeature.Shape as ITopologicalOperator;
                IPolygon pPolygon = pITopologicalOperator.Buffer(double.Parse(textBox1.Text)) as IPolygon;

                IElement pIElement = new PolygonElement();
                pIElement.Geometry = pPolygon;

                pIGraphicsContainer.AddElement(pIElement, 0);

                pfeature = pIFeatureCursor.NextFeature();
            }
            pmapcontrol.Refresh();
            this.Close();
        }
    }
}
