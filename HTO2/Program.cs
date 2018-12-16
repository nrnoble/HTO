using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace HTO2
{
    class Program
    {

        static Regex ReFindQuestionID = new Regex(@"\[....-.....\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static ChromeDriver driver = new ChromeDriver();

        static string currentQuestionPool = string.Empty;
        static string techQuestions = File.ReadAllText(@".\TechPool.txt");
        static string GeneralQuestions = File.ReadAllText(@".\GeneralPool.txt");
        static string ExtraQuestions = File.ReadAllText(@".\ExtraPool.txt");

        static void Main(string[] args)
        {
            var loginId = "nrnoble@hotmail.com";
            var password = "J$p1ter2";

            //// var wait = new WebDriverWait(driver, new TimeSpan(1000).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);

            driver.Navigate().GoToUrl("https://www.hamradiolicenseexam.com/login.htm");
            Login(loginId, password);


            OpenQA.Selenium.IWebElement studyButton = driver.FindElementByName("studybutton");
            studyButton.Click();


            AnswerCurrentQuestion();
            System.Threading.Thread.Sleep(3000);
            AnswerCurrentQuestion();


        }




        static string getAnswerElementXpath(String answer)
        {
       
            for (int i = 2; i <= 5; i++)
            {
                var xanswerTextElement = getAnswerElement(i, answer);
                if (xanswerTextElement != string.Empty)
                {
                    return xanswerTextElement;
                }
            }

            MessageBox.Show("Could not find a valid Xpath to answer in webpage");
            return string.Empty;

        }

        static string getBaseXpath()
        {
            // /td/table/tbody
                                       // /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/
            string firstQuestionXpath = @"/html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td";
            string addionalQuestionsXpath = @"/html/body/form/table/tbody/tr/td[2]/main/table[2]/tbody/tr/td";
            //string baseXpath = firstQuestionXpath;


            if (doesXpathExist(addionalQuestionsXpath) == true)
            {
                return addionalQuestionsXpath + "/";
            }
            else if (doesXpathExist(firstQuestionXpath) == true)
            {
                return firstQuestionXpath + "/";
            }
            return string.Empty;
        }




        static string getBaseXpath1()
        {
                                           // /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td
            string firstQuestionXpath =     @"/html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td";
            string addionalQuestionsXpath = @"/html/body/form/table/tbody/tr/td[2]/main/table[2]/tbody/tr";
            //string baseXpath = firstQuestionXpath;


            if (doesXpathExist(addionalQuestionsXpath) == true)
            {
                return addionalQuestionsXpath + "/";
            }
            else if (doesXpathExist(firstQuestionXpath) == true)
            {
                return firstQuestionXpath + "/";
            }            
            return string.Empty;
        }


        static bool doesXpathExist(string xPath)
        {
            IWebElement element = null;
            bool pathExists = false;
            try
            {
                 element = driver.FindElementByXPath(xPath);
            }
            catch (Exception)
            {

                return false;
            }
            

            if (element != null)
            {
                return true;
            }

            return pathExists;
        }



        static string getAnswerElement(int tr, string answer)
        {


            string baseXpath = getBaseXpath();
            //                     
            //                     /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/ -- base path
            //                     /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/td/table/tbody/tr[2]/td[2]/span
            //                     /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/tr[2]/td[2]/span
            //                     /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/
            //string baseXpath = @"/html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/";
            string trPath = @"table/tbody/tr[" + tr + "]/td[2]/span";
            string fullXpath = baseXpath + trPath;

            IWebElement answerTextElement = driver.FindElementByXPath(fullXpath);
            if (answerTextElement.Text == answer)
            {
                return fullXpath;
            }

            return string.Empty;
        }


        static String getQuestionID(String PageHTML)
        {
            MatchCollection matches = ReFindQuestionID.Matches(PageHTML);
            String match1 = string.Empty;
            String match2 = string.Empty;
            String QID = String.Empty;

            if (matches.Count == 0)
            {
                MessageBox.Show("Unable to find a valid question on page");
            }
            var m1 = matches[0];
            match1 = m1.Value;
            QID = match1;

            if (matches.Count > 1)
            {
                var m2 = matches[1];
                match2 = m1.Value;
                QID = match2;
            }
            QID = QID.Replace('[',' ');
            QID = QID.Replace(']', ' ');
            QID = QID.Trim();

            return QID;
            
        }


        static String getQuestionYear(String LongQid)
        {
            string year = String.Empty;
            year = LongQid.Substring(0, 4);
            return year;
        }


        static string getOfficalQid(String LongQid)
        {
            String officialQid = String.Empty;
            officialQid = LongQid.Substring(5, 5);
            return officialQid;
        }

        static void Login(string userEmail, string password)
        {
            //var sleepTime1 = 25;
            var enterKey = "\r\n";
            var tabKey = "\t";

            var kbd = driver.Keyboard;
            kbd.SendKeys(userEmail);

            //System.Threading.Thread.Sleep(sleepTime1);
            OpenQA.Selenium.IWebElement LoginBox = driver.FindElementByName("loginemailaddress");


            var inputValueAtrib = LoginBox.GetAttribute("value");
            // driver.ExecuteScript("document.getElementsByTagName('loginemailaddress').setAttribute('value', 'nrnoble@hotmail.com')");
            ///html/body/form/table/tbody/tr/td[2]/main/blockquote/table/tbody/tr/td[1]/p[3]/input
            //var obj = driver.ExecuteScript("document.getElementsByName('loginemailaddress')");
            var obj = driver.ExecuteScript("document.getElementsByName('loginemailaddress')");


            //System.Threading.Thread.Sleep(sleepTime1);
            kbd.SendKeys(tabKey);
            //System.Threading.Thread.Sleep(sleepTime1);
            kbd.SendKeys(password);
            //System.Threading.Thread.Sleep(sleepTime1);
            kbd.SendKeys(tabKey);
            //System.Threading.Thread.Sleep(sleepTime1);
            kbd.SendKeys(enterKey);
            // System.Threading.Thread.Sleep(sleepTime1);


        }

        static void AnswerCurrentQuestion()
        {
     
            string html = (string)driver.ExecuteScript("return document.documentElement.outerHTML");
            String longQid = getQuestionID(html);
            Console.WriteLine(longQid);

            string qestionYear = getQuestionYear(longQid);
            Console.WriteLine(qestionYear);


            if (qestionYear == "2018")
            {
                currentQuestionPool = techQuestions;
            }
            else if (qestionYear == "2015")
            {
                currentQuestionPool = GeneralQuestions;
            }
            else if (qestionYear == "2016")
            {
                currentQuestionPool = ExtraQuestions;

            }
            else
            {
                MessageBox.Show("The current year is invalid: " + qestionYear);
            }







            string Qid = getOfficalQid(longQid);
            Console.WriteLine(Qid);
            //string RxFindStr = Qid + @"\(.\).+?~~";
            string RxFindStr = Qid + @".+?\(.+?~~";
            Console.WriteLine(RxFindStr);
            Regex RxfindQuestionInPool = new Regex(RxFindStr, RegexOptions.Singleline);


            MatchCollection matches = RxfindQuestionInPool.Matches(currentQuestionPool);

            String questionFullText = string.Empty;
            if (matches.Count == 0)
            {
                MessageBox.Show("Match count is zero for finding '" + Qid + "' in the question pool");
            }

            if (matches.Count == 1)
            {
                questionFullText = matches[0].Value.ToString();
                Console.WriteLine(questionFullText);
            }

            if (matches.Count == 2)
            {
                questionFullText = matches[1].Value.ToString();
                Console.WriteLine(questionFullText);
            }

            if (matches.Count > 2)
            {
                MessageBox.Show("There are 3 or more matches found, there should be no more than 2. Exit here");
                Application.Exit();
            }


            string answerLetter = Regex.Match(questionFullText, @"\(([^)]*)\)").Groups[1].Value;
            Console.WriteLine(answerLetter);


            string answerLine = Regex.Match(questionFullText, answerLetter + @"\.(.+)").Groups[1].Value.Trim();
            Console.WriteLine("Answer: " + answerLine);

            var xPathtoAnswerText = getAnswerElementXpath(answerLine);
            Console.WriteLine("xPathtoAnswerText: " + xPathtoAnswerText);
            if (xPathtoAnswerText == string.Empty)
            {
                ClickOnSkipButton();
            }
            else
            {
                var answerTextElement = driver.FindElementByXPath(xPathtoAnswerText);
                answerTextElement.Click();
            }
        }

        static void ClickOnSkipButton()
        {
            bool status = clickOnElement("skipquestionbutton");
            if (status == false)
            {
                MessageBox.Show("Unable to find Skip Question button. Exiting application now");
                Application.Exit();
            }

            // OpenQA.Selenium.IWebElement skipQuestionButton = driver.FindElementByName("skipquestionbutton");
            // skipQuestionButton.Click();
            
            
            //skipquestionbutton
            // /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/input[3]
            // /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/ -bad do no use

            //string baseXPath = getBaseXpath();
            //string fullXpath = baseXPath + "tr/td/input[3]";
           // clickThisXpath(fullXpath);

        }


        static bool clickOnElement(string NameOfHTMLItem)
        {
            OpenQA.Selenium.IWebElement element = driver.FindElementByName("skipquestionbutton");
            if (element == null)
            {
                return false;
            }

            element.Click();
            return true;

        }



        static void clickThisXpath(string fullXpath)
        {
            if (doesXpathExist(fullXpath) == true)
            {
                driver.FindElementByXPath(fullXpath).Click();
            }
        }



    }





}
