namespace BKnE2Client.client.view
{
    partial class Lobby
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
            this.playerListBox = new System.Windows.Forms.ListBox();
            this.chatTextBox = new System.Windows.Forms.TextBox();
            this.chatListBox = new System.Windows.Forms.ListBox();
            this.playButton = new System.Windows.Forms.Button();
            this.playerPanel = new System.Windows.Forms.Panel();
            this.chatPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.serverListBox = new System.Windows.Forms.ListBox();
            this.playerPanel.SuspendLayout();
            this.chatPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // playerListBox
            // 
            this.playerListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerListBox.FormattingEnabled = true;
            this.playerListBox.Location = new System.Drawing.Point(0, 0);
            this.playerListBox.Name = "playerListBox";
            this.playerListBox.Size = new System.Drawing.Size(300, 681);
            this.playerListBox.TabIndex = 0;
            // 
            // chatTextBox
            // 
            this.chatTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chatTextBox.Location = new System.Drawing.Point(0, 661);
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.Size = new System.Drawing.Size(300, 20);
            this.chatTextBox.TabIndex = 0;
            // 
            // chatListBox
            // 
            this.chatListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatListBox.FormattingEnabled = true;
            this.chatListBox.Location = new System.Drawing.Point(0, 0);
            this.chatListBox.Name = "chatListBox";
            this.chatListBox.Size = new System.Drawing.Size(300, 661);
            this.chatListBox.TabIndex = 1;
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(200, 600);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(264, 61);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "PLAY GAME";
            this.playButton.UseVisualStyleBackColor = true;
            // 
            // playerPanel
            // 
            this.playerPanel.Controls.Add(this.playerListBox);
            this.playerPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.playerPanel.Location = new System.Drawing.Point(964, 0);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(300, 681);
            this.playerPanel.TabIndex = 4;
            // 
            // chatPanel
            // 
            this.chatPanel.Controls.Add(this.chatListBox);
            this.chatPanel.Controls.Add(this.chatTextBox);
            this.chatPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.chatPanel.Location = new System.Drawing.Point(0, 0);
            this.chatPanel.Name = "chatPanel";
            this.chatPanel.Size = new System.Drawing.Size(300, 681);
            this.chatPanel.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.serverListBox);
            this.panel1.Controls.Add(this.playButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(300, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 681);
            this.panel1.TabIndex = 6;
            // 
            // serverListBox
            // 
            this.serverListBox.Enabled = false;
            this.serverListBox.FormattingEnabled = true;
            this.serverListBox.Location = new System.Drawing.Point(200, 209);
            this.serverListBox.Margin = new System.Windows.Forms.Padding(200);
            this.serverListBox.Name = "serverListBox";
            this.serverListBox.Size = new System.Drawing.Size(264, 264);
            this.serverListBox.TabIndex = 4;
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chatPanel);
            this.Controls.Add(this.playerPanel);
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Lobby";
            this.Text = "Lobby";
            this.playerPanel.ResumeLayout(false);
            this.chatPanel.ResumeLayout(false);
            this.chatPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox playerListBox;
        private System.Windows.Forms.TextBox chatTextBox;
        private System.Windows.Forms.ListBox chatListBox;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Panel playerPanel;
        private System.Windows.Forms.Panel chatPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox serverListBox;
    }
}