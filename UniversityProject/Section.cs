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
    public partial class Section : Form {
        String connectionString;
        String instructorID;
        public Section() {
            InitializeComponent();
            //connectionString = "Server=DESKTOP-G5R0M5H\\SQLEXPRESS; Database=UniversityProject; Trusted_Connection=True";
            connectionString = SQLServerConnection.connection;
        }


        private void Section_Load(object sender, EventArgs e) {
            initialDisable();
            refreshSearchDataGridView();
            semester_comboBox1.Items.Add("Fall");
            semester_comboBox1.Items.Add("Winter");
            semester_comboBox1.Items.Add("Spring");
            semester_comboBox1.Items.Add("Summer");
        }

        // Instructor Search 
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


        private void search_textBox5_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                searchKeyWord();
            }
        }

        private void refreshSearchDataGridView() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spAllSectionSearch", myConnection);

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

        private void add_button1_Click(object sender, EventArgs e) {
            clearTextBoxes();
            addEnable();

            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spGetAllCourseID", myConnection);

            // 2. Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);


            courseID_comboBox6.DisplayMember = "CourseID";
            courseID_comboBox6.ValueMember = "CourseID";
            courseID_comboBox6.DataSource = dataTable;

            courseID_comboBox6.SelectedIndex = -1;

            myConnection.Close();
        }

        private void edit_button5_Click(object sender, EventArgs e) {
            try {
                sectionID_textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();

                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spGetAllCourseID", myConnection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                courseID_comboBox6.DisplayMember = "CourseID";
                courseID_comboBox6.ValueMember = "CourseID";
                courseID_comboBox6.DataSource = dataTable;
                courseID_comboBox6.SelectedValue = dataGridView1.SelectedCells[1].Value.ToString();



                command = new SqlCommand("spGetInstructorID", myConnection);
                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;


                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@SectionID", sectionID_textBox1.Text));

                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                DataRow row = dataTable.Rows[0];

                instructorID = row["InstructorID"].ToString();
                instructorID_textBox2.Text = row["InstructorID"] + " - " + dataGridView1.SelectedCells[3].Value.ToString();



                semester_comboBox1.Text = dataGridView1.SelectedCells[4].Value.ToString();
                year_textBox1.Text = dataGridView1.SelectedCells[5].Value.ToString();



                EditEnable();
                myConnection.Close();
            } catch {
                DialogResult dialog = MessageBox.Show("No Instructor Selected");
            }
        }

        private void save_button3_Click(object sender, EventArgs e) {
            if (checkIfEmpty() && edit_button5.Enabled == false) {
                addSection();
            } else if (checkIfEmpty() && add_button1.Enabled == false) {
                editSection();
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


        private void addSection() {
            Console.WriteLine("Add");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spAddSection", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@SectionID", sectionID_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@CourseID",courseID_comboBox6.Text));
                command.Parameters.Add(new SqlParameter("@InstructorID", instructorID));
                command.Parameters.Add(new SqlParameter("@Semester", semester_comboBox1.Text));
                command.Parameters.Add(new SqlParameter("@Year", year_textBox1.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();

                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Added Section");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Adding Instructor");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }


        private void editSection() {
            Console.WriteLine("Edit");
            try {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // 1. Create a command object identifying the stored procedure
                SqlCommand command = new SqlCommand("spEditSection", connection);

                // 2. Set the command object so it knows to execute a stored procedure
                command.CommandType = CommandType.StoredProcedure;

                // 3. Add parameters to command, which will be stored to the stored procedure
                command.Parameters.Add(new SqlParameter("@SectionID", sectionID_textBox1.Text));
                command.Parameters.Add(new SqlParameter("@CourseID", courseID_comboBox6.Text));
                command.Parameters.Add(new SqlParameter("@InstructorID", instructorID));
                command.Parameters.Add(new SqlParameter("@Semester", semester_comboBox1.Text));
                command.Parameters.Add(new SqlParameter("@Year", year_textBox1.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connection.Close();


                refreshSearchDataGridView();

                clearTextBoxes();
                DialogResult dialog = MessageBox.Show("Successfully Edited Section");
                cancelAddEdit();
            } catch (Exception ex) {
                DialogResult dialog = MessageBox.Show("Error Editing Section");
                Console.WriteLine("Error Rollback: " + ex);
            }
        }






        private void searchKeyWord() {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spSearchSectionSearch", myConnection);

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
            sectionID_textBox1.Enabled = false;
            courseID_comboBox6.Enabled = false;
            instructorID_textBox2.Enabled = false;
            choose_button1.Enabled = false;
            semester_comboBox1.Enabled = false;
            year_textBox1.Enabled = false;
            save_button3.Enabled = false;
        }

        public void addEnable() {
            sectionID_textBox1.Enabled = true;
            courseID_comboBox6.Enabled = true;
            semester_comboBox1.Enabled = true;
            instructorID_textBox2.Enabled = true;
            choose_button1.Enabled = true;
            year_textBox1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            edit_button5.Enabled = false;
            search_textBox5.Enabled = false;
        }


        public void cancelAddEdit() {
            sectionID_textBox1.Enabled = false;
            courseID_comboBox6.Enabled = false;
            semester_comboBox1.Enabled = false;
            instructorID_textBox2.Enabled = false;
            choose_button1.Enabled = false;
            year_textBox1.Enabled = false;
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
            //sectionID_textBox1.Enabled = true;
            courseID_comboBox6.Enabled = true;
            semester_comboBox1.Enabled = true;
            instructorID_textBox2.Enabled = false;
            choose_button1.Enabled = true;
            year_textBox1.Enabled = true;
            save_button3.Enabled = true;
            dataGridView1.Enabled = false;
            add_button1.Enabled = false;
            search_textBox5.Enabled = false;
        }

        public void clearTextBoxes() {
            sectionID_textBox1.Clear();
            courseID_comboBox6.SelectedIndex = -1;
            instructorID_textBox2.Clear();
            semester_comboBox1.SelectedIndex = -1;
            year_textBox1.Clear();
        }

        public Boolean checkIfEmpty() {
            Boolean success = false;
            if (!String.IsNullOrEmpty(sectionID_textBox1.Text)
                && !String.IsNullOrEmpty(courseID_comboBox6.Text)
                && !String.IsNullOrEmpty(instructorID_textBox2.Text)
                && !String.IsNullOrEmpty(semester_comboBox1.Text)
                && !String.IsNullOrEmpty(year_textBox1.Text)) {
                success = true;
            }
            return success;
        }


    }
}
