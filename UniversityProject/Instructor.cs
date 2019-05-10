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

namespace UniversityProject {
    public partial class Instructor : Form {
        String connectionString;
        public Instructor() {
            InitializeComponent();
            //connectionString = "Server=DESKTOP-G5R0M5H\\SQLEXPRESS; Database=UniversityProject; Trusted_Connection=True";
            connectionString = SQLServerConnection.connection;
        }

        private void Instructor_Load(object sender, EventArgs e) {
            initialDisable();
            refreshSearchDataGridView();
            gender_comboBox1.Items.Add("Male");
            gender_comboBox1.Items.Add("Female");
            //dataGridView1.Rows[0].Selected = false;
        }

        private void search_textBox5_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                searchKeyWord();
            }
        }

        private void add_button1_Click(object sender, EventArgs e) {
            addEnable();
            
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spGetAllDepartment", myConnection);

            // 2. Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            gender_comboBox1.SelectedIndex = -1;

            department_comboBox6.DisplayMember = "DeptName";
            department_comboBox6.ValueMember = "DeptName";
            department_comboBox6.DataSource = dataTable;
            department_comboBox6.SelectedIndex = -1;

            myConnection.Close();
        }

        private void edit_button5_Click(object sender, EventArgs e) {
            try {
                instructorID_textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
                firstName_textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
                lastName_textBox4.Text = dataGridView1.SelectedCells[2].Value.ToString();
                email_textBox3.Text = dataGridView1.SelectedCells[3].Value.ToString();
                if (dataGridView1.SelectedCells[4].Value.ToString() == "Male") {
                    gender_comboBox1.SelectedItem = "Male";
                } else {
                    gender_comboBox1.SelectedItem = "Female";
                }

                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spGetAllDepartment", myConnection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                department_comboBox6.DisplayMember = "DeptName";
                department_comboBox6.ValueMember = "DeptName";
                department_comboBox6.DataSource = dataTable;
                department_comboBox6.SelectedValue = dataGridView1.SelectedCells[5].Value.ToString();

                EditEnable();
                myConnection.Close();
            } catch {
                DialogResult dialog = MessageBox.Show("No Instructor Selected");
            }
        }

        private void save_button3_Click(object sender, EventArgs e) {
            if (checkIfEmpty() && edit_button5.Enabled == false) {
                addInstructor();
            } else if (checkIfEmpty() && add_button1.Enabled == false) {
                editInstructor();
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
            SqlCommand command = new SqlCommand("spAllInstructorSearch", myConnection);

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

        private void addInstructor() {
            Console.WriteLine("Add");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spAddInstructor", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@First", firstName_textBox2.Text));
                command.Parameters.Add(new SqlParameter("@Last", lastName_textBox4.Text));
                command.Parameters.Add(new SqlParameter("@Email", email_textBox3.Text));
                command.Parameters.Add(new SqlParameter("@Gender", gender_comboBox1.Text));
                command.Parameters.Add(new SqlParameter("@Department", department_comboBox6.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();

                
                refreshSearchDataGridView();
           
                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Added Instructor");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Adding Instructor");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }

        private void editInstructor() {
            Console.WriteLine("Edit");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spEditInstructor", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@InstructorID", instructorID_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@First", firstName_textBox2.Text));
                command.Parameters.Add(new SqlParameter("@Last", lastName_textBox4.Text));
                command.Parameters.Add(new SqlParameter("@Email", email_textBox3.Text));
                command.Parameters.Add(new SqlParameter("@Gender", gender_comboBox1.Text));
                command.Parameters.Add(new SqlParameter("@Department", department_comboBox6.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();

              
                refreshSearchDataGridView();
            
                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Edited Instructor");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Editing Instructor");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }

        public Boolean checkIfEmpty() {
            Boolean success = false;
            if (!String.IsNullOrEmpty(firstName_textBox2.Text) 
                && !String.IsNullOrEmpty(lastName_textBox4.Text)
                && !String.IsNullOrEmpty(email_textBox3.Text)
                && !String.IsNullOrEmpty(gender_comboBox1.Text)
                && !String.IsNullOrEmpty(department_comboBox6.Text)){
                success = true;
            }
            return success;
        }

        private void searchKeyWord() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spSearchInstructorSearch", myConnection);

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
            department_comboBox6.Enabled = false;
            gender_comboBox1.Enabled = false;
            save_button3.Enabled = false;
        }

        public void addEnable() {
            firstName_textBox2.Enabled = true;
            lastName_textBox4.Enabled = true;
            email_textBox3.Enabled = true;
            department_comboBox6.Enabled = true;
            gender_comboBox1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            edit_button5.Enabled = false;
            search_textBox5.Enabled = false;
        }


        public void cancelAddEdit() {
            firstName_textBox2.Enabled = false;
            lastName_textBox4.Enabled = false;
            email_textBox3.Enabled = false;
            department_comboBox6.Enabled = false;
            gender_comboBox1.Enabled = false;
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
            department_comboBox6.Enabled = true;
            gender_comboBox1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            add_button1.Enabled = false;
            search_textBox5.Enabled = false;
        }

        public void clearTextBoxes() {
            instructorID_textBox1.Clear();
            firstName_textBox2.Clear();
            lastName_textBox4.Clear();
            email_textBox3.Clear();
            department_comboBox6.SelectedIndex = -1;
            gender_comboBox1.SelectedIndex = -1;
        }
    }
}
