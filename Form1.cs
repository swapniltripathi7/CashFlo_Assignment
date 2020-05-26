using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsService1
{
    public partial class Form1 : Form
    {
        SQLiteConnection conn = new SQLiteConnection(DB_Link.conn_link);
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int check_user = 0; // check if username is entered 
            int check_pass = 0; // check if password is entered

            if(text_user.Text == "")
            { 
                // if username is not entered 
                MessageBox.Show("Username Required");
            }
            else
            {
                // user has entered the username
                check_user = 1;
            }
            if(text_pass.Text == "")
            {
                // if password is not entered 
                MessageBox.Show("Password Required");
            }
            else
            {
                // user has entered pass
                check_pass = 1;
            }
            
            if(check_user == 1 && check_pass == 1) // if both username and password has entered
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand("insert into userInfo (user_name,user_pass) values ('" + text_user.Text + "','" + text_pass.Text + "')");
                    conn.Open();
                    cmd.ExecuteNonQuery(); // save username and password in SQLite database " DemoDB.db "
                    conn.Close();
                    MessageBox.Show("Thank You! Insertion Done...");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // exit()
        }

        // system tray
        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
    }
}
