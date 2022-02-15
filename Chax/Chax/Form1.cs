using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Chax
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Setting Video/Camera Variables
            FilterInfoCollection filterInfoCollection;
            VideoCaptureDevice CaptureDevice;
            
            //Filtering Available Cameras
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);
            try { cboDevice.SelectedIndex = 0; } catch { cboDevice.Text = "No Camera Detected"; }

            //Starting Camera If Found, Else Close Application
            try
            {
                CaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
                CaptureDevice.NewFrame += CaptureDevice_NewFrame;
                CaptureDevice.Start();
                frameTimer.Start();
            } catch { this.Close(); }
        }
        //Update PictureBox To Camera, Each Frame
        void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            picBox.Image = (Bitmap)eventArgs.Frame.Clone();
        }
    }
}