using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwaitDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "InProgress!";
            string url = ("https://www.slideshare.net/NoumanBaloch5/dining-philosopher-problem-124674804");
            //ReadDataFromUrl(url);
            label2.Text = "Completed";
        }

        private async Task<string> ReadDataFromUrl(string url)
        {
            WebClient wc = new WebClient();
            byte[] result = await wc.DownloadDataTaskAsync(url); //thread 5
            string data = Encoding.ASCII.GetString(result);
            return data;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "InProgress!";
            string url = ("https://www.slideshare.net/NoumanBaloch5/dining-philosopher-problem-124674804");
            await ReadDataFromUrl(url); 
            label2.Text = "Completed";
        }
    }
}
