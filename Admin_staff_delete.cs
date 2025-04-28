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
    public partial class Admin_staff_delete : Form
    {
        public Admin_staff_delete()
        {
            InitializeComponent();
            combo();
        }
        Data obj = new Data();
        public void combo()
        {
            string query = "select User_id from Staff ";
            SqlDataReader dr = obj.ret_dr(query);
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from Staff where User_id=" + comboBox1.Text + "";
            DataSet ds = obj.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedUserId = comboBox1.Text;
            string query0 = "select Image from Staff where User_id=" + comboBox1.Text + "";
            string imagepath=obj.GetScalar(query0);
            if (!string.IsNullOrEmpty(imagepath) && File.Exists(imagepath))
            {
                try
                {
                    File.Delete(imagepath);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("error occurs"+ex.Message);
                }
                
            }
            string query = "delete from Staff where User_id=" + comboBox1.Text + "";
            if (obj.exec1(query) > 0)
            {
                string query2 = "delete from Login where User_id=" + comboBox1.Text + "";
                if (obj.exec1(query2) > 0)
                {
                    MessageBox.Show("deleted","Deleting",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    comboBox1.Items.Remove(selectedUserId);
                    dataGridView1.DataSource = null;
                }
            }        
        }
    }
}
