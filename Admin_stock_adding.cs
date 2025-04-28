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
    public partial class Admin_stock_adding : Form
    {
        public Admin_stock_adding()
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
            SqlDataReader dr = obj.ret_dr(query);
            if (dr.Read())
            {
                textBox1.Text = dr[5].ToString();
                textBox2.Text = dr[3].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "update Product set No_of_unit=" + textBox1.Text + ",Price=" + textBox2.Text + " where Product_id=" + comboBox1.Text + "";
            if (obj.exec1(query) > 0)
            {
                MessageBox.Show("successfully updated", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
