using System;
using System.Windows.Forms;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using Keys = OpenQA.Selenium.Keys;

namespace HTO
{
    public static partial class HTOAuto
    {
        public static void ShowCurrentQuestion()
        {

            try
            {
                //string currentAnswer = GetCorrectAnswerText(driver.PageSource);
                hq = new HamQuestion(driver.PageSource, HTOAuto.QuestionPool);
                string currentAnswer = hq.Answer;

                if (currentAnswer != string.Empty)
                {
                   // MessageBox.Show(currentAnswer);
                    MessageBox.Show(currentAnswer, hq.Question);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to find a question or answer on this page");
            }

        }

        public static void ResearchQuestion()
        {
            try
            {
                // var hq2 = new HamQuestion(driver.PageSource, HTO.HTOAuto.QuestionPool);
                // var actions1 = new Actions(driver);
                // var elementUrlLink = driver.FindElement(By.TagName("HTML"));
                //// var body = driver.FindElement(By.TagName("body"));
                // var body = driver.FindElement(By.Name("choosetopicsbutton"));
                // body.SendKeys(OpenQA.Selenium.Keys.Control + 'U');
                // new actions1.KeyDown(Keys.Control).Click(body).KeyUp(Keys.Control).Build().perform();
                // var ctr = OpenQA.Selenium.Keys.Control;
                // actions1.KeyDown(OpenQA.Selenium.Keys.LeftControl);
                // actions1.SendKeys("T");
                // actions1.KeyUp(OpenQA.Selenium.Keys.LeftControl);

                // actions1.SendKeys("HAM T8B06");
                // actions1.SendKeys(OpenQA.Selenium.Keys.Enter);
                // actions1.Perform();

                Console.WriteLine("driver.Url: " + driver.Url);
                //driver.Url = "https://google.com";
                //driver.ExecuteScript("window.open('HAM T8B06','_blank');");
                //Console.WriteLine("driver.Url: " + driver.Url); 

                driver.ExecuteScript("alert('Test')");
              //  ((JavascriptExecutor)driver).executeScript("alert('Test')");
                driver.SwitchTo().Alert().Accept();
                driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");

                //Actions keyAction = new Actions(driver);
                //keyAction.KeyDown(Keys.Alt.ToString()).KeyDown(Keys.ShiftKey).SendKeys("z").KeyUp(Keys.ALT).KeyUp(Keys.ShiftKey).perform();

                // String selectLinkOpeninNewTab = Keys.chord(Keys.CONTROL, Keys.RETURN);
                // driver.FindElement(By.LinkText("urlLink")).SendKeys(selectLinkOpeninNewTab);


            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to properly research question on the internet.");
                Console.WriteLine("error: " + e.Message);    
                //throw;
            }

        }

    }

}