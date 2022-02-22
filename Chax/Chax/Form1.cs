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
            Print("......", 1000);

            //Setting Video/Camera Variables
            FilterInfoCollection filterInfoCollection;
            VideoCaptureDevice CaptureDevice;
            Print("Initiating Chax", 40);
            Print("..", 850);

            //Filtering Available Cameras
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);
            try { cboDevice.SelectedIndex = 0; } catch { cboDevice.Text = "No Camera Detected"; }
            Print("Executing Start-up procedure", 40);

            //Starting Camera If Found, Else Close Application
            try
            {
                CaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
                CaptureDevice.NewFrame += CaptureDevice_NewFrame;
                CaptureDevice.Start();
                frameTimer.Start();
            } catch {
                //Console.WriteLine("------------------------------");
                //Print("Lucky Error - Camera not found\n.\n.\n.\n.\n.", 45);
                //Environment.Exit(0);
                this.Close();
            }
            Print("Camera Found", 40);
            Print("..", 500);
            Print("Starting Camera Sync", 50);
            Print("Sync Completed", 40);
            Print("Visualising", 40);
            Print(".\n.", 650);

            //Run Console Progress Output
            static void Print(string text, int speed = 40)
            {
                foreach (char c in text)
                {
                    Console.Write(c);
                    System.Threading.Thread.Sleep(speed);
                }
                Console.WriteLine();
            }
            
        }
        //Every Start-up Event, above^^^
        //Every Constant Event Below,
        public int i = 0;
        void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs) //Update PictureBox To Camera, Each Frame
        {
            picBox.Image = (Bitmap)eventArgs.Frame.Clone();
            i++;
            CorrectConsoleProgress();
        }

        void CorrectConsoleProgress()
        {
            if (i == 70) //Manual Wait
            {
                Print("Visualising Synced", 40);
                Print("Executing Hack", 40);
                Print("...", 650);
                static void Print(string text, int speed = 40)
                {
                    foreach (char c in text)
                    {
                        Console.Write(c);
                        System.Threading.Thread.Sleep(speed);
                    }
                    Console.WriteLine();
                }
                Print("Hack Complete", 40);
                Print("-Camera Bugged-",40);
                Print("Exiting Quietly", 40);
                Print(".\n.", 650);
                this.Close();
            }
        }
    }
}