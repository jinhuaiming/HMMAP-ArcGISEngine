using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace HmMap
{
    public partial class PopupField : Form
    {
        public PopupField()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Property_sheet.FieldName = textBox1.Text.Trim();
            Property_sheet.FieldType = ConversionFieldType(comboBox1.SelectedText);
            Property_sheet.FieldLength = int.Parse(numericUpDown1.Value.ToString());
            this.Close();
        }

        private esriFieldType  ConversionFieldType(string FieldType) {
            esriFieldType efd=esriFieldType.esriFieldTypeString;
            switch (FieldType)
            {
                case "Short Integer": efd=esriFieldType.esriFieldTypeSmallInteger; break;
                case "Long Integer": efd=esriFieldType.esriFieldTypeInteger; break;
                case "Single-precision floating-point num":efd=esriFieldType.esriFieldTypeSingle;break;
                case "Double-precision floating-point number": efd = esriFieldType.esriFieldTypeDouble; break;
                case "Character string": efd= esriFieldType.esriFieldTypeString; break;
                case "Date": efd = esriFieldType.esriFieldTypeDate; break;
                case "Long Integer representing an object identifier": efd = esriFieldType.esriFieldTypeOID; break;
                case "Geometry": efd = esriFieldType.esriFieldTypeGeometry; break;
                case "Binary Large Object": efd = esriFieldType.esriFieldTypeBlob; break;
                case "Raster": efd = esriFieldType.esriFieldTypeRaster; break;
                case "Globally Unique Identifier": efd = esriFieldType.esriFieldTypeGUID; break;
                case "Esri Global ID": efd = esriFieldType.esriFieldTypeGlobalID; break;
                case "XML Document": efd = esriFieldType.esriFieldTypeXML; break;
            }
            return efd;
        }

    }
}
