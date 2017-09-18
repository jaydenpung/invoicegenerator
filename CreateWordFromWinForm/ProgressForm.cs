using CreateWordFromWinForm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CreateWordFromWinForm
{
    public partial class ProgressForm : Form
    {
        Logger logger = new Logger();

        public ProgressForm()
        {
            try
            {
                InitializeComponent();
                DownloadNewInstaller();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
}

        private void DownloadNewInstaller()
        {
            try
            {
                if (File.Exists(Config.INSTALLER_NAME))
                {
                    File.Copy(Config.INSTALLER_NAME, "Old_" + Config.INSTALLER_NAME, true);
                }

                Thread thread = new Thread(() => {
                    using (var client = new WebClient())
                    {
                        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadNewInstaller_DownloadProgressChanged);
                        client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadNewInstaller_Completed);
                        client.DownloadFileAsync(new Uri(Config.UPDATE_URL), Config.INSTALLER_NAME);
                    }
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void DownloadNewInstaller_Completed(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    lblProgress.Text = "Completed. Launching Installer ...";
                });

                //Run Installer
                Process.Start(Config.INSTALLER_NAME);
                Application.Exit();
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }

        private void DownloadNewInstaller_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    double bytesIn = double.Parse(e.BytesReceived.ToString());
                    double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                    double percentage = bytesIn / totalBytes * 100;
                    lblProgress.Text = "Downloaded " + e.BytesReceived/1000 + "kB of " + e.TotalBytesToReceive/1000 + "kB";
                    progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
                });
            }
            catch (Exception ex)
            {
                logger.Error("Class: " + this.GetType().FullName + "; Method: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "; Message: " + ex.Message);
            }
        }
    }
}
