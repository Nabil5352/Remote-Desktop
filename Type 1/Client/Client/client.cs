using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class client : Form
    {
        private static Bitmap bmpScreenshot;
        private static Graphics gfxScreenshot;

        public client()
        {
            InitializeComponent();
            string path = @"C:\Users\remoteUpload\";
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartCapturing();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopAllCapturing();
        }

        private bool _performCapturing;
        string path = @"C:\Users\remoteUpload\";
        int i = 1;
        private void ClientBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Directory.Exists(path))
            {
                while (true)
                {
                    if (_performCapturing)
                    {
                        bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                        gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                        gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                        
                        switch (i) 
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                bmpScreenshot.Save(path + "0" + i + ".png");
                                ClientService.SendFile(path +"0" + i + ".png");
                                break;
                            default:
                                bmpScreenshot.Save(path + i + ".png");
                                ClientService.SendFile(path + i + ".png");
                                break;
                        }
                        //ClientService.DeleteFile( i);
                        i++;
                    }
                }
            }
        }

        private void StartCapturing()
        {
            _performCapturing = true;
            ClientBackgroundWorker.RunWorkerAsync();
        }

        private void StopAllCapturing()
        {
            _performCapturing = false;
            //ClientService.ClearFolder(path);
            this.Close();
        }

        private void ClientTimer_Tick(object sender, EventArgs e)
        {
            label2.Text = ClientService.showMsg;
        }

    }

    class ClientService
    {
        public static string showMsg = "Idle";

        internal static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.IsReadOnly = false;
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }

        internal static void DeleteFile(int i)
        {
            string[] filePaths = Directory.GetFiles(@"C:\Users\remoteUpload\");
            foreach (string filePath in filePaths)
            {
                if (filePath.Contains(i + ".png"))
                    File.Delete(filePath);
            }
        }

        internal static void SendFile(string fileName)
        {
            try
            {
                IPAddress[] ipAddress = Dns.GetHostAddresses("localhost");
                IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5656);
                Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                string filePath = "";

                fileName = fileName.Replace("\\", "/");
                while (fileName.IndexOf("/") > -1)
                {
                    filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                }

                byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                if (fileNameByte.Length > 850 * 1024)
                {
                    showMsg = "File size is more than 850kb, please try with small file.";
                    return;
                }

                showMsg = "Buffering ...";
                byte[] fileData = File.ReadAllBytes(filePath + fileName);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                showMsg = "Connecting ...";
                clientSock.Connect(ipEnd);

                showMsg = "File sending...";
                clientSock.Send(clientData, 0, fileNameByte.Length, SocketFlags.None);

                showMsg = "Disconnecting...";
                clientSock.Close();
                showMsg = "File transferring...";

            }
            catch (Exception ex)
            {
                if (ex.Message == "No connection could be made because the target machine actively refused it")
                    showMsg = "File Sending fail!!.";
                else
                    showMsg = "File Sending fail." + ex.Message;
            }
        }
    }
}
