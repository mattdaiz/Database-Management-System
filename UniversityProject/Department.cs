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
    public partial class Department : Form {
        String connectionString;
        String instructorID;
        public Department() {
            InitializeComponent();
            //connectionString = "Server=DESKTOP-G5R0M5H\\SQLEXPRESS; Database=UniversityProject; Trusted_Connection=True";
            connectionString = SQLServerConnection.connection;
        }

        private void Department_Load(object sender, EventArgs e) {
            initialDisable();
            refreshSearchDataGridView();
            //dataGridView1.Rows[0].Selected = false;
        }

        private void add_button1_Click(object sender, EventArgs e) {
            addEnable();
        }

        private void edit_button5_Click(object sender, EventArgs e) {
            try {
                department_textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
                building_textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
                //head_textBox1.Text = dataGridView1.SelectedCells[2].Value.ToString();

                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spGetDepartmentHeadID", myConnection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DeptName", department_textBox1.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                DataRow row = dataTable.Rows[0];
                instructorID = row["InstructorID"].ToString();

                instructorID_textBox2.Text = row["InstructorID"] + " - " + dataGridView1.SelectedCells[2].Value.ToString();

                EditEnable();
                myConnection.Close();
            } catch {
                DialogResult dialog = MessageBox.Show("No Department Selected");
            }
        }
            
        private void save_button3_Click(object sender, EventArgs e) {
            if (checkIfEmpty() && edit_button5.Enabled == false && !String.IsNullOrEmpty(instructorID_textBox2.Text)) {
                addDepartment();
            } else if (checkIfEmpty() && edit_button5.Enabled == false && String.IsNullOrEmpty(instructorID_textBox2.Text)) {
                addDepartmentNoHead();
            } else if (checkIfEmpty() && add_button1.Enabled == false && !String.IsNullOrEmpty(instructorID_textBox2.Text)) {
                editDepartment();
            } else if (checkIfEmpty() && add_button1.Enabled == false && String.IsNullOrEmpty(instructorID_textBox2.Text)) {
                editDepartmentNoHead();
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

        private void search_textBox5_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                searchKeyWord();
            }
        }


        private void choose_button1_Click(object sender, EventArgs e) {
            using (var form = new InstructorSearch()) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    instructorID = form.ReturnValueID;
                    String instructorFirst = form.ReturnValueFirst;
                    String instructorLast = form.ReturnValueLast;
                    instructorID_textBox2.Text = instructorID + " - " + instructorFirst + " " + instructorLast;
                }
            }
        }


        private void refreshSearchDataGridView() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            //1.Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spAllDepartmentSearch", myConnection);

            //2.Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            myConnection.Close();
        }

        private void addDepartment() {
            Console.WriteLine("Add");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spAddDepartment", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@DeptName", department_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@Building", building_textBox2.Text));
                command.Parameters.Add(new SqlParameter("@InstructorID", instructorID_textBox2.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();


                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Added Department");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Adding Department");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }

        private void addDepartmentNoHead() {
            Console.WriteLine("Add");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spAddDepartmentNoHead", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@DeptName", department_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@Building", building_textBox2.Text));              

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();


                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Added Department");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Adding Department");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }

        private void editDepartment() {
            Console.WriteLine("Edit");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spEditDepartment", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@DeptName", department_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@Building", building_textBox2.Text));
                command.Parameters.Add(new SqlParameter("@InstructorID", instructorID));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();


                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Edited Department");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Editing Department");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }

        private void editDepartmentNoHead() {
            Console.WriteLine("Edit");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spEditDepartmentNoHead", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@DeptName", department_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@Building", building_textBox2.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();

                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Edited Department");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Editing Department");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }

        private void searchKeyWord() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spSearchDepartmentSearch", myConnection);

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





        public Boolean checkIfEmpty() {
            Boolean success = false;
            if (!String.IsNullOrEmpty(department_textBox1.Text)
                && !String.IsNullOrEmpty(building_textBox2.Text)) {
                success = true;
            }
            return success;
        }

        private void initialDisable() {
            department_textBox1.Enabled = false;
            building_textBox2.Enabled = false;
            instructorID_textBox2.Enabled = false;
            choose_button1.Enabled = false;
            save_button3.Enabled = false;
        }

        public void addEnable() {
            department_textBox1.Enabled = true;
            building_textBox2.Enabled = true;
            choose_button1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            edit_button5.Enabled = false;
            search_textBox5.Enabled = false;
        }

        public void EditEnable() {
            department_textBox1.Enabled = true;
            building_textBox2.Enabled = true;
            choose_button1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            add_button1.Enabled = false;
            search_textBox5.Enabled = false;
        }

        public void cancelAddEdit() {
            department_textBox1.Enabled = false;
            building_textBox2.Enabled = false;
            choose_button1.Enabled = false;
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

        public void clearTextBoxes() {
            department_textBox1.Clear();
            building_textBox2.Clear();
            instructorID_textBox2.Clear();
        }
    }
}
