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
            FilterInfoCollection filterInfoCollection;
            VideoCaptureDevice CaptureDevice;

            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);
            try { cboDevice.SelectedIndex = 0; } catch { cboDevice.Text = "No Camera Detected"; }
            if(cboDevice.SelectedIndex > 0)
            {
                CaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
                CaptureDevice.NewFrame += CaptureDevice_NewFrame;
                CaptureDevice.Start();
                timerQRpic.Start();
                void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
                {
                    picQRscan.Image = (Bitmap)eventArgs.Frame.Clone();
                }
            }
            
        }
    }
}