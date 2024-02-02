using System.Data.SqlClient;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;

namespace TaskManagementForManager
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-26A9125\\SQLEXPRESS01;Initial Catalog=Task_Management;Integrated Security=True;");
        SqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Check if TaskID is provided for an update
                if (!string.IsNullOrEmpty(maskedTextBox7.Text))
                {
                    // Update existing task
                    cmd = new SqlCommand("INSERT INTO Tasks( TaskTitle, Description, DueDate, StaffEmail, StaffName, StaffID)VALUES(@taskTitle, @description, @dueDate, @staffEmail, @staffName, @staffID)", con);
                    cmd.Parameters.AddWithValue("@taskID", int.Parse(maskedTextBox7.Text));
                }
                else
                {
                    MessageBox.Show("TaskID is required for an update. Please provide a TaskID.");
                    return;  // Exit the method if TaskID is not provided
                }

                // Common parameters for both assignment and update
                cmd.Parameters.AddWithValue("@taskTitle", maskedTextBox6.Text);
                cmd.Parameters.AddWithValue("@description", textBox1.Text);
                cmd.Parameters.AddWithValue("@dueDate", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@staffEmail", maskedTextBox10.Text);
                cmd.Parameters.AddWithValue("@staffName", maskedTextBox9.Text);

                // Fetching StaffID based on Staff Email from 'loginn' table
                int staffID = GetStaffID(maskedTextBox8.Text);
                cmd.Parameters.AddWithValue("@staffID", staffID);

                // Additional debugging info
                Console.WriteLine(cmd.CommandText); // Output the SQL command to the console
                foreach (SqlParameter param in cmd.Parameters)
                {
                    Console.WriteLine($"{param.ParameterName}: {param.Value}");
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("Task updated successfully.");

                ClearInputs(); // Clear input fields after successful update

                // Optional: Refresh the DataGridView or update other UI elements
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        // Function to get StaffID based on Staff Email from 'loginn' table
        private int GetStaffID(string staffEmail)
        {
            int staffID = int.Parse(maskedTextBox8.Text);

            string query = "SELECT User_ID FROM loginn WHERE Email = @staffEmail";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@staffEmail", staffEmail);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    staffID = reader.GetInt32(0);
                }

                reader.Close();
            }

            return staffID;
        }

        private void ClearInputs()
        {
            // Clear input fields after task update
            maskedTextBox7.Clear();
            maskedTextBox6.Clear();
            textBox1.Clear();
            maskedTextBox10.Clear();
            maskedTextBox9.Clear();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void view_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            viewtask();
        }
        private void viewtask()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-26A9125\\SQLEXPRESS01;Initial Catalog=Task_Management;Integrated Security=True;";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Tasks", sqlConnection))
                    {
                        DataTable table = new DataTable();
                        sqlDataAdapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            submittedtasks();
        }
        private void submittedtasks()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-26A9125\\SQLEXPRESS01;Initial Catalog=Task_Management;Integrated Security=True;";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM SubmissionTasks", sqlConnection))
                    {
                        DataTable table = new DataTable();
                        sqlDataAdapter.Fill(table);
                        dataGridView2.DataSource = table;
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
