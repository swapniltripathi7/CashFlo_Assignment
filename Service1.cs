using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {

        public Service1()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10000; // 10000 = 10.000 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }
        public void OnDebug()
        {
            Onstart(null);
        }

        protected override void OnStart(string[] args)
        {
            System.Console.WriteLine("Application service is starting.");
        }

        protected override void OnStop()
        {
            System.Console.WriteLine("Application service is stopping.");
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            System.Console.WriteLine("Application service is recalled after 10s");

            SQLiteConnection conn = new SQLiteConnection(DB_Link.conn_link); // to store in SQLite daabase

            string strurltest = String.Format("url"); // fetch API
            WebRequest requestObjGet = WebRequest.Create(strurltest); 

            requestObjGet.Method = "GET";
            HttpWebResponse responseObjGet = null;
            responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

            string strresult = null;// set null 

            using (Stream stream = responseObjGet.GetResponseStream()) // in json format
            {
                StreamReader sr = new StreamReader(stream);
                strresult = sr.ReadToEnd();
                sr.Close();
            } 

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Comments> objList = (List<Comments>)serializer.Deserializer(strresult, typeof((List<Comments>))); 

            foreach (Comments obj in objList)
            {
                string username = obj.username;
                string password = obj.password;

                SQLiteCommand cmd = new SQLiteCommand("insert into userInfo (user_name,user_pass) values ('" + text_user.Text + "','" + text_pass.Text + "')");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                 

                var vault = new Windows.Security.Credentials.PasswordVault();
                var credential = new Windows.Security.Credentials.PasswordCredential(
                    resource: "Sample Application",
                    userName: this.text_username.Text,
                    password: this.text_password.Text);
                vault.Add(credential);

                System.Console.WriteLine("Insertion Done in SQLite......");
            }
        }
    }
}
