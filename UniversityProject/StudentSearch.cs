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
    public partial class StudentSearch : Form {
        String connectionString;
        public StudentSearch() {
            InitializeComponent();
            //connectionString = "Server=DESKTOP-G5R0M5H\\SQLEXPRESS; Database=UniversityProject; Trusted_Connection=True";
            connectionString = SQLServerConnection.connection;
        }

        private void InstructorSearch_Load(object sender, EventArgs e) {
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

        private void search_textBox5_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                searchKeyWord();
            }
        }

        private void select_button1_Click(object sender, EventArgs e) {
            this.ReturnValueID = dataGridView1.SelectedCells[0].Value.ToString();
            this.ReturnValueFirst = dataGridView1.SelectedCells[1].Value.ToString();
            this.ReturnValueLast = dataGridView1.SelectedCells[2].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        // Return the value back to parent form
        public string ReturnValueID { get; set; }
        public string ReturnValueFirst { get; set; }
        public string ReturnValueLast { get; set; }

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






    }
}
