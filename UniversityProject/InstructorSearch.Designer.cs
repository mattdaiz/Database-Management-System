namespace UniversityProject {
    partial class InstructorSearch {
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.search_textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.select_button1 = new System.Windows.Forms.Button();
            this.cancel_button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(32, 155);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1768, 584);
            this.dataGridView1.TabIndex = 154;
            this.dataGridView1.TabStop = false;
            // 
            // search_textBox5
            // 
            this.search_textBox5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search_textBox5.Location = new System.Drawing.Point(205, 88);
            this.search_textBox5.Margin = new System.Windows.Forms.Padding(0);
            this.search_textBox5.Name = "search_textBox5";
            this.search_textBox5.Size = new System.Drawing.Size(1588, 51);
            this.search_textBox5.TabIndex = 155;
            this.search_textBox5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_textBox5_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(32, 95);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 43);
            this.label5.TabIndex = 156;
            this.label5.Text = "Search";
            // 
            // select_button1
            // 
            this.select_button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.select_button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.select_button1.Location = new System.Drawing.Point(685, 770);
            this.select_button1.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.select_button1.Name = "select_button1";
            this.select_button1.Size = new System.Drawing.Size(213, 95);
            this.select_button1.TabIndex = 157;
            this.select_button1.Text = "Select";
            this.select_button1.UseVisualStyleBackColor = false;
            this.select_button1.Click += new System.EventHandler(this.select_button1_Click);
            // 
            // cancel_button1
            // 
            this.cancel_button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cancel_button1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_button1.Location = new System.Drawing.Point(952, 770);
            this.cancel_button1.Margin = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.cancel_button1.Name = "cancel_button1";
            this.cancel_button1.Size = new System.Drawing.Size(213, 95);
            this.cancel_button1.TabIndex = 158;
            this.cancel_button1.Text = "Cancel";
            this.cancel_button1.UseVisualStyleBackColor = false;
            this.cancel_button1.Click += new System.EventHandler(this.cancel_button1_Click);
            // 
            // InstructorSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1824, 1099);
            this.Controls.Add(this.cancel_button1);
            this.Controls.Add(this.select_button1);
            this.Controls.Add(this.search_textBox5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "InstructorSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instructor Search";
            this.Load += new System.EventHandler(this.InstructorSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox search_textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button select_button1;
        private System.Windows.Forms.Button cancel_button1;
    }
}