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
using static UniversityProject.SQLServerConnection;

namespace UniversityProject {
    public partial class Course : Form {
        String connectionString;
        public Course() {
            InitializeComponent();
            //connectionString = "Server=DESKTOP-G5R0M5H\\SQLEXPRESS; Database=UniversityProject; Trusted_Connection=True";
            connectionString = SQLServerConnection.connection;
        }

        private void Course_Load(object sender, EventArgs e) {
            initialDisable();
            refreshSearchDataGridView();
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


            department_comboBox6.DisplayMember = "DeptName";
            department_comboBox6.ValueMember = "DeptName";
            department_comboBox6.DataSource = dataTable;
            department_comboBox6.SelectedIndex = -1;
        }

        private void edit_button5_Click(object sender, EventArgs e) {
            try {
                courseID_textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
                courseName_textBox1.Text = dataGridView1.SelectedCells[2].Value.ToString();
                credits_textBox1.Text = dataGridView1.SelectedCells[3].Value.ToString();

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
                department_comboBox6.SelectedValue = dataGridView1.SelectedCells[1].Value.ToString();

                EditEnable();
                myConnection.Close();
            } catch {
                DialogResult dialog = MessageBox.Show("No Department Selected");
            }
        }


        private void save_button3_Click(object sender, EventArgs e) {
            if (checkIfEmpty() && edit_button5.Enabled == false) {
                addCourse();
            } else if (checkIfEmpty() && add_button1.Enabled == false) {
                editCourse();
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

        private void addCourse() {
            Console.WriteLine("Add");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spAddCourse", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@CourseID", courseID_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@DeptName", department_comboBox6.Text));
                command.Parameters.Add(new SqlParameter("@CourseName", courseName_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@Credit",  credits_textBox1.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();


                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Added Course");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Adding Course");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }



        private void editCourse() {
            Console.WriteLine("Edit");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spEditCourse", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@CourseID", courseID_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@DeptName", department_comboBox6.Text));
                command.Parameters.Add(new SqlParameter("@CourseName", courseName_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@Credits", credits_textBox1.Text));
               
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();


                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Edited Course");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Editing Course");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }













        private void searchKeyWord() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spSearchCourseSearch", myConnection);

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


        private void refreshSearchDataGridView() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            //1.Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spAllCourseSearch", myConnection);

            //2.Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            myConnection.Close();
        }

        public Boolean checkIfEmpty() {
            Boolean success = false;
            if (!String.IsNullOrEmpty(courseID_textBox1.Text)
                && !String.IsNullOrEmpty(department_comboBox6.Text)
                && !String.IsNullOrEmpty(courseName_textBox1.Text)
                && !String.IsNullOrEmpty(credits_textBox1.Text)) {
                success = true;
            }
            return success;
        }

        private void initialDisable() {
            courseID_textBox1.Enabled = false;
            department_comboBox6.Enabled = false;
            courseName_textBox1.Enabled = false;
            credits_textBox1.Enabled = false;
            save_button3.Enabled = false;
        }

        public void addEnable() {
            courseID_textBox1.Enabled = true;
            department_comboBox6.Enabled = true;
            courseName_textBox1.Enabled = true;
            credits_textBox1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            edit_button5.Enabled = false;
            search_textBox5.Enabled = false;
        }

        public void EditEnable() {
            //courseID_textBox1.Enabled = true;
            department_comboBox6.Enabled = true;
            courseName_textBox1.Enabled = true;
            credits_textBox1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            add_button1.Enabled = false;
            search_textBox5.Enabled = false;
        }

        public void cancelAddEdit() {
            courseID_textBox1.Enabled = false;
            department_comboBox6.Enabled = false;
            courseName_textBox1.Enabled = false;
            credits_textBox1.Enabled = false;
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
            courseID_textBox1.Clear();
            department_comboBox6.SelectedIndex = -1;
            courseName_textBox1.Clear();
            credits_textBox1.Clear();
        }

       
    }
}
