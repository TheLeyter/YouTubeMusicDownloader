using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.Net;
using System.IO;
using YoutubeExplode;
using YoutubeExplode.Search;
using YoutubeExplode.Videos;

namespace YouTube_music_downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var youtube = new YoutubeClient();
            Video video = await youtube.Videos.GetAsync(textBox1.Text);
            label1.Text = video.Title;
            var info = video.Engagement;
            label2.Text = info.LikeCount + "";
            label3.Text = info.DislikeCount + "";
            label4.Text = info.ViewCount + " " + "просмотров";
            label5.Text = video.UploadDate.ToString();
            using (WebClient client = new WebClient())
            {
                Stream stream;
                stream = await client.OpenReadTaskAsync(GetUrlImg(textBox1.Text));
                pictureBox1.Image = Image.FromStream(stream);
                stream.Dispose();
            }
        }

        private string GetUrlKey(string url)
        {
            return url.Substring(url.LastIndexOf('=') + 1);
        }

        private string GetUrlImg(string url)
        {
            var ImgUrlKey = GetUrlKey(url);
            return $"https://img.youtube.com/vi/{ImgUrlKey}/maxresdefault.jpg";
        }

    }
}
