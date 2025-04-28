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
    public partial class Admin_sales_view : Form
    {
        public Admin_sales_view()
        {
            InitializeComponent();
            grid();
            combo();
            combo2();
        }
        Data obj = new Data();

        SqlConnection con = new SqlConnection("server=Abishek;database=Shop management system;uid=sa;password=password123");
        public void grid()
        {
            string query = "select * from Sales ORDER BY Sales_id asc";
            DataSet ds = obj.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        public void combo()
        {
            // Updated query to fix the SQL error
            string query = "SELECT DISTINCT CONVERT(DATE, Date_time) AS DateOnly FROM Sales ORDER BY CONVERT(DATE, Date_time) ASC";

            // Execute the query
            SqlDataReader dr = obj.ret_dr(query);
            while (dr.Read())
            {
                // Add each date to the ComboBox
                comboBox1.Items.Add(dr["DateOnly"].ToString());
            }
        }


        public void combo2()
        {
            string query = "select DISTINCT Name from Sales";
            SqlDataReader dr = obj.ret_dr(query);
            while (dr.Read())
            {
                comboBox3.Items.Add(dr[0].ToString());
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Validate the input
                if (DateTime.TryParse(comboBox1.Text, out DateTime selectedDate))
                {
                    // Define the query
                    string query = "SELECT * FROM Sales WHERE CAST(Date_time AS DATE) = @Date_time";

                    // Create the command with a parameter
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Pass the selected date as a DateTime parameter
                        cmd.Parameters.Add("@Date_time", SqlDbType.Date).Value = selectedDate;

                        // Fetch data using SqlDataAdapter
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            // Bind the data to the DataGridView
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                            }
                            else
                            {
                                MessageBox.Show("No records found for the selected date.");
                                dataGridView1.DataSource = null;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid date format. Please select a valid date.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }



        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select Name,SUM(quantity) as items from Sales where Name='" + comboBox3.Text + "'GROUP BY Name";
            DataSet ds = obj.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the input for both ComboBoxes
                if (DateTime.TryParse(comboBox1.Text, out DateTime selectedDate) && !string.IsNullOrWhiteSpace(comboBox3.Text))
                {
                    // Use parameterized query to prevent SQL injection and conversion issues
                    string query = "SELECT * FROM Sales WHERE CAST(Date_time AS DATE) = @Date_time AND Name = @Name";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Pass the parameters
                        cmd.Parameters.AddWithValue("@Date_time", selectedDate);
                        cmd.Parameters.AddWithValue("@Name", comboBox3.Text);

                        // Execute the query and fill the DataGridView
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            dataGridView1.DataSource = ds.Tables[0].DefaultView;
                        }
                    }

                    // Calculate the sum of Amount for the selected date and name
                    string query1 = "SELECT SUM(Amount) FROM Sales WHERE CAST(Date_time AS DATE) = @Date_time AND Name = @Name";

                    using (SqlCommand cmd1 = new SqlCommand(query1, con))
                    {
                        cmd1.Parameters.AddWithValue("@Date_time", selectedDate);
                        cmd1.Parameters.AddWithValue("@Name", comboBox3.Text);

                        con.Open();
                        object result = cmd1.ExecuteScalar();
                        con.Close();

                        label2.Text = result != DBNull.Value ? result.ToString() : "0";
                    }
                    string query2 = "SELECT SUM(Quantity) FROM Sales WHERE CAST(Date_time AS DATE) = @Date_time AND Name = @Name";

                    using (SqlCommand cmd1 = new SqlCommand(query2, con))
                    {
                        cmd1.Parameters.AddWithValue("@Date_time", selectedDate);
                        cmd1.Parameters.AddWithValue("@Name", comboBox3.Text);

                        con.Open();
                        object result = cmd1.ExecuteScalar();
                        con.Close();

                        label7.Text = result != DBNull.Value ? result.ToString() : "0";
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid date and name.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
