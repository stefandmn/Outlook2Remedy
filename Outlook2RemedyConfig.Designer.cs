namespace OutlookTools
{
    partial class Outlook2RemedyConfig
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Outlook2RemedyConfig));
			this.doSave = new System.Windows.Forms.Button();
			this.ebWebServiceURL = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cbOpenInBrowser = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ebEventCode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ebAppConsoleURL = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbInsertInSubject = new System.Windows.Forms.CheckBox();
			this.cbBackupMail = new System.Windows.Forms.CheckBox();
			this.cbReadMail = new System.Windows.Forms.CheckBox();
			this.cbRemoveMail = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.ebPassword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.ebUsername = new System.Windows.Forms.TextBox();
			this.cbEmailAttachment = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// doSave
			// 
			this.doSave.Location = new System.Drawing.Point(612, 19);
			this.doSave.Name = "doSave";
			this.doSave.Size = new System.Drawing.Size(75, 23);
			this.doSave.TabIndex = 0;
			this.doSave.Text = "Save";
			this.doSave.UseVisualStyleBackColor = true;
			this.doSave.Click += new System.EventHandler(this.onSaveClick);
			// 
			// ebWebServiceURL
			// 
			this.ebWebServiceURL.Location = new System.Drawing.Point(143, 19);
			this.ebWebServiceURL.Name = "ebWebServiceURL";
			this.ebWebServiceURL.Size = new System.Drawing.Size(436, 20);
			this.ebWebServiceURL.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Web service base URL";
			// 
			// cbOpenInBrowser
			// 
			this.cbOpenInBrowser.AutoSize = true;
			this.cbOpenInBrowser.Location = new System.Drawing.Point(16, 47);
			this.cbOpenInBrowser.Name = "cbOpenInBrowser";
			this.cbOpenInBrowser.Size = new System.Drawing.Size(234, 17);
			this.cbOpenInBrowser.TabIndex = 8;
			this.cbOpenInBrowser.Text = "Open ticket in web browser after conversion";
			this.cbOpenInBrowser.UseVisualStyleBackColor = true;
			this.cbOpenInBrowser.CheckedChanged += new System.EventHandler(this.cbOpenInBrowser_CheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(119, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Conversion Event Code";
			// 
			// ebEventCode
			// 
			this.ebEventCode.Location = new System.Drawing.Point(143, 45);
			this.ebEventCode.Name = "ebEventCode";
			this.ebEventCode.Size = new System.Drawing.Size(85, 20);
			this.ebEventCode.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(124, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Application console URL";
			// 
			// ebAppConsoleURL
			// 
			this.ebAppConsoleURL.Location = new System.Drawing.Point(143, 70);
			this.ebAppConsoleURL.Name = "ebAppConsoleURL";
			this.ebAppConsoleURL.Size = new System.Drawing.Size(436, 20);
			this.ebAppConsoleURL.TabIndex = 9;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbInsertInSubject);
			this.groupBox1.Controls.Add(this.cbBackupMail);
			this.groupBox1.Controls.Add(this.cbReadMail);
			this.groupBox1.Controls.Add(this.cbRemoveMail);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.ebAppConsoleURL);
			this.groupBox1.Controls.Add(this.cbOpenInBrowser);
			this.groupBox1.Location = new System.Drawing.Point(12, 132);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(594, 100);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Post Execution";
			// 
			// cbInsertInSubject
			// 
			this.cbInsertInSubject.AutoSize = true;
			this.cbInsertInSubject.Location = new System.Drawing.Point(356, 22);
			this.cbInsertInSubject.Name = "cbInsertInSubject";
			this.cbInsertInSubject.Size = new System.Drawing.Size(149, 17);
			this.cbInsertInSubject.TabIndex = 10;
			this.cbInsertInSubject.Text = "Insert Ticket ID in Subject";
			this.cbInsertInSubject.UseVisualStyleBackColor = true;
			// 
			// cbBackupMail
			// 
			this.cbBackupMail.AutoSize = true;
			this.cbBackupMail.Location = new System.Drawing.Point(200, 22);
			this.cbBackupMail.Name = "cbBackupMail";
			this.cbBackupMail.Size = new System.Drawing.Size(152, 17);
			this.cbBackupMail.TabIndex = 7;
			this.cbBackupMail.Text = "Copy Mail in Backup folder";
			this.cbBackupMail.UseVisualStyleBackColor = true;
			// 
			// cbReadMail
			// 
			this.cbReadMail.AutoSize = true;
			this.cbReadMail.Location = new System.Drawing.Point(16, 22);
			this.cbReadMail.Name = "cbReadMail";
			this.cbReadMail.Size = new System.Drawing.Size(93, 17);
			this.cbReadMail.TabIndex = 5;
			this.cbReadMail.Text = "Mark as Read";
			this.cbReadMail.UseVisualStyleBackColor = true;
			// 
			// cbRemoveMail
			// 
			this.cbRemoveMail.AutoSize = true;
			this.cbRemoveMail.Location = new System.Drawing.Point(115, 22);
			this.cbRemoveMail.Name = "cbRemoveMail";
			this.cbRemoveMail.Size = new System.Drawing.Size(79, 17);
			this.cbRemoveMail.TabIndex = 6;
			this.cbRemoveMail.Text = "Delete Mail";
			this.cbRemoveMail.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cbEmailAttachment);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.ebPassword);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.ebUsername);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.ebWebServiceURL);
			this.groupBox2.Controls.Add(this.ebEventCode);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(594, 101);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Conversion Process";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(368, 74);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Remedy Password";
			// 
			// ebPassword
			// 
			this.ebPassword.Location = new System.Drawing.Point(471, 71);
			this.ebPassword.Name = "ebPassword";
			this.ebPassword.Size = new System.Drawing.Size(108, 20);
			this.ebPassword.TabIndex = 4;
			this.ebPassword.UseSystemPasswordChar = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(368, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(97, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Remedy Username";
			// 
			// ebUsername
			// 
			this.ebUsername.Location = new System.Drawing.Point(471, 45);
			this.ebUsername.Name = "ebUsername";
			this.ebUsername.Size = new System.Drawing.Size(108, 20);
			this.ebUsername.TabIndex = 3;
			// 
			// cbEmailAttachment
			// 
			this.cbEmailAttachment.AutoSize = true;
			this.cbEmailAttachment.Location = new System.Drawing.Point(16, 75);
			this.cbEmailAttachment.Name = "cbEmailAttachment";
			this.cbEmailAttachment.Size = new System.Drawing.Size(207, 17);
			this.cbEmailAttachment.TabIndex = 11;
			this.cbEmailAttachment.Text = "Transfer selected Email as Attachment";
			this.cbEmailAttachment.UseVisualStyleBackColor = true;
			// 
			// Outlook2RemedyConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(691, 243);
			this.Controls.Add(this.doSave);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Outlook2RemedyConfig";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Outlook2Remedy Settings";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button doSave;
        private System.Windows.Forms.TextBox ebWebServiceURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbOpenInBrowser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ebEventCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ebAppConsoleURL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbRemoveMail;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox ebPassword;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox ebUsername;
		private System.Windows.Forms.CheckBox cbReadMail;
		private System.Windows.Forms.CheckBox cbBackupMail;
		private System.Windows.Forms.CheckBox cbInsertInSubject;
		private System.Windows.Forms.CheckBox cbEmailAttachment;
    }
}