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
    public partial class DataWarehouse : Form {
        String connectionString;
        public DataWarehouse() {
            InitializeComponent();
            //connectionString = "Server=DESKTOP-G5R0M5H\\SQLEXPRESS; Database=UniversityProject; Trusted_Connection=True";
            connectionString = SQLServerConnection.connection;
        }

        private void DataWarehouse_Load(object sender, EventArgs e) {
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

        private void fillDropDown() {
            semester_comboBox6.Items.Add("");
            semester_comboBox6.Items.Add("Fall");
            semester_comboBox6.Items.Add("Winter");
            semester_comboBox6.Items.Add("Spring");
            semester_comboBox6.Items.Add("Summer");


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

        private void search_button1_Click(object sender, EventArgs e) {
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // 1. Create a command object identifying the stored procedure
            SqlCommand command = new SqlCommand("spWarehouseSearch", myConnection);

            // 2. Set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. Add parameters to command, which will be stored to the stored procedure
            command.Parameters.Add(new SqlParameter("@instructor", instructor_search_textBox5.Text));
            command.Parameters.Add(new SqlParameter("@courseID", textBox1_courseID.Text));
            command.Parameters.Add(new SqlParameter("@courseName", textBox2_courseName.Text));
            command.Parameters.Add(new SqlParameter("@semester", semester_comboBox6.Text));
            command.Parameters.Add(new SqlParameter("@year", year_textBox6.Text));
            command.Parameters.Add(new SqlParameter("@department", department_comboBox6.Text));

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            myConnection.Close();
        }

        private void quit_button6_Click(object sender, EventArgs e) {
            this.Close();
            HomeScreen homeScreen = new HomeScreen();
            homeScreen.Show();
        }

        public Boolean checkIfBothFilled() {
            Boolean success = true;
            if (!String.IsNullOrEmpty(semester_comboBox6.Text)
                && !String.IsNullOrEmpty(year_textBox6.Text)) {
                success = false;
            }
            return success;
        }

        private void clear_button1_Click(object sender, EventArgs e) {
            instructor_search_textBox5.Clear();
            textBox1_courseID.Clear();
            textBox2_courseName.Clear();
            department_comboBox6.SelectedIndex = -1;
            semester_comboBox6.SelectedIndex = -1;
            year_textBox6.Clear();
        }

        
    }
}
