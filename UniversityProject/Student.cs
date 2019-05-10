using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UniversityProject
{
    public partial class Student : Form
    {
        String connectionString;
        public Student()
        {
            InitializeComponent();
            connectionString = SQLServerConnection.connection;
        }

        private void StudentCreate_Load(object sender, EventArgs e) {
            refreshSearchDataGridView();
            //dataGridView1.Rows[0].Selected = false;
            //gender_comboBox1.Items.Add("");
            gender_comboBox1.Items.Add("Male");
            gender_comboBox1.Items.Add("Female");
            //gender_comboBox1.Text = "Select Gender";
            initialDisable();
            //gender_comboBox1.BackColor = Color.White;

        }

        private void search_textBox5_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                searchKeyWord();
            }
        }

        private void add_button1_Click_1(object sender, EventArgs e) {
            addEnable();
        }

        private void edit_button5_Click(object sender, EventArgs e) {
            try {
                studentID_textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
                firstName_textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
                lastName_textBox4.Text = dataGridView1.SelectedCells[2].Value.ToString();
                email_textBox3.Text = dataGridView1.SelectedCells[3].Value.ToString();
                if (dataGridView1.SelectedCells[4].Value.ToString() == "Male") {
                    gender_comboBox1.SelectedItem = "Male";
                } else {
                    gender_comboBox1.SelectedItem = "Female";
                }
                totalCredits_textBox6.Text = dataGridView1.SelectedCells[5].Value.ToString();
                EditEnable();
            } catch {
                DialogResult dialog = MessageBox.Show("No Student Selected");
            }
        }

        private void save_button3_Click(object sender, EventArgs e) { 
            if (checkIfEmpty() && edit_button5.Enabled == false) {
                addStudent();
            } else if (checkIfEmpty() && add_button1.Enabled == false) {
                editStudent();
            } else {
                DialogResult dialog = MessageBox.Show("Missing Fields");
            }
        }

        private void cancel_button4_Click(object sender, EventArgs e) {
            cancelAddEdit();
        }

        private void quit_button6_Click(object sender, EventArgs e) {
            this.Close();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
        }

        private void refreshSearchDataGridView() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spAllStudentSearch", myConnection);

            // 2. Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            //dataGridView1.Rows[0].Selected = false;
            searchKeyWord();
            myConnection.Close();
        }

        private void addStudent() {
            Console.WriteLine("Add");
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction
                transaction = connection.BeginTransaction("AddTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    String t1 = null;
                    if (!(string.IsNullOrEmpty(firstName_textBox2.Text))) {
                        t1 = firstName_textBox2.Text;
                    }

                    String t2 = null; ;
                    if (!(string.IsNullOrEmpty(lastName_textBox4.Text))) {
                        t2 = lastName_textBox4.Text;
                    }

                    String t3 = null; ;
                    if (!(string.IsNullOrEmpty(email_textBox3.Text))) {
                        t3 = email_textBox3.Text;
                    }

                    String t4 = null; ;
                    if (!(string.IsNullOrEmpty(gender_comboBox1.Text))) {
                        t4 = gender_comboBox1.Text;
                    }

                    float t5 = Convert.ToSingle(totalCredits_textBox6.Text);

                    command.CommandText =
                        "Insert into vStudentSearch (FirstName, LastName, Email, Gender, TotalCredits) VALUES (@firstName, @lastName, @email, @gender, @totalCredits)";
                    /*command.Parameters.AddWithValue("@firstName", firstName_textBox2.Text);
                    command.Parameters.AddWithValue("@lastName", lastName_textBox4.Text);
                    command.Parameters.AddWithValue("@email", email_textBox3.Text);
                    command.Parameters.AddWithValue("@gender", gender_comboBox1.Text);
                    command.Parameters.AddWithValue("@totalCredits", Convert.ToSingle(totalCredits_textBox6.Text)); */

                    command.Parameters.AddWithValue("@firstName", t1);
                    command.Parameters.AddWithValue("@lastName", t2);
                    command.Parameters.AddWithValue("@email", t3);
                    command.Parameters.AddWithValue("@gender", t4);
                    command.Parameters.AddWithValue("@totalCredits", t5);
                    command.ExecuteNonQuery();
                   
                    // Attempt to commit the transaction.
                    transaction.Commit();
                    refreshSearchDataGridView();
                    clearTextBoxes();
                    DialogResult dialog = MessageBox.Show("Successfully Added Student");
                    cancelAddEdit();
                    Console.WriteLine("Both records are written to database.");
                } catch (Exception ex) {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try {
                        transaction.Rollback();
                        DialogResult dialog = MessageBox.Show("Error Adding Student");
                    } catch (Exception ex2) {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }
            }
        }

        private void editStudent() {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction
                transaction = connection.BeginTransaction("EditTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try {
                    int studentID = Convert.ToInt32(studentID_textBox1.Text);

                    String t1 = null;
                    if (!(string.IsNullOrEmpty(firstName_textBox2.Text))) {
                        t1 = firstName_textBox2.Text;
                    }

                    String t2 = null; ;
                    if (!(string.IsNullOrEmpty(lastName_textBox4.Text))) {
                        t2 = lastName_textBox4.Text;
                    }

                    String t3 = null; ;
                    if (!(string.IsNullOrEmpty(email_textBox3.Text))) {
                        t3 = email_textBox3.Text;
                    }

                    String t4 = null; ;
                    if (!(string.IsNullOrEmpty(gender_comboBox1.Text))) {
                        t4 = gender_comboBox1.Text;
                    }

                    float t5 = Convert.ToSingle(totalCredits_textBox6.Text);

                    command.CommandText =
                        "Update vStudentSearch " +
                        "SET FirstName = @firstName, LastName = @lastName , Email = @email, Gender = @gender, TotalCredits = @totalCredits " +
                        "WHERE StudentID = @studentID";
                    /*command.Parameters.AddWithValue("@firstName", firstName_textBox2.Text);
                    command.Parameters.AddWithValue("@lastName", lastName_textBox4.Text);
                    command.Parameters.AddWithValue("@email", email_textBox3.Text);
                    command.Parameters.AddWithValue("@gender", gender_comboBox1.Text);
                    command.Parameters.AddWithValue("@totalCredits", Convert.ToSingle(totalCredits_textBox6.Text)); */
                    command.Parameters.AddWithValue("@studentID", studentID);
                    command.Parameters.AddWithValue("@firstName", t1);
                    command.Parameters.AddWithValue("@lastName", t2);
                    command.Parameters.AddWithValue("@email", t3);
                    command.Parameters.AddWithValue("@gender", t4);
                    command.Parameters.AddWithValue("@totalCredits", t5);
                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    refreshSearchDataGridView();
                    clearTextBoxes();
                    DialogResult dialog = MessageBox.Show("Successfully Updated Student");
                    cancelAddEdit();
                    Console.WriteLine("Both records are written to database.");
                } catch (Exception ex) {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try {
                        transaction.Rollback();
                        DialogResult dialog = MessageBox.Show("Error Updating Student");
                    } catch (Exception ex2) {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }
            }
        }

        public Boolean checkIfEmpty() {
            Boolean success = false;
            if (!String.IsNullOrEmpty(firstName_textBox2.Text)
                && !String.IsNullOrEmpty(lastName_textBox4.Text)
                && !String.IsNullOrEmpty(email_textBox3.Text)
                && !String.IsNullOrEmpty(gender_comboBox1.Text)
                && !String.IsNullOrEmpty(totalCredits_textBox6.Text)) {
                success = true;
            }
            return success;
        }

        private void searchKeyWord() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spSearchStudentSearch", myConnection);

            // 2. Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. Add parameters to command, which will be stored to the stored procedure
            command.Parameters.Add(new SqlParameter("@keyword", search_textBox5.Text));

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            myConnection.Close();
        }


        private void initialDisable() {
            firstName_textBox2.Enabled = false;
            lastName_textBox4.Enabled = false;
            email_textBox3.Enabled = false;
            gender_comboBox1.Enabled = false;
            totalCredits_textBox6.Enabled = false;
            save_button3.Enabled = false;
        }

        public void addEnable() {
            firstName_textBox2.Enabled = true;
            lastName_textBox4.Enabled = true;
            email_textBox3.Enabled = true;
            gender_comboBox1.Enabled = true;
            totalCredits_textBox6.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            edit_button5.Enabled = false;
            search_textBox5.Enabled = false;
        }

        public void cancelAddEdit() {
            firstName_textBox2.Enabled = false;
            lastName_textBox4.Enabled = false;
            email_textBox3.Enabled = false;
            gender_comboBox1.Enabled = false;
            totalCredits_textBox6.Enabled = false;
            save_button3.Enabled = false;

            clearTextBoxes();
            dataGridView1.Enabled = true;

            if (add_button1.Enabled == false) {
                add_button1.Enabled = true;
            } else {
                edit_button5.Enabled = true;
            }
            search_textBox5.Enabled = true;
        }

        public void EditEnable() {
            firstName_textBox2.Enabled = true;
            lastName_textBox4.Enabled = true;
            email_textBox3.Enabled = true;
            gender_comboBox1.Enabled = true;
            totalCredits_textBox6.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            add_button1.Enabled = false;
            search_textBox5.Enabled = false;
        }

        public void clearTextBoxes() {
            studentID_textBox1.Clear();
            firstName_textBox2.Clear();
            lastName_textBox4.Clear();
            email_textBox3.Clear();
            gender_comboBox1.SelectedIndex = -1;
            totalCredits_textBox6.Clear();
        }

       
    }
}
