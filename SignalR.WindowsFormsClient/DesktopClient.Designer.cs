namespace SignalR.WindowsFormsClient
{
    partial class FrmChat
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
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.btn_SendToAll = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.messagesList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.txt_sender = new System.Windows.Forms.TextBox();
            this.btn_JoinGroup = new System.Windows.Forms.Button();
            this.btn_LeaveGroup = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_GroupName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_SendToGroup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Location = new System.Drawing.Point(144, 101);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(75, 23);
            this.btn_Disconnect.TabIndex = 13;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            this.btn_Disconnect.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // btn_SendToAll
            // 
            this.btn_SendToAll.Location = new System.Drawing.Point(438, 101);
            this.btn_SendToAll.Name = "btn_SendToAll";
            this.btn_SendToAll.Size = new System.Drawing.Size(75, 23);
            this.btn_SendToAll.TabIndex = 12;
            this.btn_SendToAll.Text = "Send To All";
            this.btn_SendToAll.UseVisualStyleBackColor = true;
            this.btn_SendToAll.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTextBox.Location = new System.Drawing.Point(63, 75);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(617, 20);
            this.messageTextBox.TabIndex = 11;
            this.messageTextBox.Text = "iam desktop client";
            this.messageTextBox.Enter += new System.EventHandler(this.messageTextBox_Enter);
            // 
            // messagesList
            // 
            this.messagesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.messagesList.FormattingEnabled = true;
            this.messagesList.Location = new System.Drawing.Point(12, 130);
            this.messagesList.Name = "messagesList";
            this.messagesList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.messagesList.Size = new System.Drawing.Size(668, 290);
            this.messagesList.TabIndex = 10;
            this.messagesList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.messagesList_DrawItem);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Address:";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(63, 101);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 8;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // addressTextBox
            // 
            this.addressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addressTextBox.Location = new System.Drawing.Point(63, 12);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(617, 20);
            this.addressTextBox.TabIndex = 7;
            this.addressTextBox.Text = "https://localhost:7032/hubs/chatHub\r\n";
            this.addressTextBox.Enter += new System.EventHandler(this.addressTextBox_Enter);
            // 
            // txt_sender
            // 
            this.txt_sender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_sender.Location = new System.Drawing.Point(63, 41);
            this.txt_sender.Name = "txt_sender";
            this.txt_sender.Size = new System.Drawing.Size(287, 20);
            this.txt_sender.TabIndex = 14;
            this.txt_sender.Text = "Desktop";
            // 
            // btn_JoinGroup
            // 
            this.btn_JoinGroup.Location = new System.Drawing.Point(248, 101);
            this.btn_JoinGroup.Name = "btn_JoinGroup";
            this.btn_JoinGroup.Size = new System.Drawing.Size(82, 23);
            this.btn_JoinGroup.TabIndex = 15;
            this.btn_JoinGroup.Text = "Join Group";
            this.btn_JoinGroup.UseVisualStyleBackColor = true;
            this.btn_JoinGroup.Click += new System.EventHandler(this.btn_JoinGroup_Click);
            // 
            // btn_LeaveGroup
            // 
            this.btn_LeaveGroup.Location = new System.Drawing.Point(336, 101);
            this.btn_LeaveGroup.Name = "btn_LeaveGroup";
            this.btn_LeaveGroup.Size = new System.Drawing.Size(84, 23);
            this.btn_LeaveGroup.TabIndex = 15;
            this.btn_LeaveGroup.Text = "Leave Group";
            this.btn_LeaveGroup.UseVisualStyleBackColor = true;
            this.btn_LeaveGroup.Click += new System.EventHandler(this.btn_LeaveGroup_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Group:";
            // 
            // txt_GroupName
            // 
            this.txt_GroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_GroupName.Location = new System.Drawing.Point(402, 38);
            this.txt_GroupName.Name = "txt_GroupName";
            this.txt_GroupName.Size = new System.Drawing.Size(278, 20);
            this.txt_GroupName.TabIndex = 16;
            this.txt_GroupName.Text = "admin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Sender:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Message:";
            // 
            // btn_SendToGroup
            // 
            this.btn_SendToGroup.Location = new System.Drawing.Point(519, 101);
            this.btn_SendToGroup.Name = "btn_SendToGroup";
            this.btn_SendToGroup.Size = new System.Drawing.Size(92, 23);
            this.btn_SendToGroup.TabIndex = 20;
            this.btn_SendToGroup.Text = "Send To Group";
            this.btn_SendToGroup.UseVisualStyleBackColor = true;
            this.btn_SendToGroup.Click += new System.EventHandler(this.btn_SendToGroup_Click);
            // 
            // FrmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 438);
            this.Controls.Add(this.btn_SendToGroup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_GroupName);
            this.Controls.Add(this.btn_LeaveGroup);
            this.Controls.Add(this.btn_JoinGroup);
            this.Controls.Add(this.txt_sender);
            this.Controls.Add(this.btn_Disconnect);
            this.Controls.Add(this.btn_SendToAll);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.messagesList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.addressTextBox);
            this.Name = "FrmChat";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.frmChat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.Button btn_SendToAll;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.ListBox messagesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.TextBox txt_sender;
        private System.Windows.Forms.Button btn_JoinGroup;
        private System.Windows.Forms.Button btn_LeaveGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_GroupName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_SendToGroup;
    }
}

