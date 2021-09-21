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

namespace PS_Updater_X
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            string appPatch = System.IO.Path.Combine(Application.StartupPath, "");
            WebClient client = new WebClient();
            string newVersion = client.DownloadString("http://repairbox.pl/PS_OS/latestVersion.txt");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log.AppendText("Checking exe.. ");
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;


        }
    }
}
