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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
namespace HmMap
{
    public partial class Property_sheet : Form
    {
        
        private ILayer layer;//index界面，传回的选定的图层；
        private string tablenmae;//index界面，传回的选定的图层的名字；
        ToolStripMenuItem toolStripMenuItem;//目录界面的选定按钮对象；

        public static string FieldName;//字段名称；
        public static esriFieldType FieldType;//字段数据类型；
        public static int FieldLength;//字段数据长度；

        public Property_sheet(ILayer layer, string TableName, ToolStripMenuItem toolStripMenuItem)
        {
            InitializeComponent();
            this.layer = layer;
            this.tablenmae = TableName;
            this.toolStripMenuItem = toolStripMenuItem;
        }

        #region  事件
        //属性表Load事件
        private void Property_sheet_Load(object sender, EventArgs e)
        {
            int n;
            dataGridView2.Visible = false;
            dataGridView1.DataSource = FillDataTable(layer, tablenmae);
            toolStripLabel1.Text = string.Format("要素数目：{0}     选中要素数目：{1}", (dataGridView1.RowCount - 1).ToString(), dataGridView1.SelectedRows.Count.ToString());
        }

        //选中要素后，统计要素事件
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == dataGridView1.RowCount)
            {
                toolStripLabel1.Text = string.Format("要素数目：{0}     选中要素数目：{1}", (dataGridView1.RowCount - 1).ToString(), (dataGridView1.RowCount - 1).ToString());
                return;
            }
            toolStripLabel1.Text = string.Format("要素数目：{0}     选中要素数目：{1}", (dataGridView1.RowCount - 1).ToString(), dataGridView1.SelectedRows.Count.ToString());
        }

        //显示选中数据；
        private void toolStripButton1_Click(object sender, EventArgs e)
        {



            try
            {
                DataTable table = new DataTable();
                DataColumn colume = null;
                DataRow row = null;
                for (int i = 0; i < dataGridView1.SelectedCells.Count / dataGridView1.SelectedRows.Count; i++)
                {
                    colume = new DataColumn();
                    colume.ColumnName = dataGridView1.Columns[i].Name;
                    colume.DataType = dataGridView1.Columns[i].ValueType;
                    colume.AllowDBNull = true;
                    table.Columns.Add(colume);
                }


                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    row = table.NewRow();
                    for (int j = 0; j < dataGridView1.SelectedCells.Count / dataGridView1.SelectedRows.Count; j++)
                    {

                        row[j] = dataGridView1.SelectedRows[i].Cells[j].Value;
                    }
                    table.Rows.Add(row);
                }
                dataGridView2.DataSource = table;
                dataGridView2.Visible = true;
            }
            catch (Exception arr)
            {
                MessageBox.Show("错误:" + arr.Message);
            }
            toolStripButton1.Enabled = false;
        }

        //清楚选中数据；
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Visible = false;
            toolStripButton1.Enabled = true;
        }

        //窗体关闭事件
        private void Property_sheet_FormClosed(object sender, FormClosedEventArgs e)
        {
            toolStripMenuItem.Enabled = true;
        }

        //添加字段；
        private void 添加字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureLayer player = layer as IFeatureLayer;
            IFeatureClass pfeatureclass = player.FeatureClass;
            PopupField pf = new PopupField();
            pf.ShowDialog();

            if (FieldName!=string.Empty&&FieldType!=null&&FieldLength!=null)
            {
                IFieldEdit fe = new FieldClass();
                fe.Name_2 = FieldName;
                fe.Type_2 = FieldType;
                fe.Length_2 = FieldLength;
                pfeatureclass.AddField(fe as IField);
            }
        }

       // 按属性选择
        private void 按属性选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("功能尚未完善");
        }

        #region 方法
        #region 打开属性表

        //创建带字段空表；
        private static DataTable CreateDataTable(ILayer layer, string TableName)
        {

            DataTable datatables = new DataTable(TableName);

            ITable table = layer as ITable;
            IField pfield = null;

            DataColumn datacolumn;
            for (int i = 0; i < table.Fields.FieldCount; i++)
            {
                pfield = table.Fields.get_Field(i);
                datacolumn = new DataColumn(pfield.Name);
                if (pfield.Name == table.OIDFieldName)
                {
                    datacolumn.Unique = true;
                }
                datacolumn.ColumnName = pfield.Name;
                datacolumn.AllowDBNull = pfield.IsNullable;
                datacolumn.Caption = pfield.AliasName;
                datacolumn.DataType = System.Type.GetType(ParseFieldType(pfield.Type));
                datacolumn.DefaultValue = pfield.DefaultValue;
                if (pfield.VarType == 8)
                {
                    datacolumn.MaxLength = pfield.Length;
                }

                datatables.Columns.Add(datacolumn);
            }
            return datatables;
        }

        //填充datatable数据
        private static DataTable FillDataTable(ILayer layer, string TableName)
        {
            DataTable datatables = CreateDataTable(layer, TableName);
            string shapetype = getShapeType(layer);
            DataRow datarow = null;
            ITable table = layer as ITable;
            ICursor cursor = table.Search(null, false);
            IRow row = cursor.NextRow();
            while (row != null)
            {
                datarow = datatables.NewRow();
                for (int i = 0; i < row.Fields.FieldCount; i++)
                {
                    if (row.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        datarow[i] = shapetype;
                    }
                    else if (row.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeBlob)
                    {
                        datarow[i] = "Element";
                    }
                    else
                    {
                        datarow[i] = row.get_Value(i);
                    }
                }
                datatables.Rows.Add(datarow);
                row = cursor.NextRow();
            }
            return datatables;
        }

        //esri属性表类型转化为.net数据类型方法
        public static string ParseFieldType(esriFieldType fieldType)
        {

            switch (fieldType)
            {

                case esriFieldType.esriFieldTypeBlob:

                    return "System.String";

                case esriFieldType.esriFieldTypeDate:

                    return "System.DateTime";

                case esriFieldType.esriFieldTypeDouble:

                    return "System.Double";

                case esriFieldType.esriFieldTypeGeometry:

                    return "System.String";

                case esriFieldType.esriFieldTypeGlobalID:

                    return "System.String";

                case esriFieldType.esriFieldTypeGUID:

                    return "System.String";

                case esriFieldType.esriFieldTypeInteger:

                    return "System.Int32";

                case esriFieldType.esriFieldTypeOID:

                    return "System.String";

                case esriFieldType.esriFieldTypeRaster:

                    return "System.String";

                case esriFieldType.esriFieldTypeSingle:

                    return "System.Single";

                case esriFieldType.esriFieldTypeSmallInteger:

                    return "System.Int32";

                case esriFieldType.esriFieldTypeString:

                    return "System.String";

                default:

                    return "System.String";

            }

        }
        //获取图层类型
        public static string getShapeType(ILayer pLayer)
        {

            IFeatureLayer pFeatLyr = (IFeatureLayer)pLayer;

            switch (pFeatLyr.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint: return "Point";
                    break;
                case esriGeometryType.esriGeometryPolyline: return "Polyline";
                    break;
                case esriGeometryType.esriGeometryPolygon: return "Polygon";
                    break;
                default:
                    return "";
            }

        }
        #endregion  
        #endregion

    

        #endregion
    }
}
