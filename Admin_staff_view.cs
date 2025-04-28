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
    public partial class Admin_staff_view : Form
    {
        public Admin_staff_view()
        {
            InitializeComponent();
            combo();
            imageview();
        }
        Data obj = new Data();
        private EventHandler listView1_DoubleClick;
        private object selectedItem;

        public void combo()
        {
            string query = "select * from Staff order by Staff_id asc";
            SqlDataReader dr = obj.ret_dr(query);
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }
        public void grid()
        {
            string query = "select * from Staff order by Staff_id asc";
            DataSet ds = obj.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        public void imageview()
        {
            listView1.Items.Clear();
            imageList1.Images.Clear();
            string query = "select Image from Staff order by Image asc";
            SqlDataReader dr = obj.ret_dr(query);
            int imageindex = 0;
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imageList1;
            while (dr.Read())
            {
                string imagepath = dr[0].ToString();
                if (File.Exists(imagepath))
                {
                    Bitmap image = new Bitmap(imagepath);
                    imageList1.Images.Add(image);
                    string imageNameWithoutExtension = Path.GetFileNameWithoutExtension(imagepath);//this line for showing User id with image
                    ListViewItem imageItem = new ListViewItem
                    {
                        ImageIndex = imageindex,
                        Text = imageNameWithoutExtension,//this is also used for showing id with image
                        ForeColor = Color.White
                    };
                    listView1.Items.Add(imageItem);
                    imageindex++;
                }
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select * from Staff";
            DataSet ds = obj.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            listView1.Items.Clear();
            imageList1.Images.Clear();
            string query2 = "select * from Staff";
            SqlDataReader dr = obj.ret_dr(query2);
            int imageindex = 0;
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imageList1;
            while(dr.Read())
            {
                string imagepath = dr[8].ToString();
                if (File.Exists(imagepath))
                {
                    Bitmap image = new Bitmap(imagepath);
                    imageList1.Images.Add(image);
                    string imageNameWithoutExtension = Path.GetFileNameWithoutExtension(imagepath);
                    ListViewItem imageItem = new ListViewItem
                    {
                        ImageIndex = imageindex,
                        Text = imageNameWithoutExtension
                    };
                    listView1.Items.Add(imageItem);
                    imageindex++;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            imageList1.Images.Clear();
            string query = "select * from Staff where Staff_id=" + comboBox1.Text + "";
            DataSet ds = obj.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            SqlDataReader dr = obj.ret_dr(query);
            int imageindex = 0;
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imageList1;
            while (dr.Read())
            {
                string imagepath = dr[8].ToString();
                if (File.Exists(imagepath))
                {
                    Bitmap image = new Bitmap(imagepath);
                    imageList1.Images.Add(image);
                    string imageNameWithoutExtension = Path.GetFileNameWithoutExtension(imagepath);
                    ListViewItem item = new ListViewItem
                    {
                        ImageIndex = imageindex,
                        Text = imageNameWithoutExtension
                    };
                    listView1.Items.Add(item);
                    imageindex++;
                }
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            // Ensure an item is selected
            if (listView1.SelectedItems.Count > 0)
            {
                // Get the selected item's text (this holds the ID or image name)
                string selectedId = listView1.SelectedItems[0].Text;
                string query = $"SELECT * FROM Staff WHERE Image LIKE '%{selectedId}%'";
                DataSet ds = obj.ret_ds(query);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
        }
    }
}
   