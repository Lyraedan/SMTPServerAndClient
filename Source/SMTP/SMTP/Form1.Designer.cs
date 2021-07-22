/*
 
    Author: Zel (Luke Rapkin)
    Student ID: 100527733
    Created: 01/11/2020
    Last Modified: 01/11/2020

 */

namespace SMTP
{
    partial class Form1
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
            this.ConnectedStatus = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.BtnRegister = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RegisterPassword = new System.Windows.Forms.TextBox();
            this.RegisterEmail = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.LoginButton = new System.Windows.Forms.Button();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.EmailLabel = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SendButton = new System.Windows.Forms.Button();
            this.BodyLabel = new System.Windows.Forms.Label();
            this.CCNote = new System.Windows.Forms.Label();
            this.ToNote = new System.Windows.Forms.Label();
            this.SubjectLabel = new System.Windows.Forms.Label();
            this.CCLabel = new System.Windows.Forms.Label();
            this.ToLabel = new System.Windows.Forms.Label();
            this.SubjectBox = new System.Windows.Forms.TextBox();
            this.CCBox = new System.Windows.Forms.TextBox();
            this.ToBox = new System.Windows.Forms.TextBox();
            this.BodyBox = new System.Windows.Forms.RichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.BtnReply = new System.Windows.Forms.Button();
            this.BtnForward = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SubjectDisplay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.InboxCC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.InboxFrom = new System.Windows.Forms.TextBox();
            this.InboxTo = new System.Windows.Forms.TextBox();
            this.BodyDisplay = new System.Windows.Forms.RichTextBox();
            this.InboxDisplay = new System.Windows.Forms.Panel();
            this.ServerSettings = new System.Windows.Forms.TabPage();
            this.SSLEnabled = new System.Windows.Forms.CheckBox();
            this.PortsList = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.PortLabel = new System.Windows.Forms.Label();
            this.ServerLabel = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.TextBox();
            this.Server = new System.Windows.Forms.TextBox();
            this.Page = new System.Windows.Forms.TabControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage3.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.ServerSettings.SuspendLayout();
            this.Page.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectedStatus
            // 
            this.ConnectedStatus.AutoSize = true;
            this.ConnectedStatus.Location = new System.Drawing.Point(1, 391);
            this.ConnectedStatus.Name = "ConnectedStatus";
            this.ConnectedStatus.Size = new System.Drawing.Size(117, 13);
            this.ConnectedStatus.TabIndex = 10;
            this.ConnectedStatus.Text = "Status: Not connected!";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(464, 362);
            this.tabPage3.TabIndex = 6;
            this.tabPage3.Text = "About";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(133, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 26);
            this.label7.TabIndex = 0;
            this.label7.Text = "SMTP Client written by Luke Rapkin\nStudent Number: 100527733";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.BtnRegister);
            this.tabPage6.Controls.Add(this.label1);
            this.tabPage6.Controls.Add(this.label2);
            this.tabPage6.Controls.Add(this.RegisterPassword);
            this.tabPage6.Controls.Add(this.RegisterEmail);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(464, 362);
            this.tabPage6.TabIndex = 8;
            this.tabPage6.Text = "Register";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // BtnRegister
            // 
            this.BtnRegister.Location = new System.Drawing.Point(291, 75);
            this.BtnRegister.Name = "BtnRegister";
            this.BtnRegister.Size = new System.Drawing.Size(75, 23);
            this.BtnRegister.TabIndex = 13;
            this.BtnRegister.Text = "Register";
            this.BtnRegister.UseVisualStyleBackColor = true;
            this.BtnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Email";
            // 
            // RegisterPassword
            // 
            this.RegisterPassword.Location = new System.Drawing.Point(138, 48);
            this.RegisterPassword.Name = "RegisterPassword";
            this.RegisterPassword.PasswordChar = '*';
            this.RegisterPassword.Size = new System.Drawing.Size(229, 20);
            this.RegisterPassword.TabIndex = 10;
            // 
            // RegisterEmail
            // 
            this.RegisterEmail.Location = new System.Drawing.Point(138, 19);
            this.RegisterEmail.Name = "RegisterEmail";
            this.RegisterEmail.Size = new System.Drawing.Size(229, 20);
            this.RegisterEmail.TabIndex = 9;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.LoginButton);
            this.tabPage2.Controls.Add(this.PasswordLabel);
            this.tabPage2.Controls.Add(this.EmailLabel);
            this.tabPage2.Controls.Add(this.Password);
            this.tabPage2.Controls.Add(this.Email);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(464, 362);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Login";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(283, 78);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 8;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(71, 54);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.PasswordLabel.TabIndex = 7;
            this.PasswordLabel.Text = "Password";
            // 
            // EmailLabel
            // 
            this.EmailLabel.AutoSize = true;
            this.EmailLabel.Location = new System.Drawing.Point(92, 25);
            this.EmailLabel.Name = "EmailLabel";
            this.EmailLabel.Size = new System.Drawing.Size(32, 13);
            this.EmailLabel.TabIndex = 6;
            this.EmailLabel.Text = "Email";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(130, 51);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(229, 20);
            this.Password.TabIndex = 5;
            // 
            // Email
            // 
            this.Email.Location = new System.Drawing.Point(130, 22);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(229, 20);
            this.Email.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.SendButton);
            this.tabPage1.Controls.Add(this.BodyLabel);
            this.tabPage1.Controls.Add(this.CCNote);
            this.tabPage1.Controls.Add(this.ToNote);
            this.tabPage1.Controls.Add(this.SubjectLabel);
            this.tabPage1.Controls.Add(this.CCLabel);
            this.tabPage1.Controls.Add(this.ToLabel);
            this.tabPage1.Controls.Add(this.SubjectBox);
            this.tabPage1.Controls.Add(this.CCBox);
            this.tabPage1.Controls.Add(this.ToBox);
            this.tabPage1.Controls.Add(this.BodyBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(464, 362);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Send";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(381, 332);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 11;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // BodyLabel
            // 
            this.BodyLabel.AutoSize = true;
            this.BodyLabel.Location = new System.Drawing.Point(8, 107);
            this.BodyLabel.Name = "BodyLabel";
            this.BodyLabel.Size = new System.Drawing.Size(31, 13);
            this.BodyLabel.TabIndex = 9;
            this.BodyLabel.Text = "Body";
            // 
            // CCNote
            // 
            this.CCNote.AutoSize = true;
            this.CCNote.Location = new System.Drawing.Point(31, 44);
            this.CCNote.Name = "CCNote";
            this.CCNote.Size = new System.Drawing.Size(264, 13);
            this.CCNote.TabIndex = 8;
            this.CCNote.Text = "Seperate recipients with commas \"<emailA>,<emailB>\"";
            // 
            // ToNote
            // 
            this.ToNote.AutoSize = true;
            this.ToNote.Location = new System.Drawing.Point(31, 3);
            this.ToNote.Name = "ToNote";
            this.ToNote.Size = new System.Drawing.Size(264, 13);
            this.ToNote.TabIndex = 7;
            this.ToNote.Text = "Seperate recipients with commas \"<emailA>,<emailB>\"";
            // 
            // SubjectLabel
            // 
            this.SubjectLabel.AutoSize = true;
            this.SubjectLabel.Location = new System.Drawing.Point(5, 90);
            this.SubjectLabel.Name = "SubjectLabel";
            this.SubjectLabel.Size = new System.Drawing.Size(43, 13);
            this.SubjectLabel.TabIndex = 6;
            this.SubjectLabel.Text = "Subject";
            // 
            // CCLabel
            // 
            this.CCLabel.AutoSize = true;
            this.CCLabel.Location = new System.Drawing.Point(8, 64);
            this.CCLabel.Name = "CCLabel";
            this.CCLabel.Size = new System.Drawing.Size(21, 13);
            this.CCLabel.TabIndex = 5;
            this.CCLabel.Text = "CC";
            // 
            // ToLabel
            // 
            this.ToLabel.AutoSize = true;
            this.ToLabel.Location = new System.Drawing.Point(8, 25);
            this.ToLabel.Name = "ToLabel";
            this.ToLabel.Size = new System.Drawing.Size(20, 13);
            this.ToLabel.TabIndex = 4;
            this.ToLabel.Text = "To";
            // 
            // SubjectBox
            // 
            this.SubjectBox.Location = new System.Drawing.Point(49, 87);
            this.SubjectBox.Name = "SubjectBox";
            this.SubjectBox.Size = new System.Drawing.Size(407, 20);
            this.SubjectBox.TabIndex = 3;
            // 
            // CCBox
            // 
            this.CCBox.Enabled = false;
            this.CCBox.Location = new System.Drawing.Point(34, 61);
            this.CCBox.Name = "CCBox";
            this.CCBox.Size = new System.Drawing.Size(422, 20);
            this.CCBox.TabIndex = 2;
            // 
            // ToBox
            // 
            this.ToBox.Location = new System.Drawing.Point(34, 22);
            this.ToBox.Name = "ToBox";
            this.ToBox.Size = new System.Drawing.Size(422, 20);
            this.ToBox.TabIndex = 1;
            // 
            // BodyBox
            // 
            this.BodyBox.Location = new System.Drawing.Point(8, 123);
            this.BodyBox.Name = "BodyBox";
            this.BodyBox.Size = new System.Drawing.Size(448, 203);
            this.BodyBox.TabIndex = 0;
            this.BodyBox.Text = "";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.BtnReply);
            this.tabPage5.Controls.Add(this.BtnForward);
            this.tabPage5.Controls.Add(this.BtnDelete);
            this.tabPage5.Controls.Add(this.BtnRefresh);
            this.tabPage5.Controls.Add(this.label6);
            this.tabPage5.Controls.Add(this.SubjectDisplay);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.InboxCC);
            this.tabPage5.Controls.Add(this.label4);
            this.tabPage5.Controls.Add(this.label3);
            this.tabPage5.Controls.Add(this.InboxFrom);
            this.tabPage5.Controls.Add(this.InboxTo);
            this.tabPage5.Controls.Add(this.BodyDisplay);
            this.tabPage5.Controls.Add(this.InboxDisplay);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(464, 362);
            this.tabPage5.TabIndex = 7;
            this.tabPage5.Text = "Inbox";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // BtnReply
            // 
            this.BtnReply.Enabled = false;
            this.BtnReply.Location = new System.Drawing.Point(368, 327);
            this.BtnReply.Name = "BtnReply";
            this.BtnReply.Size = new System.Drawing.Size(75, 23);
            this.BtnReply.TabIndex = 13;
            this.BtnReply.Text = "Reply";
            this.BtnReply.UseVisualStyleBackColor = true;
            this.BtnReply.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnForward
            // 
            this.BtnForward.Enabled = false;
            this.BtnForward.Location = new System.Drawing.Point(286, 327);
            this.BtnForward.Name = "BtnForward";
            this.BtnForward.Size = new System.Drawing.Size(75, 23);
            this.BtnForward.TabIndex = 12;
            this.BtnForward.Text = "Forward";
            this.BtnForward.UseVisualStyleBackColor = true;
            this.BtnForward.Click += new System.EventHandler(this.BtnForward_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(204, 326);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 11;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnReply_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(47, 7);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(75, 23);
            this.BtnRefresh.TabIndex = 10;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Inbox";
            // 
            // SubjectDisplay
            // 
            this.SubjectDisplay.Enabled = false;
            this.SubjectDisplay.Location = new System.Drawing.Point(201, 102);
            this.SubjectDisplay.Name = "SubjectDisplay";
            this.SubjectDisplay.Size = new System.Drawing.Size(254, 20);
            this.SubjectDisplay.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(204, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "CC";
            // 
            // InboxCC
            // 
            this.InboxCC.Enabled = false;
            this.InboxCC.Location = new System.Drawing.Point(237, 70);
            this.InboxCC.Name = "InboxCC";
            this.InboxCC.Size = new System.Drawing.Size(218, 20);
            this.InboxCC.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "From";
            // 
            // InboxFrom
            // 
            this.InboxFrom.Enabled = false;
            this.InboxFrom.Location = new System.Drawing.Point(237, 18);
            this.InboxFrom.Name = "InboxFrom";
            this.InboxFrom.Size = new System.Drawing.Size(218, 20);
            this.InboxFrom.TabIndex = 4;
            // 
            // InboxTo
            // 
            this.InboxTo.Enabled = false;
            this.InboxTo.Location = new System.Drawing.Point(237, 44);
            this.InboxTo.Name = "InboxTo";
            this.InboxTo.Size = new System.Drawing.Size(218, 20);
            this.InboxTo.TabIndex = 2;
            // 
            // BodyDisplay
            // 
            this.BodyDisplay.Enabled = false;
            this.BodyDisplay.Location = new System.Drawing.Point(200, 128);
            this.BodyDisplay.Name = "BodyDisplay";
            this.BodyDisplay.Size = new System.Drawing.Size(255, 192);
            this.BodyDisplay.TabIndex = 1;
            this.BodyDisplay.Text = "";
            // 
            // InboxDisplay
            // 
            this.InboxDisplay.AutoScroll = true;
            this.InboxDisplay.BackColor = System.Drawing.Color.Silver;
            this.InboxDisplay.Location = new System.Drawing.Point(9, 36);
            this.InboxDisplay.Name = "InboxDisplay";
            this.InboxDisplay.Size = new System.Drawing.Size(185, 320);
            this.InboxDisplay.TabIndex = 0;
            // 
            // ServerSettings
            // 
            this.ServerSettings.Controls.Add(this.SSLEnabled);
            this.ServerSettings.Controls.Add(this.PortsList);
            this.ServerSettings.Controls.Add(this.ConnectButton);
            this.ServerSettings.Controls.Add(this.PortLabel);
            this.ServerSettings.Controls.Add(this.ServerLabel);
            this.ServerSettings.Controls.Add(this.Port);
            this.ServerSettings.Controls.Add(this.Server);
            this.ServerSettings.Location = new System.Drawing.Point(4, 22);
            this.ServerSettings.Name = "ServerSettings";
            this.ServerSettings.Padding = new System.Windows.Forms.Padding(3);
            this.ServerSettings.Size = new System.Drawing.Size(464, 362);
            this.ServerSettings.TabIndex = 3;
            this.ServerSettings.Text = "Server";
            this.ServerSettings.UseVisualStyleBackColor = true;
            // 
            // SSLEnabled
            // 
            this.SSLEnabled.AutoSize = true;
            this.SSLEnabled.Location = new System.Drawing.Point(199, 98);
            this.SSLEnabled.Name = "SSLEnabled";
            this.SSLEnabled.Size = new System.Drawing.Size(88, 17);
            this.SSLEnabled.TabIndex = 11;
            this.SSLEnabled.Text = "SSL Enabled";
            this.SSLEnabled.UseVisualStyleBackColor = true;
            // 
            // PortsList
            // 
            this.PortsList.AutoSize = true;
            this.PortsList.Location = new System.Drawing.Point(136, 52);
            this.PortsList.Name = "PortsList";
            this.PortsList.Size = new System.Drawing.Size(73, 13);
            this.PortsList.TabIndex = 10;
            this.PortsList.Text = "(587, 465, 25)";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(293, 94);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 9;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(73, 71);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(60, 13);
            this.PortLabel.TabIndex = 3;
            this.PortLabel.Text = "Server Port";
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(54, 32);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(79, 13);
            this.ServerLabel.TabIndex = 2;
            this.ServerLabel.Text = "Server Address";
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(139, 68);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(229, 20);
            this.Port.TabIndex = 1;
            this.Port.Text = "25";
            // 
            // Server
            // 
            this.Server.Location = new System.Drawing.Point(139, 29);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(229, 20);
            this.Server.TabIndex = 0;
            this.Server.Text = "127.0.0.1";
            // 
            // Page
            // 
            this.Page.Controls.Add(this.ServerSettings);
            this.Page.Controls.Add(this.tabPage5);
            this.Page.Controls.Add(this.tabPage1);
            this.Page.Controls.Add(this.tabPage2);
            this.Page.Controls.Add(this.tabPage6);
            this.Page.Controls.Add(this.tabPage3);
            this.Page.Dock = System.Windows.Forms.DockStyle.Top;
            this.Page.Location = new System.Drawing.Point(0, 0);
            this.Page.Name = "Page";
            this.Page.SelectedIndex = 0;
            this.Page.Size = new System.Drawing.Size(472, 388);
            this.Page.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 410);
            this.Controls.Add(this.Page);
            this.Controls.Add(this.ConnectedStatus);
            this.Name = "Form1";
            this.Text = "SMTP Client - By Luke Rapkin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ServerSettings.ResumeLayout(false);
            this.ServerSettings.PerformLayout();
            this.Page.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ConnectedStatus;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label EmailLabel;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label BodyLabel;
        private System.Windows.Forms.Label CCNote;
        private System.Windows.Forms.Label ToNote;
        private System.Windows.Forms.Label SubjectLabel;
        private System.Windows.Forms.Label CCLabel;
        private System.Windows.Forms.Label ToLabel;
        private System.Windows.Forms.TextBox SubjectBox;
        private System.Windows.Forms.TextBox CCBox;
        private System.Windows.Forms.TextBox ToBox;
        private System.Windows.Forms.RichTextBox BodyBox;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage ServerSettings;
        private System.Windows.Forms.CheckBox SSLEnabled;
        private System.Windows.Forms.Label PortsList;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.TextBox Server;
        private System.Windows.Forms.TabControl Page;
        private System.Windows.Forms.Button BtnRegister;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RegisterPassword;
        private System.Windows.Forms.TextBox RegisterEmail;
        private System.Windows.Forms.Panel InboxDisplay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox InboxFrom;
        private System.Windows.Forms.TextBox InboxTo;
        private System.Windows.Forms.RichTextBox BodyDisplay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox InboxCC;
        private System.Windows.Forms.TextBox SubjectDisplay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnReply;
        private System.Windows.Forms.Button BtnForward;
        private System.Windows.Forms.Button BtnDelete;
    }
}

