namespace HTO
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
            this.userID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.techPool = new System.Windows.Forms.RadioButton();
            this.generalPoool = new System.Windows.Forms.RadioButton();
            this.extraPool = new System.Windows.Forms.RadioButton();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.questionCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.questionCountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userID
            // 
            this.userID.Location = new System.Drawing.Point(12, 25);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(135, 20);
            this.userID.TabIndex = 0;
            this.userID.Text = "nrnoble@hotmail.com";
            this.userID.TextChanged += new System.EventHandler(this.userID_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "User:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(12, 64);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(135, 20);
            this.password.TabIndex = 2;
            this.password.Text = "J$p1ter2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // techPool
            // 
            this.techPool.AutoSize = true;
            this.techPool.Checked = true;
            this.techPool.Location = new System.Drawing.Point(12, 101);
            this.techPool.Name = "techPool";
            this.techPool.Size = new System.Drawing.Size(50, 17);
            this.techPool.TabIndex = 4;
            this.techPool.TabStop = true;
            this.techPool.Text = "Tech";
            this.techPool.UseVisualStyleBackColor = true;
            this.techPool.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // generalPoool
            // 
            this.generalPoool.AutoSize = true;
            this.generalPoool.Location = new System.Drawing.Point(12, 124);
            this.generalPoool.Name = "generalPoool";
            this.generalPoool.Size = new System.Drawing.Size(62, 17);
            this.generalPoool.TabIndex = 4;
            this.generalPoool.Text = "General";
            this.generalPoool.UseVisualStyleBackColor = true;
            // 
            // extraPool
            // 
            this.extraPool.AutoSize = true;
            this.extraPool.Location = new System.Drawing.Point(12, 147);
            this.extraPool.Name = "extraPool";
            this.extraPool.Size = new System.Drawing.Size(49, 17);
            this.extraPool.TabIndex = 4;
            this.extraPool.Text = "Extra";
            this.extraPool.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(205, 206);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Go";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(296, 206);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 6;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Question Count";
            // 
            // questionCount
            // 
            this.questionCount.Location = new System.Drawing.Point(12, 199);
            this.questionCount.Name = "questionCount";
            this.questionCount.Size = new System.Drawing.Size(135, 20);
            this.questionCount.TabIndex = 8;
            this.questionCount.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Answering Question:";
            // 
            // questionCountLabel
            // 
            this.questionCountLabel.AutoSize = true;
            this.questionCountLabel.Location = new System.Drawing.Point(276, 25);
            this.questionCountLabel.Name = "questionCountLabel";
            this.questionCountLabel.Size = new System.Drawing.Size(13, 13);
            this.questionCountLabel.TabIndex = 10;
            this.questionCountLabel.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 239);
            this.Controls.Add(this.questionCountLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.questionCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.extraPool);
            this.Controls.Add(this.generalPoool);
            this.Controls.Add(this.techPool);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userID);
            this.MaximumSize = new System.Drawing.Size(393, 278);
            this.MinimumSize = new System.Drawing.Size(393, 278);
            this.Name = "MainForm";
            this.Opacity = 0.8D;
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
        private System.Windows.Forms.RadioButton techPool;
        private System.Windows.Forms.RadioButton generalPoool;
        private System.Windows.Forms.RadioButton extraPool;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox questionCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label questionCountLabel;
    }
}

