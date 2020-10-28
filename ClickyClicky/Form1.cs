using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows; // Or use whatever point class you like for the implicit cast operator
using System.Collections;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;
using QuickType;
using System.IO;
using System.Collections.Specialized;

namespace ClickyClicky
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]



        public static extern void mouse_event(int dwflags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;


        private void Form1_Load(object sender, EventArgs e)
        {

            Timer t1 = new Timer();
            t1.Interval = 50;
            t1.Tick += new EventHandler(timer1_Tick);
            t1.Enabled = true;


            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://adgambling.com/keywords.json");
                var welcome = Welcome.FromJson(jsonString);
                Console.WriteLine(welcome);
            }

      

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = Cursor.Position.X.ToString();
            label4.Text = Cursor.Position.Y.ToString();
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://google.com/search?q="+ Properties.Settings.Default.searchString);
 
            webBrowser1.Navigate("http://google.com/search?q=" + Properties.Settings.Default.searchString);

            textBox2.Text = webBrowser1.DocumentText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = 1389;
            int y = -1881;
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchTermListbox.Items.Add(searchTermTextInput.Text);


        }

        private void searchTermListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            MessageBox.Show("Saved");
        }

        private void button5_Click(object sender, EventArgs e)
        {
   

            checkin();
        }

        void checkin()
        {
         DateTime date = new DateTime(2019, 11, 12, 22, 45, 12, 004);

            string date_str = date.ToString("dd/MM/yyyy HH:mm:ss");

            var url = "https://diablo.hugoadmin.com/api/checkin";

            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["checkin"] = pubIp;
           

                var response = wb.UploadValues(url, "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);
            }
        }
    }
}
