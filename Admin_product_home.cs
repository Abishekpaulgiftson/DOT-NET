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
    public partial class Admin_product_home : Form
    {
        public Admin_product_home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin_product_adding obj = new Admin_product_adding();
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin_product_view obj = new Admin_product_view();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin_stock_adding obj = new Admin_stock_adding();
            obj.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Admin_stock_viewing obj = new Admin_stock_viewing();
            obj.Show();
        }

        private void Admin_product_home_Load(object sender, EventArgs e)
        {

        }
    }
}
