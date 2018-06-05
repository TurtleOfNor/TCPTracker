using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Threading;
using System.Diagnostics;

namespace TCPTracker
{
    public class PingLogger : Observer
    {
        private static PingLogger instance;
        private int index = -1;
        private bool sendMail = false;
        private IPAddress IPAdd;
        private int failed = 0;
        
        struct result
        {
            public PingReply reply;
            public IPAddress IP;
        }
        List<result> results = new List<result>();
        public static PingLogger Instance()
        {
            if (instance == null)
            {
                instance = new PingLogger();
            }

            return instance;
        }


        StringBuilder sb = new StringBuilder();
        private PingLogger()
        {
            Timer emailTimer = new Timer(sendEmail,null,0, 10000);
        }

        public override async Task Update(PingReply reply, IPAddress IP)
        {
            index = -1;
            result r;
            r.reply = reply;
            r.IP = IP;
            if (results.Count == 0)
            {
                results.Add(r);
            }

            for (int i = 0; i<results.Count;i++)
            {
                if(results[i].IP == r.IP)
                {
                    index = i;
                }
            }
            if (index != -1)
            {
                results[index] = r;
            }
            else
            {
                results.Add(r);
            }

            if (results.All(c=> c.reply.Status != IPStatus.Success))
            {
                await WriteMessage(reply, IP, false);
            }
            if(r.reply.RoundtripTime > 500)
            {
                await WriteMessage(reply, IP, true);
            }
        }

        private async Task WriteMessage(PingReply reply, IPAddress IP, bool InternetUp)
        {
            string newLine;
            IPAdd = IP;
            sendMail = true;
            sb.Clear();
            if (InternetUp)
            {
                newLine = string.Format("{0},{1},{2},{3},High Ping", IP, reply.RoundtripTime, reply.Status, DateTime.Now);

            }
            else
            {
                newLine = string.Format("{0},{1},{2},{3},Internet Down", IP, reply.RoundtripTime, reply.Status, DateTime.Now);
            }
            

            using (FileStream stream = new FileStream("Log.csv", true ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            using (StreamWriter sw = new StreamWriter(stream))
            {
                await sw.WriteLineAsync(newLine);
            };
        }

        private void sendEmail(Object stateInfo)
        {
            if (sendMail)
            {
                Debug.WriteLine("Sending mail");
                MailMessage mail = new MailMessage("notify@internetdown.com", "aidan.junkmail132@gmail.com");
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("internetisdown@gmail.com", "NotPassword132");
                client.Host = "smtp.gmail.com";
                client.SendCompleted += new SendCompletedEventHandler(Sent);
                mail.Subject = "Internet Is Down at " + DateTime.Now;
                mail.Body = "Your Internet has gone down while trying to connect to " + IPAdd + " at " + DateTime.Now.AddMinutes(1) + " Error: " + results[index].reply.Status + " Response Time: " + results[index].reply.RoundtripTime;
                try
                {
                    client.SendAsync(mail, "test");
                }
                catch { }
                
            }
        }

        private void Sent(object sender, AsyncCompletedEventArgs e)
        {
            

            if (e.Error != null)
            {
                Debug.WriteLine("[{0}] {1}", e.Error.ToString());
                failed++;
                if (failed >= 10)
                {
                    sendMail = false;
                    IPAdd = null;
                }
            }
            else
            {
                Debug.WriteLine("Message sent.");
                IPAdd = null;
                sendMail = false;
            }
           
        }
    }
}
