using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPTracker
{
    public class Pinger : Observable
    {
        Ping pingSender = new Ping();
        private IPAddress address;
        private PingReply reply;

        byte[] buffer = Encoding.ASCII.GetBytes(".");
        PingOptions options = new PingOptions(50, true);
        AutoResetEvent reset = new AutoResetEvent(false);

        

        public Pinger(IPAddress IPadress)
        {
            address = IPadress;

            BackgroundWorker BW = new BackgroundWorker();

            BW.WorkerSupportsCancellation = true;
            BW.DoWork += new DoWorkEventHandler(BW_doWork);

            pingSender.PingCompleted += new PingCompletedEventHandler(ping_Complete);
            Attach(PingLogger.Instance());
            if (!BW.IsBusy)
            {
                BW.RunWorkerAsync();
            }
        }

        public Pinger(string IPadress)
        {
            address = IPAddress.Parse(IPadress);

            BackgroundWorker BW = new BackgroundWorker();

            BW.WorkerSupportsCancellation = true;
            BW.DoWork += new DoWorkEventHandler(BW_doWork);

            pingSender.PingCompleted += new PingCompletedEventHandler(ping_Complete);
            Attach(PingLogger.Instance());
            if (!BW.IsBusy)
            {
                BW.RunWorkerAsync();
            }
        }

        private async void ping_Complete(object sender, PingCompletedEventArgs e)
        {
            reply = e.Reply;
            try
            {
                await Notify(reply, address);
            }
            catch { }
        }

        private void BW_doWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                pingSender.SendAsync(address, 5000, buffer, options, reset);
                Thread.Sleep(10000);
            }
        }

        public override string ToString()
        {
            return address.ToString();
        }
    }
}
