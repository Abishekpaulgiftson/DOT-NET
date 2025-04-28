using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using System.Net.Mail;
using System.IO;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using Org.BouncyCastle.Crypto;


namespace Shop_management_system
{
    public partial class Staff_billing : Form
    {
        public Staff_billing()
        {
            InitializeComponent();
            grid1();
            combo();
            grid2();
            sales_id();
        }
        public Staff_billing(string id)
        {
            InitializeComponent();
            grid1();
            combo();
            grid2();
            label14.Text = id;
        }

        Data obj = new Data();
        private List<string> _billingInfo = new List<string>();
        private List<string> _billingamount = new List<string>();
        public void grid1()
        {
            string query = "select * from Product where No_of_unit > 0";
            DataSet ds = obj.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        public void grid2()
        {
            string register = Sales_id1();
            string query = "select * from Sales where Sales_id='" + register + "'and Quantity > 0";
            DataSet ds = obj.ret_ds(query);
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
        }
        private void LoadDataGridView2()
        {
            string query = "SELECT * FROM Sales WHERE Sales_id = '" + label14.Text + "'";
            DataSet ds = obj.ret_ds(query);
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
        }
        public void combo()
        {
            string query = "select * from Product where No_of_unit > 0";
            SqlDataReader dr = obj.ret_dr(query);
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }
        string salesidy = "";
        public string Sales_id1()
        {
            string salesidy = string.Empty;
            string query = "select isnull (max(Sales_id),500)+1 as newSales_id from Sales";
            SqlDataReader dr = obj.ret_dr(query);
            if (dr.Read())
            {
                salesidy = dr[0].ToString();
            }
            dr.Close();
            return salesidy;
        }

        public void sales_id()
        {

            string num = Sales_id1();
            label14.Text = num.ToString();
            label14.Text = Sales_id1();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            comboBox2.Items.Clear();
            string query = "select Name,Price,No_of_unit from Product where Product_id='" + comboBox1.Text + "'";
            SqlDataReader dr = obj.ret_dr(query);
            if (dr.Read())
            {
                label12.Text = dr[0].ToString();
                label15.Text = dr[1].ToString();
                int num = Convert.ToInt16(dr[2].ToString());
                for (int i = 1; i <= num; i++)
                {
                    comboBox2.Items.Add(i);
                }
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int num4 = 0;
            string query5 = "select Price from Product where Product_id=" + comboBox1.Text + "";
            SqlDataReader dr = obj.ret_dr(query5);
            if (dr.Read())
            {
                label11.Text = 0.ToString();

                int num = Convert.ToInt32(dr[0].ToString());
                num4 = num * Convert.ToInt32(comboBox2.Text);
            }
            label11.Text = num4.ToString();
        }
        public int totalamount()
        {
            int num = 0;
            string query = "select Amount from Sales where Sales_id=" + label14.Text + " and Product_id=" + comboBox1.Text + "";
            SqlDataReader dr = obj.ret_dr(query);
            if (dr.Read())
            {
                int num1 = Convert.ToInt32(dr[0].ToString());
                num = num1 - Convert.ToInt32(label11.Text);
            }
            return num;
        }
        public int quantity()
        {
            int num2 = 0;
            string query3 = "select No_of_unit from Product where Product_id=" + comboBox1.Text + "";
            SqlDataReader dr = obj.ret_dr(query3);
            if (dr.Read())
            {
                int num = Convert.ToInt16(dr[0].ToString());
                num2 = num - Convert.ToInt16(comboBox2.Text);
            }
            return num2;
        }
        public int quantityadd()
        {
            int num2 = 0;
            string query = "select No_of_unit from Product where Product_id=" + comboBox1.Text + "";
            SqlDataReader dr = obj.ret_dr(query);
            if (dr.Read())
            {
                int num = Convert.ToInt16(dr[0].ToString());
                num2 = num + Convert.ToInt16(comboBox2.Text);
            }
            return num2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Please select a Product ID and Quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int num3 = quantity();
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "insert into Sales(Sales_id,Product_id,Name,Price,Quantity,Date_time,Amount) values (" + label14.Text + "," + comboBox1.Text + ",'" + label12.Text + "'," + label15.Text + "," + comboBox2.Text + ",'" + date + "'," + label11.Text + ")";
            if (obj.exec1(query) > 0)
            {
                string query2 = "select * from Sales where Sales_id='" + label14.Text + "'";
                DataSet ds = obj.ret_ds(query2);
                dataGridView2.DataSource = ds.Tables[0].DefaultView;
                string query6 = "select SUM(Amount) from Sales where Sales_id='" + label14.Text + "'";
                SqlDataReader dr = obj.ret_dr(query6);
                if (dr.Read())
                {
                    label17.Text = dr[0].ToString();
                }

            }
            string query4 = "update Product set No_of_unit=" + num3 + " where Product_id=" + comboBox1.Text + "";
            if (obj.exec1(query4) > 0)
            {
                MessageBox.Show("confirmation", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            grid1();
            comboBox1.SelectedIndex = -1; // Deselects any selected item
            comboBox1.Text = "";
            label12.Text = "";
            label15.Text = "";
            comboBox2.SelectedIndex = -1; // Deselects any selected item
            comboBox2.Text = "";
            label11.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Please select a Product ID and Quantity.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string fetchQuery = "SELECT Quantity FROM Sales WHERE Sales_id = '" + label14.Text + "' AND Product_id = '" + comboBox1.Text + "'";
            SqlDataReader dr = obj.ret_dr(fetchQuery);
            int currentQuantity = 0;

            if (dr.Read())
            {
                currentQuantity = Convert.ToInt32(dr["Quantity"]);
            }
            dr.Close();

            if (currentQuantity == 0)
            {
                MessageBox.Show("No matching record found to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int removeQuantity = Convert.ToInt32(comboBox2.Text);

            if (removeQuantity > currentQuantity)
            {
                MessageBox.Show("The quantity to remove exceeds the current quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int updatedQuantity = currentQuantity - removeQuantity;
            int num3 = quantityadd(); // Adjust product stock
            int newAmount = totalamount(); // Calculate new amount

            if (updatedQuantity == 0)
            {
                // If updated quantity is 0, delete the record
                string deleteQuery = "DELETE FROM Sales WHERE Sales_id = '" + label14.Text + "' AND Product_id = '" + comboBox1.Text + "'";
                if (obj.exec1(deleteQuery) > 0)
                {
                    MessageBox.Show("Record removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Update the record with the new quantity and amount
                string updateQuery = "UPDATE Sales SET Quantity = " + updatedQuantity + ", Amount = " + newAmount + " WHERE Sales_id = '" + label14.Text + "' AND Product_id = '" + comboBox1.Text + "'";
                if (obj.exec1(updateQuery) > 0)
                {
                    MessageBox.Show("Quantity updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            // Update Product stock
            string updateProductQuery = "UPDATE Product SET No_of_unit = " + num3 + " WHERE Product_id = '" + comboBox1.Text + "'";
            obj.exec1(updateProductQuery);
            string totalAmountQuery = "SELECT SUM(Amount) FROM Sales WHERE Sales_id = '" + label14.Text + "'";
            SqlDataReader totalAmountReader = obj.ret_dr(totalAmountQuery);
            if (totalAmountReader.Read())
            {
                label17.Text = totalAmountReader[0].ToString();
            }
            totalAmountReader.Close();
            grid1();
            LoadDataGridView2();
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "";
            label12.Text = "";
            label15.Text = "";
            comboBox2.SelectedIndex = -1;
            comboBox2.Text = "";
            label11.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == label17.Text)
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string query = "insert into Payment (Sales_id,Date_time,Total_amount)values(" + label14.Text + ",'" + date + "'," + textBox1.Text + ")";
                if (obj.exec1(query) > 0)
                {
                    MessageBox.Show("payment successfull", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("enter correct amount", "invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1; // Deselects any selected item
            comboBox1.Text = "";
            label12.Text = "";
            label15.Text = "";
            comboBox2.SelectedIndex = -1; // Deselects any selected item
            comboBox2.Text = "";
            label11.Text = "";
        }
        //payment printing steps
        private string _salesid;
        private void FetchDataFromDatabase()
        {
            _billingInfo.Clear();
            _billingamount.Clear();
            _salesid = label14.Text; // Fetch Sales ID from the label
            using (SqlConnection conn = new SqlConnection("server=Abishek;database=Shop management system;uid=sa;password=password123"))
                try
                {
                    // Fetch billing info for the current Sales ID
                    string query = "SELECT Product_id, Name, Price, Quantity, Amount FROM Sales WHERE Sales_id = @SalesId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SalesId", _salesid);
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Collect row details in a formatted string
                                _billingInfo.Add($"{dr["Product_id"]},{dr["Name"]},{dr["Price"]},{dr["Quantity"]},{dr["Amount"]}");
                            }
                        }
                    }

                    // Fetch total amount from Payment table
                    string totalAmountQuery = "SELECT TOP 1 Total_amount FROM Payment WHERE Sales_id = @SalesId ORDER BY Sales_id DESC";
                    using (SqlCommand cmd = new SqlCommand(totalAmountQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@SalesId", _salesid);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                _billingamount.Add($"Total Amount: {dr["Total_amount"]}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
        private string ConvertNumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + ConvertNumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertNumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                string[] unitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                string[] tensMap = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float yPosition = 250; // Initial Y position
            float lineSpacing = 30; // Space between lines
            float tableStartX = 130; // X position of table
            float[] columnWidths = { 50, 100, 130, 60, 80, 100 }; // Widths for columns

            // Header
            e.Graphics.DrawString("PAYMENT BILL", new System.Drawing.Font("Arial", 20, FontStyle.Bold), Brushes.Black, new PointF(300, 100));
            e.Graphics.DrawString($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}", new System.Drawing.Font("Arial", 12, FontStyle.Regular), Brushes.Black, new PointF(500, 200));

            // Sales ID
            e.Graphics.DrawString("Sales ID:", new System.Drawing.Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(tableStartX, 200));
            e.Graphics.DrawString(_salesid, new System.Drawing.Font("Arial", 12), Brushes.Black, new PointF(tableStartX + 100, 200));
            yPosition += lineSpacing * 2;

            // Table Headers
            string[] headers = { "S.No", "Product ID", "Name", "Price", "Quantity", "Amount" };
            float currentX = tableStartX;

            for (int i = 0; i < headers.Length; i++)
            {
                e.Graphics.DrawString(headers[i], new System.Drawing.Font("Arial", 10, FontStyle.Bold), Brushes.Black, new PointF(currentX, yPosition));
                currentX += columnWidths[i];
            }

            yPosition += lineSpacing;
            e.Graphics.DrawLine(Pens.Black, tableStartX, yPosition, tableStartX + columnWidths.Sum(), yPosition);

            // Table Rows
            int serialnumber = 1;
            foreach (var row in _billingInfo)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(row))
                        continue;

                    string[] rowData = row.Split(',');
                    currentX = tableStartX;

                    e.Graphics.DrawString(serialnumber.ToString(), new System.Drawing.Font("Arial", 10), Brushes.Black, new PointF(currentX, yPosition));
                    currentX += columnWidths[0];

                    for (int i = 0; i < Math.Min(rowData.Length, columnWidths.Length - 1); i++)
                    {
                        e.Graphics.DrawString(rowData[i], new System.Drawing.Font("Arial", 10), Brushes.Black, new PointF(currentX, yPosition));
                        currentX += columnWidths[i + 1];
                    }

                    serialnumber++;
                    yPosition += lineSpacing;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing row: {ex.Message}");
                }

            }

            e.Graphics.DrawLine(Pens.Black, tableStartX, yPosition, tableStartX + columnWidths.Sum(), yPosition);
            // Total Amount
            yPosition += lineSpacing;
            string totalAmount = _billingamount.FirstOrDefault() ?? "0";
            string numericAmount = new string(totalAmount.Where(char.IsDigit).ToArray());
            int totalAmountInt = int.TryParse(numericAmount, out int parsedAmount) ? parsedAmount : 0;
            e.Graphics.DrawString(totalAmount, new System.Drawing.Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(tableStartX + 150, yPosition));
            yPosition += lineSpacing;
            string amountInWords = ConvertNumberToWords(totalAmountInt) + " Only";
            e.Graphics.DrawString(amountInWords, new System.Drawing.Font("Arial", 12, FontStyle.Italic), Brushes.Black, new PointF(tableStartX + 150, yPosition));
        }
        private void button5_Click(object sender, EventArgs e)
        {
            FetchDataFromDatabase();
            PrintDialog dlg = new PrintDialog();
            dlg.Document = printDocument1;
                
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {// mail sending

                string salesidy = label14.Text;
               
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("abishekgiftson08@gmail.com");
                mail.To.Add(textBox2.Text);
                mail.Subject = "SHOP MANAGEMENT";
                mail.Body = $"payment pay slip";
                string filepath = $@"C:\Users\Hp\source\repos\Shop management system\Shop management system\bill pdf\{salesidy}.pdf";
                Attachment attachment = new Attachment(filepath);
                mail.Attachments.Add(attachment);

                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("abishekgiftson08@gmail.com", "mctb cjvh noez kuyg");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show("mail sended");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sales_id();
            comboBox1.Text = "";
            label12.Text = "";
            label15.Text = "";
            comboBox2.Text = "";
            label11.Text = "";
            dataGridView2.DataSource = "";
            label17.Text = "";
            textBox1.Text = "";
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {
            string id = Sales_id1();
            MessageBox.Show(id);
            label14.Text = id;
            Staff_billing obj = new Staff_billing(id);
            obj.Show();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
        private void Staff_billing_Load(object sender, EventArgs e)
        {

        }
    }
}
