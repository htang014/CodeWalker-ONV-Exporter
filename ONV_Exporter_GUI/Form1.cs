using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ONV_Exporter_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InputDirectoryButton_Click(object sender, EventArgs e)
        {
            InputDirectoryDialog.ShowDialog();
            InputDirectoryText.Text = InputDirectoryDialog.SelectedPath;
        }

        private void OutputDirectoryButton_Click(object sender, EventArgs e)
        {
            OutputDirectoryDialog.ShowDialog();
            OutputDirectoryText.Text = OutputDirectoryDialog.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("cmd.exe", $"/k batch_exec.bat {InputDirectoryText.Text} {OutputDirectoryText.Text}");
        }
    }
}
