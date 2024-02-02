// Import necessary namespaces

using System.Data.SqlClient;
using System.Windows.Forms;
using System;

namespace TaskManagementForManager
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-26A9125\\SQLEXPRESS01;Initial Catalog=Task_Management;Integrated Security=True;");
        SqlCommand cmd;

        public Login()
        {
            InitializeComponent();

            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            login_combobox.Items.Add("Manager");
            login_combobox.Items.Add("Staff");
        }

        private void login_password_TextChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = '*';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (login_checkbox.Checked)
            {
                login_password.PasswordChar = '\0';
            }
            else
            {
                login_password.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Assuming you have a table named 'loginn' with columns 'Username', 'Email', 'Password', 'User_Type'
                cmd = new SqlCommand("SELECT * FROM loginn WHERE User_Name = @username AND Password = @password AND User_Type = @userType", con);
                cmd.Parameters.AddWithValue("@username", login_username.Text);
                cmd.Parameters.AddWithValue("@password", login_password.Text);

                string userType = login_combobox.SelectedItem.ToString();
                cmd.Parameters.AddWithValue("@userType", userType);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (userType == "Staff")
                    {
                        // Perform actions for Staff user
                        Staff_Task form1 = new Staff_Task();
                        form1.Show();
                        this.Hide();
                    }
                    else if (userType == "Manager")
                    {
                        // Perform actions for Manager user
                        Form1 form2 = new Form1();
                        form2.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username, password, or user type.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // Close the connection in the finally block to ensure it is closed even if an exception occurs.
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
