using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TCPTracker
{
    public partial class frmMain : Form
    {

        List<Pinger> pingers = new List<Pinger>();
        public frmMain()
        {
            InitializeComponent();
        }

        

        private void btnAddIP_Click(object sender, EventArgs e)
        {
            if (txtIPAddress.Text != "")
            {
                //pingers.Add(new Pinger(txtIPAddress.Text));
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            pingers.Add(new Pinger(Dns.GetHostAddresses("google.com")[0]));
            pingers.Add(new Pinger(Dns.GetHostAddresses("facebook.com")[0]));
            pingers.Add(new Pinger(Dns.GetHostAddresses("reddit.com")[0]));
            pingers.Add(new Pinger(Dns.GetHostAddresses("duckduckgo.com")[0]));
            pingers.Add(new Pinger("1.1.1.1"));

            foreach (Pinger p in pingers)
            {
                listView1.Items.Add(p.ToString());
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllLines("Log.csv", File.ReadAllLines("Log.csv").Where(l => !string.IsNullOrWhiteSpace(l)));

        }
    }
}
