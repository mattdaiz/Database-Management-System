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
    public partial class CourseSearch : Form {
        String connectionString;
        public CourseSearch() {
            InitializeComponent();
            connectionString = SQLServerConnection.connection;
        }

        private void CourseSearch_Load(object sender, EventArgs e) {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spAllEnrollCourseSearch", myConnection);

            // 2. Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            //dataGridView1.Rows[0].Selected = false;
            //searchKeyWord();
            myConnection.Close();


            fillDropDown();
        }

        private void search_button1_Click(object sender, EventArgs e) {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spSearchEnrollCourseSearch", myConnection);

            // 2. Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. Add parameters to command, which will be stored to the stored procedure
            command.Parameters.Add(new SqlParameter("@semester", semester_comboBox1.Text));
            command.Parameters.Add(new SqlParameter("@year", year_textBox1.Text));
            command.Parameters.Add(new SqlParameter("@department", department_comboBox6.Text));

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            myConnection.Close();
        }

        private void clear_button1_Click(object sender, EventArgs e) {
            semester_comboBox1.SelectedIndex = -1;
            department_comboBox6.SelectedIndex = -1;
            year_textBox1.Clear();
        }

        private void select_button1_Click(object sender, EventArgs e) {
            this.ReturnValueSectionID = dataGridView1.SelectedCells[0].Value.ToString();
            this.ReturnValueCourseID = dataGridView1.SelectedCells[1].Value.ToString();
            this.ReturnValueCourseName = dataGridView1.SelectedCells[2].Value.ToString();
            this.ReturnValueInstructor = dataGridView1.SelectedCells[4].Value.ToString();
            this.ReturnValueSemester = dataGridView1.SelectedCells[5].Value.ToString();
            this.ReturnValueYear = dataGridView1.SelectedCells[6].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Return the value back to parent form
        public string ReturnValueSectionID { get; set; }
        public string ReturnValueCourseID { get; set; }
        public string ReturnValueCourseName { get; set; }
        public string ReturnValueInstructor { get; set; }
        public string ReturnValueSemester { get; set; }
        public string ReturnValueYear{ get; set; }

        private void cancel_button1_Click(object sender, EventArgs e) {
            this.Close();
        }




        private void fillDropDown() {
            semester_comboBox1.Items.Add("Fall");
            semester_comboBox1.Items.Add("Winter");
            semester_comboBox1.Items.Add("Spring");
            semester_comboBox1.Items.Add("Summer");


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

            myConnection.Close();
        }

        private void refreshDataGridView() {

        }

       
    }
}
