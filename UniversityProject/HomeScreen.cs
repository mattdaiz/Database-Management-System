using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversityProject;

namespace UniversityProject
{
    public partial class HomeScreen : Form
    {
        public HomeScreen()
        {
            InitializeComponent();
            //StartPosition = FormStartPosition.CenterScreen;

            //this.StartPosition = FormStartPosition.Manual;
            //this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 4;
            //this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 7;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void student_button2_Click(object sender, EventArgs e) {
            this.Hide();
            Student studentForm = new Student();
            studentForm.Show();
        }

        private void enroll_button2_Click(object sender, EventArgs e) {
            this.Hide();
            StudentEnroll studentEnrollForm = new StudentEnroll();
            studentEnrollForm.Show();
        }

        private void instructor_button3_Click(object sender, EventArgs e) {
            this.Hide();
            Instructor instructorForm = new Instructor();
            instructorForm.Show();
        }

        private void course_button1_Click(object sender, EventArgs e) {
            this.Hide();
            Course courseForm = new Course();
            courseForm.Show();
        }


        private void section_button3_Click(object sender, EventArgs e) {
            this.Hide();
            Section sectionForm = new Section();
            sectionForm.Show();
        }

        private void department_button4_Click(object sender, EventArgs e) {
            this.Hide();
            Department departmentForm = new Department();
            departmentForm.Show();
        }

        private void warehouse_button1_Click(object sender, EventArgs e) {
            this.Hide();
            DataWarehouse warehouseForm = new DataWarehouse();
            warehouseForm.Show();
        }     
    }
}
