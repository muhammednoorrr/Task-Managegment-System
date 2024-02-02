using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskManagementForManager
{
    public partial class Register_Staff : Form
    {
        public Register_Staff()
        {
            InitializeComponent();

            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            comboBox1.Items.Add("Male");
            comboBox1.Items.Add("Female");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '*';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.PasswordChar = '\0';
            }
            else
            {
                textBox4.PasswordChar = '*';
            }
        }

        // Your connection string (replace with your actual connection string)
        public string conString = "Data Source=DESKTOP-26A9125\\SQLEXPRESS01;Initial Catalog=Task_Management;Integrated Security=True;";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    // Check if the email already exists
                    using (SqlCommand checkEmailCmd = new SqlCommand("SELECT COUNT(*) FROM loginn WHERE Email = @Email", con))
                    {
                        checkEmailCmd.Parameters.AddWithValue("@Email", textBox1.Text);

                        int emailCount = (int)checkEmailCmd.ExecuteScalar();

                        if (emailCount > 0)
                        {
                            MessageBox.Show("Email already exists. Please use a different email.");
                            return;
                        }
                    }

                    // Insert new staff member
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO loginn (User_Name, Email, Password, User_Type, User_ID) VALUES (@User_Name, @Email, @Password, @User_Type, @User_ID)", con))
                    {
                        cmd.Parameters.AddWithValue("@User_Name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Password", textBox4.Text);
                        cmd.Parameters.AddWithValue("@User_Type", "staff");
                        cmd.Parameters.AddWithValue("@User_ID", textBox3.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Staff member added successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add staff member.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
