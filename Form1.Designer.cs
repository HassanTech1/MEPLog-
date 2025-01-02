namespace SimpleIDE
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox txtCode;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Label lblStatus;
        private Panel panelLineNumbers;

        // Designer method to initialize components
        private void InitializeComponent()
        {
            txtCode = new RichTextBox();
            btnRun = new Button();
            txtOutput = new RichTextBox();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // txtCode
            // 
            txtCode.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCode.Location = new Point(12, 22);
            txtCode.Name = "txtCode";
            txtCode.RightToLeft = RightToLeft.No;
            txtCode.Size = new Size(1244, 340);
            txtCode.TabIndex = 0;
            txtCode.Text = "";
            // 
            // btnRun
            // 
            btnRun.Location = new Point(18, 386);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 23);
            btnRun.TabIndex = 1;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // txtOutput
            // 
            txtOutput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtOutput.Location = new Point(12, 425);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(1244, 150);
            txtOutput.TabIndex = 2;
            txtOutput.Text = "";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(445, 386);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 15);
            lblStatus.TabIndex = 3;
            // 
            // MainForm
            // 
            ClientSize = new Size(1280, 608);
            Controls.Add(lblStatus);
            Controls.Add(txtOutput);
            Controls.Add(btnRun);
            Controls.Add(txtCode);
            Name = "MainForm";
            Text = "MEPLog+";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
