using System;
using System.Windows.Forms;

namespace HTO
{
    public static partial class HTOAuto
    {
        public static void ShowCurrentQuestion()
        {

            try
            {
                string currentAnswer = GetCorrectAnswerText(driver.PageSource);
                if (currentAnswer != string.Empty)
                {
                    MessageBox.Show(currentAnswer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to find a question or answer on this page");
            }

        }  

    }

}