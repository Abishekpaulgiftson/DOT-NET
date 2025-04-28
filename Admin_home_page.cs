using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_management_system
{
    public partial class Admin_home_page : Form
    {
        public Admin_home_page()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin_staff_home admin_Staff_Home = new Admin_staff_home();
            admin_Staff_Home.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin_product_home obj = new Admin_product_home();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin_sales_view obj = new Admin_sales_view();
            obj.Show();
        }



        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Admin_staff_home admin_Staff_Home = new Admin_staff_home();
            admin_Staff_Home.Show();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            Admin_product_home obj = new Admin_product_home();
            obj.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Admin_sales_view obj = new Admin_sales_view();
            obj.Show();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Admin_staff_home admin_Staff_Home = new Admin_staff_home();
            admin_Staff_Home.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Admin_product_home obj = new Admin_product_home();
            obj.Show();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Admin_sales_view obj = new Admin_sales_view();
            obj.Show();
        }
    }
}
