namespace Client
{
    partial class client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStatusHeader = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClientTimer = new System.Windows.Forms.Timer(this.components);
            this.btnStop = new System.Windows.Forms.Button();
            this.ClientBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(106, 98);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(116, 45);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Client";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStatusHeader
            // 
            this.btnStatusHeader.AutoSize = true;
            this.btnStatusHeader.Location = new System.Drawing.Point(168, 193);
            this.btnStatusHeader.Name = "btnStatusHeader";
            this.btnStatusHeader.Size = new System.Drawing.Size(37, 13);
            this.btnStatusHeader.TabIndex = 1;
            this.btnStatusHeader.Text = "Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Idle";
            // 
            // ClientTimer
            // 
            this.ClientTimer.Enabled = true;
            this.ClientTimer.Interval = 500;
            this.ClientTimer.Tick += new System.EventHandler(this.ClientTimer_Tick);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(240, 98);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(132, 45);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop Client";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // ClientBackgroundWorker
            // 
            this.ClientBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ClientBackgroundWorker_DoWork);
            // 
            // client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 441);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStatusHeader);
            this.Controls.Add(this.btnStart);
            this.Name = "client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label btnStatusHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer ClientTimer;
        private System.Windows.Forms.Button btnStop;
        private System.ComponentModel.BackgroundWorker ClientBackgroundWorker;
    }
}

