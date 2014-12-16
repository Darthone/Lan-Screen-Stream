using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Expression.Encoder.Live;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Devices;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Microsoft.Expression.Encoder.ScreenCapture;
using Microsoft.Expression.Encoder.Profiles;

namespace LSS_Client {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            // show recording options
            foreach (EncoderDevice ved in EncoderDevices.FindDevices(EncoderDeviceType.Video)){
                videoMenu.Items.Add(ved.Name);
            }
            foreach (EncoderDevice aed in EncoderDevices.FindDevices(EncoderDeviceType.Audio)) {
                audioMenu.Items.Add(aed.Name);
            }

        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {

        }

        private void StartCasting_Click(object sender, EventArgs e) {
            EncoderDevice videoDev = null;
            EncoderDevice audioDev = null;
            foreach (EncoderDevice ved in EncoderDevices.FindDevices(EncoderDeviceType.Video)) {
                if (ved.Name == (string)videoMenu.SelectedItem) {
                    videoDev = ved;
                }
            }
            foreach (EncoderDevice aed in EncoderDevices.FindDevices(EncoderDeviceType.Audio)) {
                Trace.WriteLine(aed.Name);
                Trace.WriteLine(audioMenu.SelectedItem);
                if (aed.Name == (string)audioMenu.SelectedItem) {
                    audioDev = aed;
                }
            }

            var job = new LiveJob();

            Collection<EncoderDevice> devices = EncoderDevices.FindDevices(EncoderDeviceType.Video);
            EncoderDevice device = devices[0]; // maybe its somewhere else, so check for name ...  

            LiveDeviceSource source = job.AddDeviceSource(device, null);
            source.ScreenCaptureSourceProperties = new ScreenCaptureSourceProperties {
                CaptureCursor = true,
                CaptureLargeCursor = false,
                FrameRate = 30,
                CaptureLayeredWindow = true,
                Height = 1080,
                Width = 1920,
                Left = 0,
                Top = 0,
            };
            job.ActivateSource(source);

            // Finds and applys a smooth streaming preset
            //job.ApplyPreset(LivePresets.VC1Broadband16x9);

            // Sets up variable for fomat data

            var format = new PullBroadcastPublishFormat { BroadcastPort = 8080 };

            job.PublishFormats.Add(format);
            //var data = job.BufferWindowSize;
            job.StartEncoding();
        }

        private void label3_Click(object sender, EventArgs e) {

        }
    }
}
