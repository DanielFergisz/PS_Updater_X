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
using ImTools;

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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (File.Exists("PS_OS.exe"))
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
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            File.Move("PS_OS.exe", "PS_OS.exe.old");
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
            client.DownloadFile("http://repairbox.pl/PS_OS/" + newVersion + "/PS_OS.exe", appPatch + "/PS_OS.exe");
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
            Process.Start("PS_OS.exe");
            Log.AppendText(Environment.NewLine + "Restarting...");
            timer5.Enabled = true;
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            timer5.Enabled = false;
            foreach (var process in Process.GetProcessesByName("PS_Updater_X"))
            {
                process.Kill();
            }
        }
    }
}
