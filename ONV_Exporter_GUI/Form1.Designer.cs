
namespace ONV_Exporter_GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InputDirectoryText = new System.Windows.Forms.TextBox();
            this.OutputDirectoryText = new System.Windows.Forms.TextBox();
            this.InputDirectoryButton = new System.Windows.Forms.Button();
            this.OutputDirectoryButton = new System.Windows.Forms.Button();
            this.InputDirectoryDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.OutputDirectoryDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.RunButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input directory";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output directory";
            // 
            // InputDirectoryText
            // 
            this.InputDirectoryText.Location = new System.Drawing.Point(12, 64);
            this.InputDirectoryText.Name = "InputDirectoryText";
            this.InputDirectoryText.Size = new System.Drawing.Size(626, 26);
            this.InputDirectoryText.TabIndex = 2;
            // 
            // OutputDirectoryText
            // 
            this.OutputDirectoryText.Location = new System.Drawing.Point(12, 151);
            this.OutputDirectoryText.Name = "OutputDirectoryText";
            this.OutputDirectoryText.Size = new System.Drawing.Size(626, 26);
            this.OutputDirectoryText.TabIndex = 3;
            // 
            // InputDirectoryButton
            // 
            this.InputDirectoryButton.Location = new System.Drawing.Point(644, 64);
            this.InputDirectoryButton.Name = "InputDirectoryButton";
            this.InputDirectoryButton.Size = new System.Drawing.Size(82, 26);
            this.InputDirectoryButton.TabIndex = 4;
            this.InputDirectoryButton.Text = "Browse";
            this.InputDirectoryButton.UseVisualStyleBackColor = true;
            this.InputDirectoryButton.Click += new System.EventHandler(this.InputDirectoryButton_Click);
            // 
            // OutputDirectoryButton
            // 
            this.OutputDirectoryButton.Location = new System.Drawing.Point(644, 151);
            this.OutputDirectoryButton.Name = "OutputDirectoryButton";
            this.OutputDirectoryButton.Size = new System.Drawing.Size(82, 26);
            this.OutputDirectoryButton.TabIndex = 5;
            this.OutputDirectoryButton.Text = "Browse";
            this.OutputDirectoryButton.UseVisualStyleBackColor = true;
            this.OutputDirectoryButton.Click += new System.EventHandler(this.OutputDirectoryButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(559, 402);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(167, 36);
            this.RunButton.TabIndex = 6;
            this.RunButton.Text = "Run Script";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.OutputDirectoryButton);
            this.Controls.Add(this.InputDirectoryButton);
            this.Controls.Add(this.OutputDirectoryText);
            this.Controls.Add(this.InputDirectoryText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "ONV Exporter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox InputDirectoryText;
        private System.Windows.Forms.TextBox OutputDirectoryText;
        private System.Windows.Forms.Button InputDirectoryButton;
        private System.Windows.Forms.Button OutputDirectoryButton;
        private System.Windows.Forms.FolderBrowserDialog InputDirectoryDialog;
        private System.Windows.Forms.FolderBrowserDialog OutputDirectoryDialog;
        private System.Windows.Forms.Button RunButton;
    }
}

