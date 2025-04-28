using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_management_system
{
    public partial class Admin_staff_adding : Form
    {
        public Admin_staff_adding()
        {
            InitializeComponent();
            staff_id();
            user_id();
        }
        Data obj = new Data();
        private void Admin_staff_adding_Load(object sender, EventArgs e)
        {

        }
        public void staff_id()
        {
            string query = "select isnull(max(Staff_id),100)+1 as newStaff_id from Staff";
            SqlDataReader dr = obj.ret_dr(query);
            if (dr.Read())
            {
                textBox2.Text = dr[0].ToString();
            }
        }
        public void user_id()
        {
            string query = "select isnull(max(User_id),0)+1 as newUser_id from Login";
            SqlDataReader dr = obj.ret_dr(query);
            if (dr.Read())
            {
                textBox8.Text = dr[0].ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //creating folder for image
            if (!ValidateInputs())
            {
                return;
            }
            try
            {
                string folder = Path.Combine(Application.StartupPath, "staff_image");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string img = Path.Combine(folder, textBox8.Text + Path.GetExtension(textBox1.Text));
                File.Copy(textBox1.Text, img, true);
                string link = Path.Combine("staff_image", textBox8.Text + Path.GetExtension(textBox1.Text));

                //insert phase
                string query2 = "select * from Login where(Username='" + textBox9.Text + "'or Password='" + textBox10.Text + "')";
                SqlDataReader dr = obj.ret_dr(query2);
                if (dr.Read())
                {
                    if (textBox9.Text == dr[1].ToString())
                    {
                        MessageBox.Show("username already exit");
                        return;
                    }
                    if (textBox10.Text == dr[2].ToString())
                    {
                        MessageBox.Show("give strong password");
                        return;
                    }
                }
                string query = "insert into Staff values(" + textBox2.Text + "," + textBox8.Text + ",'" + textBox3.Text + "','" + dateTimePicker1.Text + "'," + textBox5.Text + ",'" + textBox6.Text + "'," + textBox7.Text + ",'" + textBox11.Text + "','" + link + "')";
                if (obj.exec1(query) > 0)
                {
                    string query3 = "insert into Login values(" + textBox8.Text + ",'" + textBox9.Text + "','" + textBox10.Text + "','Staff')";
                    if (obj.exec1(query3) > 0)
                    {
                        MessageBox.Show("Inserted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                // mail sending

               // Random rand = new Random();
                //int rands = (rand.Next(99999));//for random otp
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("abishekgiftson08@gmail.com");
                mail.To.Add(textBox6.Text);
                mail.Subject = "SHOP MANAGEMENT";
                mail.Body = $"NAME:{textBox9.Text}\n\n PASSWORD:{textBox10.Text}";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("abishekgiftson08@gmail.com", "mctb cjvh noez kuyg");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show("mail sended");

                //clearing the fields

                staff_id();
                user_id();
                clearfields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Name is required");
                return false;
            }
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Age is required");
                return false;
            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Email is required");
                return false;
            }
            if (string.IsNullOrEmpty(textBox7.Text) || !Regex.IsMatch(textBox7.Text, @"^\d{10,11}$"))
            {
                MessageBox.Show("Enter a valid Phone number");
                return false;
            }
            if (string.IsNullOrEmpty(textBox11.Text))
            {
                MessageBox.Show("Address is required");
                return false;
            }
            if (string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox10.Text))
            {
                MessageBox.Show("Username and Password are required");
                return false;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Image path is required");
                return false;
            }
            string email = textBox6.Text;
            Regex regex = new Regex(@"^([a-zA-Z0-9_\.\-\+])+@(([a-zA-Z0-9\-])+\.)+([a-zA-Z]{2,3})$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                MessageBox.Show("Enter a valid email address");
                return false;
            }
            return true;

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string email = textBox6.Text;
            Regex regex = new Regex(@"^([a-zA-Z0-9_\.\-\+])+@(([a-zA-Z0-9\-])+\.)+([a-zA-Z]{2,3})$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                textBox6.ForeColor = Color.Black;
            }
            else
            {
                textBox6.ForeColor = Color.Red;
            }
        }
        private bool isClearingFields = false;
        private void clearfields()
        {
            isClearingFields = true;
            textBox1.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            pictureBox1.Image = null;
            dateTimePicker1.Value = DateTime.Now;
            isClearingFields = false;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (isClearingFields) return; // Skip validation if clearing fields
            DateTime dateTime = dateTimePicker1.Value;
            int age = DateTime.Now.Year - dateTime.Year;
            if (age < 18)
            {
                MessageBox.Show("Age is below criteria", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                textBox5.Text = age.ToString();
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            open.Title = "Select an Image File";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                textBox1.Text = open.FileName;
            }
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
