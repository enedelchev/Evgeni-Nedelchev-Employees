namespace SirmaSolutionsTask2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBrowse = new System.Windows.Forms.Button();
            this.viewEmpCouples = new System.Windows.Forms.DataGridView();
            this.empID_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empID_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.daysWorked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.viewEmpCouples)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(13, 276);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(468, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // viewEmpCouples
            // 
            this.viewEmpCouples.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.viewEmpCouples.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.empID_1,
            this.empID_2,
            this.projectID,
            this.daysWorked});
            this.viewEmpCouples.Location = new System.Drawing.Point(13, 13);
            this.viewEmpCouples.Name = "viewEmpCouples";
            this.viewEmpCouples.Size = new System.Drawing.Size(468, 248);
            this.viewEmpCouples.TabIndex = 1;
            // 
            // empID_1
            // 
            this.empID_1.HeaderText = "Employee ID #1";
            this.empID_1.Name = "empID_1";
            this.empID_1.ReadOnly = true;
            // 
            // empID_2
            // 
            this.empID_2.HeaderText = "Employee ID #2";
            this.empID_2.Name = "empID_2";
            this.empID_2.ReadOnly = true;
            // 
            // projectID
            // 
            this.projectID.HeaderText = "Project ID";
            this.projectID.Name = "projectID";
            this.projectID.ReadOnly = true;
            // 
            // daysWorked
            // 
            this.daysWorked.HeaderText = "DaysWorked";
            this.daysWorked.Name = "daysWorked";
            this.daysWorked.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 326);
            this.Controls.Add(this.viewEmpCouples);
            this.Controls.Add(this.btnBrowse);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.viewEmpCouples)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DataGridView viewEmpCouples;
        private System.Windows.Forms.DataGridViewTextBoxColumn empID_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn empID_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectID;
        private System.Windows.Forms.DataGridViewTextBoxColumn daysWorked;
    }
}

