using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CloseKodi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            uint lastactiv = (Win32.GetIdleTime() / 1000);

            lblActiv.Text = lastactiv.ToString() + " seconds";

            if (Process.GetProcessesByName(txtProc.Text).Length > 0)
            {
                lblStatus.Text = "Running";

                if (lastactiv > uint.Parse(txtTime.Text))
                {
                    foreach (var process in Process.GetProcesses())
                    {
                        if (process.ProcessName == txtProc.Text)
                            process.Kill();
                    }
                }
            }
            else
            {
                lblStatus.Text = "Close";
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Activate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keypress = e.KeyChar;
            if (!char.IsDigit(keypress))
            {
                e.Handled = true;
                //MessageBox.Show("Just numbers!!!");
            }
        }
    }
}