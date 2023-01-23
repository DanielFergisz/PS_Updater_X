using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PS_Updater_X
{
    public partial class Form1 : Form
    {
        private string appPatch = System.IO.Path.Combine(Application.StartupPath, "");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log.AppendText("Checking exe.. ");
            timer1.Enabled = true;

            string[] Process_name_list = { "PS_OS", "PsFirm" };
            foreach (string Process_name in Process_name_list)
            {
                foreach (var process in Process.GetProcessesByName(Process_name))
                {
                    process.Kill();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (File.Exists("PsFirm.exe") || File.Exists("PS_OS.exe"))
            {
                Log.SelectionColor = Color.ForestGreen;
                Log.AppendText("OK");
                Log.SelectionColor = Color.Empty;
                Log.AppendText(Environment.NewLine + "");
                Log.AppendText(Environment.NewLine + "Rename old version.. ");
                timer2.Enabled = true;
            }
            else
            {
                Log.SelectionColor = Color.Red;
                Log.AppendText("Failure !!!");
                Log.SelectionColor = Color.Empty;
                Log.AppendText(Environment.NewLine + "");
                Log.AppendText(Environment.NewLine + "No application to update.");
                timer6.Enabled = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;

            if (File.Exists("PsFirm.exe"))
            {
                try
                {
                    File.Move("PsFirm.exe", "PsFirm.exe.old");
                }
                catch
                {
                    File.Delete("PsFirm.exe.old");
                    File.Move("PsFirm.exe", "PsFirm.exe.old");
                }
            }

            if (File.Exists("PS_OS.exe"))
            {
                try
                {
                    File.Move("PS_OS.exe", "PS_OS.exe.old");
                }
                catch
                {
                    File.Delete("PS_OS.exe.old");
                    File.Move("PS_OS.exe", "PS_OS.exe.old");
                }
            }

            Log.SelectionColor = Color.ForestGreen;
            Log.AppendText("OK");
            Log.SelectionColor = Color.Empty;
            Log.AppendText(Environment.NewLine + "Download new version.. ");
            timer3.Enabled = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Enabled = false;
            WebClient client = new WebClient();
            string newVersion = client.DownloadString("http://repairbox.pl/PS_OS/latestVersion.txt");
                 
            client.DownloadFile("http://repairbox.pl/PS_OS/" + newVersion + "/PsFirm.exe", appPatch + "/PsFirm.exe");
           
            client.Dispose();
            Log.SelectionColor = Color.ForestGreen;
            Log.AppendText("OK");
            Log.SelectionColor = Color.Empty;
            timer4.Enabled = true;
            Log.AppendText(Environment.NewLine + "Updating.. ");
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Enabled = false;
            Log.SelectionColor = Color.ForestGreen;
            Log.AppendText("OK");
            Log.SelectionColor = Color.Empty;
            Log.AppendText(Environment.NewLine + "Update completed.");
            Log.AppendText(Environment.NewLine + "");
            try
            {
                Process.Start("PsFirm.exe");
                Log.AppendText(Environment.NewLine + "Restarting...");
                timer5.Enabled = true;
            }
            catch 
            {
                Log.AppendText(Environment.NewLine + "Checking.. ");
                Log.SelectionColor = Color.Red;
                Log.AppendText("Failure !!!");
                Log.SelectionColor = Color.Empty;
                timer7.Enabled = true;
            }
            
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            timer5.Enabled = false;
            
            string[] Process_name_list = {"PS_Updater_X", "Updater_PS"};
            foreach (string Process_name in Process_name_list)
            {
                foreach (var process in Process.GetProcessesByName(Process_name))
                {
                    process.Kill();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.repairbox.pl");
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            timer6.Enabled = false;

            string[] Process_name_list = { "PS_Updater_X", "Updater_PS" };
            foreach (string Process_name in Process_name_list)
            {
                foreach (var process in Process.GetProcessesByName(Process_name))
                {
                    process.Kill();
                }
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            timer7.Enabled = false;
            WebClient client = new WebClient();
            string newVersion = client.DownloadString("http://repairbox.pl/PS_OS/latestVersion.txt");

            client.DownloadFile("http://repairbox.pl/PS_OS/LastV/PS_OS.exe", appPatch + "/PsFirm.exe");

            client.Dispose();
            timer4.Enabled = true;
            Log.AppendText(Environment.NewLine + "Updating.. ");
        }
    }
}
