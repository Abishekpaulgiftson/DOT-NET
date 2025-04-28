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
    public partial class Admin_product_adding : Form
    {
        Data obj = new Data();
        public Admin_product_adding()
        {
            InitializeComponent();
            Product_id();
            
        }
        public void Product_id()
        {
            string query = "select isnull(max(Product_id),0)+1 as newProduct_id from Product";
            SqlDataReader dr = obj.ret_dr(query);
            if (dr.Read())
            {
                textBox2.Text = dr[0].ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.Title = "Select an Image File";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                textBox1.Text = openFileDialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string folder = Path.Combine(Application.StartupPath, "Product_image");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string img = Path.Combine(folder, textBox2.Text + Path.GetExtension(textBox1.Text));
            File.Copy(textBox1.Text, img, true);
            string link = Path.Combine("Product_image", textBox2.Text, Path.GetExtension(textBox1.Text));
            string query = "insert into Product(Product_id,Name,Company,Price,Description,Image) values(" + textBox2.Text + ",'" + textBox3.Text + "','" + textBox4.Text + "'," + textBox5.Text + ",'" + textBox6.Text + "','" + link + "')";
            if (obj.exec1(query) > 0)
            {
                MessageBox.Show("successfully inserted");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.')
            {
                // Check if a dot already exists
                if (textBox5.Text.Contains("."))
                {
                    e.Handled = true; // Prevent the second dot
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Admin_product_adding_Load(object sender, EventArgs e)
        {
            string query = "select * from Product";
            obj.exec1(query);
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
            SqlDataReader dr = obj.ret_dr(query);
            while (dr.Read())
            {
                autoCompleteStringCollection.Add(dr[4].ToString());
            }
            textBox6.AutoCompleteCustomSource = autoCompleteStringCollection;
            textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox6.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        
    }
}
