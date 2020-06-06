using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MailService
{
    public partial class Service1 : ServiceBase
    {
        private static System.Timers.Timer aTimer;
        public Service1()
        {
            InitializeComponent();

        }

        protected override void OnStart(string[] args)
        {
            try
            {

                Log("Starting Service");
                // ReadMails.GetMails();
                aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //change the value to 10000 for testing
                // aTimer.Interval = 1800000/2;
                aTimer.Interval = 300000;
                aTimer.Enabled = true;

            }
            catch (Exception ex)

            { Log(ex.Message); }
        }
        public static void Log(string logMessage)
        {
            string _logFileLocation = @"C:\temp\servicelog_" + DateTime.Now.ToString("dd-MMM-yy") + ".txt";
            Directory.CreateDirectory(Path.GetDirectoryName(_logFileLocation));
            File.AppendAllText(_logFileLocation, DateTime.UtcNow.ToString() + " : " + logMessage + Environment.NewLine);
        }
        protected override void OnStop()
        {
            Log("Stopped");
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Log("Starting fetching");
            ReadMails.GetMails();

        }
    }
}
