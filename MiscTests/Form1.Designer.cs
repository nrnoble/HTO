﻿namespace HTO
{
    partial class MainForm
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
            this.userID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.studyButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.maxQuestionCount = new System.Windows.Forms.TextBox();
            this.AnswerQuestionLabel = new System.Windows.Forms.Label();
            this.questionCountLabel = new System.Windows.Forms.Label();
            this.answerButton = new System.Windows.Forms.Button();
            this.Testbtn = new System.Windows.Forms.Button();
            this.PracticeExamBtn = new System.Windows.Forms.Button();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.techPool = new System.Windows.Forms.RadioButton();
            this.generalPoool = new System.Windows.Forms.RadioButton();
            this.extraPool = new System.Windows.Forms.RadioButton();
            this.LoginTip = new System.Windows.Forms.ToolTip(this.components);
            this.BtnTest = new System.Windows.Forms.Button();
            this.ShowAnswer = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // userID
            // 
            this.userID.Location = new System.Drawing.Point(153, 26);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(135, 20);
            this.userID.TabIndex = 0;
            this.userID.Text = "nrnoble@hotmail.com";
            this.userID.TextChanged += new System.EventHandler(this.userID_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "User:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(153, 65);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(135, 20);
            this.password.TabIndex = 2;
            this.password.Text = "J$p1ter2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // studyButton
            // 
            this.studyButton.Enabled = false;
            this.studyButton.Location = new System.Drawing.Point(12, 125);
            this.studyButton.Name = "studyButton";
            this.studyButton.Size = new System.Drawing.Size(107, 23);
            this.studyButton.TabIndex = 5;
            this.studyButton.Text = "Auto Study";
            this.LoginTip.SetToolTip(this.studyButton, "Answer a series of study questions back to back");
            this.studyButton.UseVisualStyleBackColor = true;
            this.studyButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(299, 244);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(96, 23);
            this.stopButton.TabIndex = 6;
            this.stopButton.Text = "Stop Automation";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.UseWaitCursor = true;
            this.stopButton.Visible = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Auto Study Questions";
            // 
            // maxQuestionCount
            // 
            this.maxQuestionCount.Location = new System.Drawing.Point(154, 128);
            this.maxQuestionCount.Name = "maxQuestionCount";
            this.maxQuestionCount.Size = new System.Drawing.Size(135, 20);
            this.maxQuestionCount.TabIndex = 8;
            this.maxQuestionCount.Text = "10";
            this.LoginTip.SetToolTip(this.maxQuestionCount, "The number of questions that will be asnwered when selecting Auto Study");
            // 
            // AnswerQuestionLabel
            // 
            this.AnswerQuestionLabel.AutoSize = true;
            this.AnswerQuestionLabel.Location = new System.Drawing.Point(154, 155);
            this.AnswerQuestionLabel.Name = "AnswerQuestionLabel";
            this.AnswerQuestionLabel.Size = new System.Drawing.Size(107, 13);
            this.AnswerQuestionLabel.TabIndex = 9;
            this.AnswerQuestionLabel.Text = "Questions Answered:";
            // 
            // questionCountLabel
            // 
            this.questionCountLabel.AutoSize = true;
            this.questionCountLabel.Location = new System.Drawing.Point(263, 155);
            this.questionCountLabel.Name = "questionCountLabel";
            this.questionCountLabel.Size = new System.Drawing.Size(13, 13);
            this.questionCountLabel.TabIndex = 10;
            this.questionCountLabel.Text = "0";
            // 
            // answerButton
            // 
            this.answerButton.Enabled = false;
            this.answerButton.Location = new System.Drawing.Point(12, 94);
            this.answerButton.Name = "answerButton";
            this.answerButton.Size = new System.Drawing.Size(107, 23);
            this.answerButton.TabIndex = 11;
            this.answerButton.Text = "Single Answer";
            this.LoginTip.SetToolTip(this.answerButton, "Answer the current question. Question must be currently be visible on page prior " +
        "to selecting button");
            this.answerButton.UseVisualStyleBackColor = true;
            this.answerButton.Click += new System.EventHandler(this.answerButton_Click);
            // 
            // Testbtn
            // 
            this.Testbtn.Enabled = false;
            this.Testbtn.Location = new System.Drawing.Point(13, 244);
            this.Testbtn.Name = "Testbtn";
            this.Testbtn.Size = new System.Drawing.Size(106, 23);
            this.Testbtn.TabIndex = 12;
            this.Testbtn.Text = "Test Menu Buttons";
            this.Testbtn.UseVisualStyleBackColor = true;
            this.Testbtn.Click += new System.EventHandler(this.Testbtn_Click);
            // 
            // PracticeExamBtn
            // 
            this.PracticeExamBtn.Enabled = false;
            this.PracticeExamBtn.Location = new System.Drawing.Point(13, 65);
            this.PracticeExamBtn.Name = "PracticeExamBtn";
            this.PracticeExamBtn.Size = new System.Drawing.Size(106, 23);
            this.PracticeExamBtn.TabIndex = 13;
            this.PracticeExamBtn.Text = "Practice Exam";
            this.LoginTip.SetToolTip(this.PracticeExamBtn, "Run a full practice exam of the currently selected Exam (Tech, General, Extra).");
            this.PracticeExamBtn.UseVisualStyleBackColor = true;
            this.PracticeExamBtn.Click += new System.EventHandler(this.PracticeExamBtn_Click);
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(12, 12);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(107, 23);
            this.LoginBtn.TabIndex = 14;
            this.LoginBtn.Text = "Login";
            this.LoginTip.SetToolTip(this.LoginBtn, "Will launch the Chrome Browser and login");
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // techPool
            // 
            this.techPool.AutoSize = true;
            this.techPool.Checked = true;
            this.techPool.Enabled = false;
            this.techPool.Location = new System.Drawing.Point(333, 14);
            this.techPool.Name = "techPool";
            this.techPool.Size = new System.Drawing.Size(50, 17);
            this.techPool.TabIndex = 4;
            this.techPool.TabStop = true;
            this.techPool.Text = "Tech";
            this.techPool.UseVisualStyleBackColor = true;
            this.techPool.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // generalPoool
            // 
            this.generalPoool.AutoSize = true;
            this.generalPoool.Enabled = false;
            this.generalPoool.Location = new System.Drawing.Point(333, 37);
            this.generalPoool.Name = "generalPoool";
            this.generalPoool.Size = new System.Drawing.Size(62, 17);
            this.generalPoool.TabIndex = 4;
            this.generalPoool.Text = "General";
            this.generalPoool.UseVisualStyleBackColor = true;
            this.generalPoool.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // extraPool
            // 
            this.extraPool.AutoSize = true;
            this.extraPool.Enabled = false;
            this.extraPool.Location = new System.Drawing.Point(333, 60);
            this.extraPool.Name = "extraPool";
            this.extraPool.Size = new System.Drawing.Size(49, 17);
            this.extraPool.TabIndex = 4;
            this.extraPool.Text = "Extra";
            this.extraPool.UseVisualStyleBackColor = true;
            this.extraPool.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // LoginTip
            // 
            this.LoginTip.ToolTipTitle = "Login";
            // 
            // BtnTest
            // 
            this.BtnTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnTest.Location = new System.Drawing.Point(288, 215);
            this.BtnTest.Name = "BtnTest";
            this.BtnTest.Size = new System.Drawing.Size(107, 23);
            this.BtnTest.TabIndex = 17;
            this.BtnTest.Text = "Test";
            this.LoginTip.SetToolTip(this.BtnTest, "Will launch the Chrome Browser and login");
            this.BtnTest.UseVisualStyleBackColor = true;
            this.BtnTest.Visible = false;
            this.BtnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // ShowAnswer
            // 
            this.ShowAnswer.Location = new System.Drawing.Point(12, 155);
            this.ShowAnswer.Name = "ShowAnswer";
            this.ShowAnswer.Size = new System.Drawing.Size(107, 23);
            this.ShowAnswer.TabIndex = 15;
            this.ShowAnswer.Text = "Show Answer";
            this.ShowAnswer.UseVisualStyleBackColor = true;
            this.ShowAnswer.Click += new System.EventHandler(this.ShowAnswer_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Research Answer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(154, 207);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 18;
            this.textBox1.Text = "5";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(156, 184);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(51, 17);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "delay";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(413, 279);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BtnTest);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ShowAnswer);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.PracticeExamBtn);
            this.Controls.Add(this.Testbtn);
            this.Controls.Add(this.answerButton);
            this.Controls.Add(this.questionCountLabel);
            this.Controls.Add(this.AnswerQuestionLabel);
            this.Controls.Add(this.maxQuestionCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.studyButton);
            this.Controls.Add(this.extraPool);
            this.Controls.Add(this.generalPoool);
            this.Controls.Add(this.techPool);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userID);
            this.Location = new System.Drawing.Point(2000, 500);
            this.MinimumSize = new System.Drawing.Size(393, 278);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HTO Automation";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox userID;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button studyButton;
        public System.Windows.Forms.Button stopButton;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox maxQuestionCount;
        public System.Windows.Forms.Label AnswerQuestionLabel;
        public System.Windows.Forms.Label questionCountLabel;
        public System.Windows.Forms.Button answerButton;
        private System.Windows.Forms.Button Testbtn;
        private System.Windows.Forms.Button PracticeExamBtn;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.RadioButton techPool;
        private System.Windows.Forms.RadioButton generalPoool;
        private System.Windows.Forms.RadioButton extraPool;
        private System.Windows.Forms.ToolTip LoginTip;
        private System.Windows.Forms.Button ShowAnswer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnTest;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
