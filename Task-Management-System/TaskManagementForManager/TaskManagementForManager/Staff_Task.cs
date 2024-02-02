using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace TaskManagementForManager
{
    public partial class Staff_Task : Form
    {
        public Staff_Task()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedDocumentPath = openFileDialog.FileName;
                    textBox2.Text = selectedDocumentPath;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the textbox1 contains a valid integer value
                if (int.TryParse(textBox1.Text, out int taskId))
                {
                    string connectionString = "Data Source=DESKTOP-26A9125\\SQLEXPRESS01;Initial Catalog=Task_Management;Integrated Security=True;";

                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();

                        // Fetch the StaffID from the Tasks table based on TaskID
                        string staffIdQuery = "SELECT StaffID FROM Tasks WHERE TaskID = @taskId";

                        using (SqlCommand staffIdCommand = new SqlCommand(staffIdQuery, sqlConnection))
                        {
                            staffIdCommand.Parameters.AddWithValue("@taskId", taskId);

                            int staffId = Convert.ToInt32(staffIdCommand.ExecuteScalar());

                            // Read the selected file into a byte array
                            byte[] fileBytes = File.ReadAllBytes(textBox2.Text);

                            // Insert a new record into the SubmissionTasks table
                            string insertQuery = "INSERT INTO SubmissionTasks (StaffID, [Files]) VALUES (@staffId, @file)";

                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection))
                            {
                                insertCommand.Parameters.AddWithValue("@staffId", staffId);
                                insertCommand.Parameters.AddWithValue("@file", fileBytes);

                                // Execute the insert query
                                int rowsAffected = insertCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Task inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Failed to insert the task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid TaskID in the textbox.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Refresh the DataGridView or update other UI elements if needed
                viewtask();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
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

                    // Check if the textbox1 contains a valid integer value
                    if (int.TryParse(textBox1.Text, out int taskId))
                    {
                        // Parameterized query to select a specific row based on TaskID
                        string query = "SELECT * FROM Tasks WHERE TaskID = @taskId";

                        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                        {
                            sqlCommand.Parameters.AddWithValue("@taskId", taskId);

                            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                            {
                                DataTable table = new DataTable();
                                sqlDataAdapter.Fill(table);

                                // Check if any rows are returned
                                if (table.Rows.Count > 0)
                                {
                                    dataGridView1.DataSource = table;
                                }
                                else
                                {
                                    MessageBox.Show("No task found with the provided TaskID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid TaskID in the textbox.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
