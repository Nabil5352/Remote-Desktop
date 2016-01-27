using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;

namespace Server
{
    public partial class server : Form
    {
        private bool _performReceiving;
        public server()
        {
            InitializeComponent();
            string path = @"C:\Users\remoteDownload\";
             ServerService.receivedPath = path;
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
                _performReceiving = true;
                ServerBackgroundWorker.RunWorkerAsync();       
        }

        ServerService obj = new ServerService();
        string path = @"C:\Users\remoteDownload\";
        //FileInfo file = null;
        //int filecount = 0;
        //int i = 0;
        //ArrayList alist = new ArrayList();
        private void ServerBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Directory.Exists(path))
            {
                while (true)
                {
                    if (_performReceiving)
                    {
                            obj.StartServer();
                            ServerTimer.Tick += new EventHandler(ServerTimer_Tick);
                            ServerTimer.Start();
                    }
                }
            }
        }

        private void ServerTimer_Tick(object sender, EventArgs e)
        {
            lblServerStatus.Text = ServerService.curMsg;

            //DirectoryInfo inputDir = new DirectoryInfo(@"C:\Users\remoteDownload\");
            //foreach (FileInfo eachfile in inputDir.GetFiles())
            //{
            //    file = eachfile;
            //    if (file.Extension.ToLower() == ".png")
            //    {
            //        alist.Add(file.FullName);
            //        //Store file count here 
            //        filecount = filecount + 1;
            //    }
            //}
            ////Find first files numeric name
            //Regex rgx = new Regex(@"\d+");
            //Match match = rgx.Match(alist[0] + "");
            //int firstNum = int.Parse(match.Value);
            //if (_performReceiving)
            //{
            //    try
            //    {
            //        while (_performReceiving)
            //        {

                    //    switch (firstNum)
                    //    {
                    //        case 0:
                    //        case 1:
                    //        case 2:
                    //        case 3:
                    //        case 4:
                    //        case 5:
                    //        case 6:
                    //        case 7:
                    //        case 8:
                    //        case 9:
                    //            PicBox.Image = Image.FromFile(path + "0" + firstNum + ".png");
                    //            this.PicBox.SizeMode = PictureBoxSizeMode.Zoom;
                    //            PicBox.Refresh();
                    //            firstNum++;
                    //            break;
                    //        default:
                    //            PicBox.Image = Image.FromFile(path + firstNum + ".png");
                    //            this.PicBox.SizeMode = PictureBoxSizeMode.Zoom;
                    //            PicBox.Refresh();
                    //            firstNum++;
                    //            break;
                    //    }
                    //}
            //    }
            //    catch (FileNotFoundException)
            //    {
            //        ServerTimer.Stop();
            //    }
            //}
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _performReceiving = false;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            _performReceiving = false;
            this.Close();
        }
    }

    class ServerService
    {
        IPEndPoint ipEnd;
        Socket sock;
        public ServerService()
        {
            ipEnd = new IPEndPoint(IPAddress.Any, 5656);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(ipEnd);
        }
        public static string receivedPath;
        public static string curMsg = "Stopped";

        internal void StartServer()
        {
            try
            {
                curMsg = "Starting...";
                sock.Listen(100);

                curMsg = "Running...";
                Socket clientSock = sock.Accept();

                byte[] clientData = new byte[900 * 1024];

                int receivedBytesLen = clientSock.Receive(clientData);

                curMsg = "Receiving...";

                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + "/" + fileName, FileMode.Append)); ;
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                curMsg = "Saving...";

                bWrite.Close();
                clientSock.Close();
                curMsg = "Reeived & Saved..";
            }
            catch (Exception)
            {
                curMsg = "ERROR!!.";
            }
        }
    }
}