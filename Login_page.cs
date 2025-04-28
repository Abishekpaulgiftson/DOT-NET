using System.Data.SqlClient;

namespace Shop_management_system
{
    public partial class Login_page : Form
    {
        public Login_page()
        {
            InitializeComponent();
        }
        Data obj = new Data();

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "Admin" && textBox2.Text == "pwd")
            {
                Admin_home_page obj = new Admin_home_page();
                obj.Show();
            }
            else
            {
                string query = "select * from Login where Username='" + textBox1.Text + "'and Password='" + textBox2.Text + "'";
                SqlDataReader dr = obj.ret_dr(query);
                if (dr.Read()) 
                {
                    if (dr[3].ToString() == "Staff")
                    {
                        Staff_billing Staff = new Staff_billing();
                        Staff.Show();
                    }
                }
                else
                {
                    MessageBox.Show("username or password incorrect", "login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
