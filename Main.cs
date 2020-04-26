using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WebScraper
{
    public partial class Main : ServiceBase
    {
        private string pageUrl = ConfigurationManager.AppSettings["PAGE_URL"];
        private string firebaseUrl = ConfigurationManager.AppSettings["FIREBASE_URL"];
        private string firebaseSecret = ConfigurationManager.AppSettings["FIREBASE_SECRET"];
        private Scraper scraper;
        private Firebase firebase;
        private Timer timer;
        public LogWriter Logger;
        public Main()
        {
            InitializeComponent();
            Logger = new LogWriter("logger");
            timer = new System.Timers.Timer();
            scraper = new Scraper();
            firebase = new Firebase(firebaseUrl, firebaseSecret);
        }
        private void Update_Database()
        {
            if (firebase.client == null)
            {
                firebase.Connect();
                Logger.LogWrite("Connected to Firebase Realtime Database.");
            }
            var result = scraper.ScrapData(pageUrl);
            foreach (Items item in result)
            {
                firebase.CreateRecord(item);
                Logger.LogWrite($"Data has been inserted for {item.country}");
            }
        }
        public void  OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Update_Database();
            Logger.LogWrite($"Database Updated.");
        }
        public void onDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            timer.Interval = Convert.ToInt32( ConfigurationManager.AppSettings["TIMER_INTERVAL"]) ;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
             protected override void OnStop()
        {

        }
    }
}
