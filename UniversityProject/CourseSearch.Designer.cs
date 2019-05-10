namespace UniversityProject {
    partial class CourseSearch {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cancel_button1 = new System.Windows.Forms.Button();
            this.select_button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.semester_comboBox1 = new System.Windows.Forms.ComboBox();
            this.year_textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.department_comboBox6 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.search_button1 = new System.Windows.Forms.Button();
            this.clear_button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cancel_button1
            // 
            this.cancel_button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cancel_button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_button1.Location = new System.Drawing.Point(933, 947);
            this.cancel_button1.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.cancel_button1.Name = "cancel_button1";
            this.cancel_button1.Size = new System.Drawing.Size(213, 95);
            this.cancel_button1.TabIndex = 163;
            this.cancel_button1.Text = "Cancel";
            this.cancel_button1.UseVisualStyleBackColor = false;
            this.cancel_button1.Click += new System.EventHandler(this.cancel_button1_Click);
            // 
            // select_button1
            // 
            this.select_button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.select_button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.select_button1.Location = new System.Drawing.Point(667, 947);
            this.select_button1.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.select_button1.Name = "select_button1";
            this.select_button1.Size = new System.Drawing.Size(213, 95);
            this.select_button1.TabIndex = 162;
            this.select_button1.Text = "Select";
            this.select_button1.UseVisualStyleBackColor = false;
            this.select_button1.Click += new System.EventHandler(this.select_button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 117);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1768, 584);
            this.dataGridView1.TabIndex = 159;
            this.dataGridView1.TabStop = false;
            // 
            // semester_comboBox1
            // 
            this.semester_comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.semester_comboBox1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.semester_comboBox1.FormattingEnabled = true;
            this.semester_comboBox1.IntegralHeight = false;
            this.semester_comboBox1.Location = new System.Drawing.Point(320, 737);
            this.semester_comboBox1.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.semester_comboBox1.Name = "semester_comboBox1";
            this.semester_comboBox1.Size = new System.Drawing.Size(337, 50);
            this.semester_comboBox1.TabIndex = 181;
            // 
            // year_textBox1
            // 
            this.year_textBox1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.year_textBox1.Location = new System.Drawing.Point(869, 737);
            this.year_textBox1.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.year_textBox1.Name = "year_textBox1";
            this.year_textBox1.Size = new System.Drawing.Size(337, 51);
            this.year_textBox1.TabIndex = 180;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(717, 744);
            this.label6.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 43);
            this.label6.TabIndex = 179;
            this.label6.Text = "Year";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(75, 744);
            this.label4.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 43);
            this.label4.TabIndex = 178;
            this.label4.Text = "Semester";
            // 
            // department_comboBox6
            // 
            this.department_comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.department_comboBox6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.department_comboBox6.FormattingEnabled = true;
            this.department_comboBox6.Location = new System.Drawing.Point(320, 849);
            this.department_comboBox6.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.department_comboBox6.Name = "department_comboBox6";
            this.department_comboBox6.Size = new System.Drawing.Size(887, 50);
            this.department_comboBox6.TabIndex = 183;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(75, 856);
            this.label7.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 43);
            this.label7.TabIndex = 182;
            this.label7.Text = "Subject";
            // 
            // search_button1
            // 
            this.search_button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.search_button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search_button1.Location = new System.Drawing.Point(1267, 770);
            this.search_button1.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.search_button1.Name = "search_button1";
            this.search_button1.Size = new System.Drawing.Size(213, 95);
            this.search_button1.TabIndex = 184;
            this.search_button1.Text = "Search";
            this.search_button1.UseVisualStyleBackColor = false;
            this.search_button1.Click += new System.EventHandler(this.search_button1_Click);
            // 
            // clear_button1
            // 
            this.clear_button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.clear_button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear_button1.Location = new System.Drawing.Point(1533, 773);
            this.clear_button1.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.clear_button1.Name = "clear_button1";
            this.clear_button1.Size = new System.Drawing.Size(213, 95);
            this.clear_button1.TabIndex = 185;
            this.clear_button1.Text = "Clear";
            this.clear_button1.UseVisualStyleBackColor = false;
            this.clear_button1.Click += new System.EventHandler(this.clear_button1_Click);
            // 
            // CourseSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1824, 1099);
            this.Controls.Add(this.clear_button1);
            this.Controls.Add(this.search_button1);
            this.Controls.Add(this.department_comboBox6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.semester_comboBox1);
            this.Controls.Add(this.year_textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cancel_button1);
            this.Controls.Add(this.select_button1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "CourseSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CourseSearch";
            this.Load += new System.EventHandler(this.CourseSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancel_button1;
        private System.Windows.Forms.Button select_button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox semester_comboBox1;
        private System.Windows.Forms.TextBox year_textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox department_comboBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button search_button1;
        private System.Windows.Forms.Button clear_button1;
    }
}