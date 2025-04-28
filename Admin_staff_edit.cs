using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_management_system
{
    public partial class Admin_staff_edit : Form
    {
        public bool picture = false;
        public bool text = false;

        public Admin_staff_edit()
        {
            InitializeComponent();
            combo();
        }
        Data obj = new Data();
        public void combo()
        {
            string query = "select * from Staff";
            SqlDataReader sql = obj.ret_dr(query);
            while (sql.Read())
            {
                comboBox1.Items.Add(sql[1].ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            picture = true;

            using (OpenFileDialog open = new OpenFileDialog())
                //open.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                //open.Title = "Select an Image File";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    pictureBox1.Image = new Bitmap(open.FileName);
                    textBox7.Text = open.FileName;
                }

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            int age = DateTime.Now.Year - date.Year;
            if (age < 18)
            {
                MessageBox.Show("Age invalid", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                textBox3.Text = age.ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from Staff where User_id=" + comboBox1.Text + "";
            SqlDataReader dr = obj.ret_dr(query);
            if (dr.Read())
            {
                textBox1.Text = dr[2].ToString();
                dateTimePicker1.Text = dr[3].ToString();
                textBox3.Text = dr[4].ToString();
                textBox4.Text = dr[5].ToString();
                textBox5.Text = dr[6].ToString();
                textBox6.Text = dr[7].ToString();
                textBox7.Text = dr[8].ToString();
                string img = dr[8].ToString();
                if (File.Exists(img))
                {
                    pictureBox1.Image = new Bitmap(img);
                }
                else
                {
                    pictureBox1.Image = null;
                    MessageBox.Show("there is no image");
                }
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }
            if (!picture && !text)
            {
                MessageBox.Show("enter a valid data");
                return;
            }
            else
            {
                if (picture && text)
                {
                    string query = "select * from Staff where User_id=" + comboBox1.Text + "";
                    SqlDataReader dr = obj.ret_dr(query);
                    if (dr.Read())
                    {
                        textBox1.Text = dr[2].ToString();
                        dateTimePicker1.Text = dr[3].ToString();
                        textBox3.Text = dr[4].ToString();
                        textBox4.Text = dr[5].ToString();
                        textBox5.Text = dr[6].ToString();
                        textBox6.Text = dr[7].ToString();
                        textBox7.Text = dr[8].ToString();
                        string img = dr[8].ToString();
                        dr.Close();
                        if (File.Exists(img))
                        {
                            string folder = Path.Combine("staff_image", comboBox1.Text + Path.GetExtension(textBox7.Text));
                            if (!Directory.Exists(textBox7.Text))
                            {
                                Directory.CreateDirectory("staff_image");
                            }
                            File.Copy(textBox7.Text, folder, true);
                            string db = Path.Combine("staff_image", comboBox1.Text + Path.GetExtension(textBox7.Text));

                            string query2 = "update Staff set  Name='" + textBox1.Text + "',DOB='" + dateTimePicker1.Text + "',Age=" + textBox3.Text + ",Email='" + textBox4.Text + "',Phone=" + textBox5.Text + ",Address='" + textBox6.Text + "',Image='" + db + "'WHERE User_id=" + comboBox1.Text + "";
                            if (obj.exec1(query2) > 0)
                            {
                                MessageBox.Show("both are successfully updated");
                            }
                            else
                            {
                                MessageBox.Show("error");
                            }
                        }
                    }
                }
                else if (picture)
                {
                    string query3 = "select * from Staff where User_id=" + comboBox1.Text + "";
                    SqlDataReader dr = obj.ret_dr(query3);
                    if (dr.Read())
                    {
                        string img = dr[8].ToString();
                        dr.Close();
                        if (File.Exists(img))
                        {
                            pictureBox1.Image.Dispose();
                            string folder = Path.Combine("staff_image", comboBox1.Text + Path.GetExtension(textBox7.Text));
                            File.Copy(textBox7.Text, folder, true);
                            string db = Path.Combine("staff_image", comboBox1.Text + Path.GetExtension(textBox7.Text));
                            string query4 = "update Staff set Image='" + db + "' where User_id=" + comboBox1.Text + "";
                            if (obj.exec1(query4) > 0)
                            {
                                MessageBox.Show("image successfully Updated");
                            }
                            else
                            {
                                MessageBox.Show("error");
                            }
                        }
                    }
                }
                else if (text)
                {
                    string query5 = "update Staff set Name='" + textBox1.Text + "',DOB='" + dateTimePicker1.Text + "',Age=" + textBox3.Text + ",Email='" + textBox4.Text + "',Phone=" + textBox5.Text + ",Address='" + textBox6.Text + "' WHERE User_id='" + comboBox1.Text + "'";
                    if (obj.exec1(query5) > 0)
                    {
                        MessageBox.Show("text updated");
                    }
                    else { MessageBox.Show("error"); }
                }
                else
                {
                    MessageBox.Show("failed");
                }
            }

        }

        private bool ValidateInputs()
        {
            string email = textBox4.Text;
            Regex regex = new Regex(@"^([a-zA-Z0-9_\.\-\+])+@(([a-zA-Z0-9\-])+\.)+([a-zA-Z]{2,3})$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                MessageBox.Show("Enter a valid email address");
                return false;
            }
            return true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click_1(object sender, EventArgs e)
        {
            text = true;

        }

        private void textBox3_Click_1(object sender, EventArgs e)
        {
            text = true;
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            text = true;
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            text = true;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            text = true;
        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            text = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string email = textBox4.Text;
            Regex regex = new Regex(@"^([a-zA-Z0-9_\.\-\+])+@(([a-zA-Z0-9\-])+\.)+([a-zA-Z]{2,3})$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                textBox4.ForeColor = Color.Black;
            }
            else
            {
                textBox4.ForeColor = Color.Red;
            }
        }
    }
}
