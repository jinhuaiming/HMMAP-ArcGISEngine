using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HmMap
{
    public partial class Distinguish : Form
    {
        private DataTable datatable ;
        public Distinguish(DataTable datatable)
        {
            this.datatable = datatable;
            InitializeComponent();
           
        }

        private void Distinguish_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = datatable;
        }
    }
}
