using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace HTO
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //string str1 = "It will change depending on the resistor's temperature coefficient";

            //Console.WriteLine(str1);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            HTO.HTOAuto.ClickOnButton(HTOMenuButtons.MainMenu);
            HTO.HTOAuto.SelectMainTopics();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            HTO.HTOAuto.ClickOnButton(HTOMenuButtons.MainMenu);
            HTO.HTOAuto.ClickOnButton(HTOMenuButtons.Study);


            stopButton.Enabled = true;
            ButtonState(false);
            
            int qcount = 0;

            try
            {
                qcount = int.Parse(maxQuestionCount.Text.Trim());
            }
            catch (Exception)
            {
                ButtonState(true);
                stopButton.Enabled = false;
                return;
            }

       
            for (int i = 0; i < qcount; i++)
            {
                answerButton_Click(this, new EventArgs());
            }

            ButtonState(true);
            stopButton.Enabled = false;
        }


        private void userID_TextChanged(object sender, EventArgs e)
        {
            // do nothing
        }


        private void stopButton_Click(object sender, EventArgs e)
        {
            //stopButton.Enabled = false;
            //studyButton.Enabled = true;
            // this.Refresh();
        }


        private void answerButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = true;
            ButtonState(false);
            bool status = false;

            // TODO: This section should be a function
            { 
                try
                {
                    status = HTO.HTOAuto.AnswerCurrentQuestion();
                    if (status == true)
                    {
                        Console.WriteLine("Successfully answered question");
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine("Exception Thrown while answering question. Error: " + err.Message);
                    status = false;
                }
           
                // Unable to answer question, so try clicking on alternate buttons.
                if (status == false)
                {   
                    // try clicking OK button
                    var okbuttonStatus = HTOAuto.clickOnElement("okbutton");

                    // Now try answering question again.
                    if (okbuttonStatus == true)
                    {
                       status = HTO.HTOAuto.AnswerCurrentQuestion();
                    }

                    // If there is no OK button, try Skip button.
                    if (okbuttonStatus == false)
                    {
                        var skipButtonStatus = HTOAuto.ClickOnSkipButton();
                    }
                }
            }
            ButtonState(true);
            stopButton.Enabled = false;
        }


        private void Testbtn_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = true;
            ButtonState(false);
            HTOAuto.ClickOnButton(HTOMenuButtons.MainMenu);
            System.Threading.Thread.Sleep(3000);
            HTOAuto.ClickOnButton(HTOMenuButtons.ChooseTopics);
            System.Threading.Thread.Sleep(3000);
            HTOAuto.ClickOnButton(HTOMenuButtons.Study);
            System.Threading.Thread.Sleep(3000);
            HTOAuto.ClickOnButton(HTOMenuButtons.ViewCourses);
            System.Threading.Thread.Sleep(3000);
            HTOAuto.ClickOnButton(HTOMenuButtons.PracticeExam);
            System.Threading.Thread.Sleep(3000);
            HTOAuto.ClickOnButton(HTOMenuButtons.MyAccount);
            System.Threading.Thread.Sleep(3000);
            HTOAuto.ClickOnButton(HTOMenuButtons.TopScores);
            System.Threading.Thread.Sleep(3000);
           // HTOAuto.ClickOnButton(HTOMenuButtons.StudyHistory);
            ButtonState(true);
            stopButton.Enabled = false;
        }


        private void PracticeExamBtn_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = true;
            ButtonState(false);
            HTOAuto.DoPracticeExam(HTO.HTOAuto.Exam.Tech);
            ButtonState(true);
            stopButton.Enabled = false;
        }


        private void LoginBtn_Click(object sender, EventArgs e)
        {
          
            stopButton.Enabled = true;
            ButtonState(false);
            
            HTOAuto.StartAutomation();
           
            ButtonState(true);
            stopButton.Enabled = false;
            techPool.Enabled = true;
            generalPoool.Enabled = true;
            extraPool.Enabled = true;
            //this.Refresh();
        }


        public void ButtonState(bool state)
        {  
            PracticeExamBtn.Enabled = state;
            answerButton.Enabled = state;
            studyButton.Enabled = state;
            Testbtn.Enabled = state;
            this.Refresh();
        }

        private void ShowAnswer_Click(object sender, EventArgs e)
        {
            HTO.HTOAuto.ShowCurrentQuestion();
        }
    }
}
