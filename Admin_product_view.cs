using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_management_system
{
    public partial class Admin_product_view : Form
    {
        public Admin_product_view()
        {
            InitializeComponent();
            combo();
        }
        Data obj = new Data();
        public void combo()
        {
            string query = "select * from Product";
            SqlDataReader dr = obj.ret_dr(query);
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from Product where Product_id=" + comboBox1.Text + "";
            DataSet ds = obj.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select * from Product";
            DataSet d = obj.ret_ds(query);
            dataGridView1.DataSource=d.Tables[0].DefaultView;
        }
    }
}
