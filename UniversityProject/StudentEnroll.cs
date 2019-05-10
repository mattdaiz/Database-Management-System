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
    public partial class StudentEnroll : Form {
        String connectionString;
        public StudentEnroll() {
            InitializeComponent();
            //connectionString = "Server=DESKTOP-G5R0M5H\\SQLEXPRESS; Database=UniversityProject; Trusted_Connection=True";
            connectionString = SQLServerConnection.connection;
        }

        private void StudentEnroll_Load(object sender, EventArgs e) {
            initialDisable();
            refreshSearchDataGridView();
        }

        private void chooseStudent_button1_Click(object sender, EventArgs e) {
            using (var form = new StudentSearch()) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    String studentID = form.ReturnValueID;
                    String studentFirst = form.ReturnValueFirst;
                    String studentLast = form.ReturnValueLast;
                    studentID_textBox2.Text = studentID;
                    studentName_textBox2.Text = studentFirst + " " + studentLast;
                }
            }
        }

        private void chooseCourse_button1_Click(object sender, EventArgs e) {
            using (var form = new CourseSearch()) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    String sectionID = form.ReturnValueSectionID;
                    String courseID = form.ReturnValueCourseID;
                    String courseName = form.ReturnValueCourseName;
                    String instructor = form.ReturnValueInstructor;
                    String semester = form.ReturnValueSemester;
                    String year = form.ReturnValueYear;

                    section_textBox3.Text = sectionID;
                    courseID_textBox1.Text = courseID;
                    courseName_textBox4.Text = courseName;
                    instructor_textBox1.Text = instructor;
                    semester_textBox5.Text = semester;
                    year_textBox6.Text = year;
                }
            }
        }

        private void search_textBox5_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                searchKeyWord();
            }
        }


        private void edit_button5_Click(object sender, EventArgs e) {
            try {
                studentID_textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
                studentName_textBox2.Text = dataGridView1.SelectedCells[2].Value.ToString();

                section_textBox3.Text = dataGridView1.SelectedCells[3].Value.ToString();
                courseID_textBox1.Text = dataGridView1.SelectedCells[4].Value.ToString();
                courseName_textBox4.Text = dataGridView1.SelectedCells[5].Value.ToString();
                instructor_textBox1.Text = dataGridView1.SelectedCells[6].Value.ToString();
                semester_textBox5.Text = dataGridView1.SelectedCells[7].Value.ToString();
                year_textBox6.Text = dataGridView1.SelectedCells[8].Value.ToString();

                EditEnable();
            } catch {
                DialogResult dialog = MessageBox.Show("No Instructor Selected");
            }
        }

        private void add_button1_Click(object sender, EventArgs e) {
            addEnable();
        }

        private void save_button3_Click(object sender, EventArgs e) {
            if (checkIfEmpty() && edit_button5.Enabled == false) {
                addEnrollment();
            } else if (checkIfEmpty() && add_button1.Enabled == false) {
                editEnrollment();
            } else {
                DialogResult dialog = MessageBox.Show("Missing Fields");
            }
        }

        private void quit_button6_Click(object sender, EventArgs e) {
            this.Close();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
        }

        private void cancel_button4_Click(object sender, EventArgs e) {
            cancelAddEdit();
        }

        private void addEnrollment() {
            Console.WriteLine("Add");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spAddEnrollment", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@StudentID", studentID_textBox2.Text));
                command.Parameters.Add(new SqlParameter("@SectionID", section_textBox3.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();


                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Added Enrollment");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Adding Enrollment");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }

        private void editEnrollment() {
            Console.WriteLine("Edit");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spEditEnrollment", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@EnrollmentID", dataGridView1.SelectedCells[0].Value.ToString()));
                command.Parameters.Add(new SqlParameter("@StudentID", studentID_textBox2.Text));
                command.Parameters.Add(new SqlParameter("@SectionID", section_textBox3.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();


                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Edited Enrollment");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Editing Enrollment");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }

        private void refreshSearchDataGridView() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spAllEnrollSearch", myConnection);

            // 2. Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            //dataGridView1.Rows[0].Selected = false;
            //searchKeyWord();
            myConnection.Close();
        }

        private void searchKeyWord() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spSearchEnrollSearch", myConnection);

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
            if (!String.IsNullOrEmpty(studentID_textBox2.Text)
                && !String.IsNullOrEmpty(section_textBox3.Text)) {
                success = true;
            }
            return success;
        }


        private void initialDisable() {
            chooseStudent_button1.Enabled = false;
            chooseCourse_button1.Enabled = false;
            save_button3.Enabled = false;
        }

        public void addEnable() {
            chooseStudent_button1.Enabled = true;
            chooseCourse_button1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            edit_button5.Enabled = false;
            search_textBox5.Enabled = false;
        }


        public void cancelAddEdit() {
            chooseStudent_button1.Enabled = false;
            chooseCourse_button1.Enabled = false;
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
            chooseStudent_button1.Enabled = true;
            chooseCourse_button1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            add_button1.Enabled = false;
            search_textBox5.Enabled = false;
        }

        public void clearTextBoxes() {
            studentID_textBox2.Clear();
            studentName_textBox2.Clear();
            section_textBox3.Clear();
            courseID_textBox1.Clear();
            courseName_textBox4.Clear();
            instructor_textBox1.Clear();
            semester_textBox5.Clear();
            year_textBox6.Clear();
        }
    }
}
