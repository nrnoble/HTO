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


        
        static void Main(string[] args)
        {

            //Regex rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b",

            string currentQuestionPool = string.Empty;
            string techQuestions = File.ReadAllText(@".\TechPool.txt");
            string GeneralQuestions = File.ReadAllText(@".\GeneralPool.txt");
            string ExtraQuestions = File.ReadAllText(@".\ExtraPool.txt");

            

            var loginId = "nrnoble@hotmail.com";
            var password = "J$p1ter2";
           



            

            //// var wait = new WebDriverWait(driver, new TimeSpan(1000).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);

            driver.Navigate().GoToUrl("https://www.hamradiolicenseexam.com/login.htm");


            //var kbd = driver.Keyboard;
            //kbd.SendKeys(loginId);

            ////System.Threading.Thread.Sleep(sleepTime1);
            //OpenQA.Selenium.IWebElement LoginBox = driver.FindElementByName("loginemailaddress");


            //var inputValueAtrib = LoginBox.GetAttribute("value");
            //// driver.ExecuteScript("document.getElementsByTagName('loginemailaddress').setAttribute('value', 'nrnoble@hotmail.com')");
            /////html/body/form/table/tbody/tr/td[2]/main/blockquote/table/tbody/tr/td[1]/p[3]/input
            ////var obj = driver.ExecuteScript("document.getElementsByName('loginemailaddress')");
            //var obj = driver.ExecuteScript("document.getElementsByName('loginemailaddress')");


            ////System.Threading.Thread.Sleep(sleepTime1);
            //kbd.SendKeys(tabKey);
            ////System.Threading.Thread.Sleep(sleepTime1);
            //kbd.SendKeys(password);
            ////System.Threading.Thread.Sleep(sleepTime1);
            //kbd.SendKeys(tabKey);
            ////System.Threading.Thread.Sleep(sleepTime1);
            //kbd.SendKeys(enterKey);
            //// System.Threading.Thread.Sleep(sleepTime1);

            ////var loginElement = driver.FindElementsByName("loginemailaddress");
            ///

            Login(loginId, password);


            OpenQA.Selenium.IWebElement studyButton = driver.FindElementByName("studybutton");
            studyButton.Click();
            //OpenQA.Selenium.IWebElement studyButton = driver.FindElementByName("studybutton");
            string html = (string)driver.ExecuteScript("return document.documentElement.outerHTML");
            String longQid = getQuestionID(html);
            Console.WriteLine(longQid);
            string qestionYear = getQuestionYear(longQid);
            Console.WriteLine(qestionYear);
            string Qid = getOfficalQid(longQid);
            Console.WriteLine(Qid);

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

            //string RxFindStr = Qid + @"\(.\).+?~~";
            string RxFindStr = Qid + @".+?\(.+?~~";
            Console.WriteLine(RxFindStr);
            Regex RxfindQuestionInPool = new Regex(RxFindStr, RegexOptions.Singleline);
  

            MatchCollection matches = RxfindQuestionInPool.Matches(currentQuestionPool);
           

            if (matches.Count == 0)
            {
                MessageBox.Show("Match count is zero for finding '" + Qid + "' in the question pool");
            }
            String questionFullText = matches[0].Value.ToString();
            Console.WriteLine(questionFullText);

            string answerLetter = Regex.Match(questionFullText, @"\(([^)]*)\)").Groups[1].Value;
            Console.WriteLine(answerLetter);


            string answerLine = Regex.Match(questionFullText, answerLetter + @"\.(.+)").Groups[1].Value.Trim();
            Console.WriteLine("Answer: "+ answerLine);

            var xPathtoAnswerText = getAnswerElementXpath(answerLine);
            Console.WriteLine("xPathtoAnswerText: " + xPathtoAnswerText);

            var answerTextElement = driver.FindElementByXPath(xPathtoAnswerText);
            answerTextElement.Click();


            ///html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/tr[2]/td[1]/input
            ///html/body/form/table/tbody/tr/td[2]/main/table[2]/tbody/tr/td/table/tbody/tr[2]/td[1]/input
            // document.evaluate('/html/body/div[4]/div[2]/div/div/div/div[3]/div/span[2]/span', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.innerHTML;
            //currentQuestionPool.IndexOf(qid)
            //body > form > table > tbody > tr > td:nth-child(2) > main > table > tbody > tr > td > table > tbody > tr:nth-child(2) > td:nth-child(1) > input[type="radio"]
            ///html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/tr[2]/td[1]/input
            ///
            //     /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/tr[2]/td[1]/input  --- Selector
            //     /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/tr[2]/td[2]/span   --- Text



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


            //int tr = 2;
            //string baseXpath = @"/html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/";
            //string trPath = @"tr[" + tr + "]/td[2]/span";
            //string fullXpath = baseXpath + trPath;

            //IWebElement answerTextElement = driver.FindElementByXPath(fullXpath);
            //if (answerTextElement.Text == answer)
            //{
            //    return fullXpath;
            //}

            //tr = 3;
            //trPath = @"tr[" + tr + "]/td[2]/span";
            //fullXpath = baseXpath + trPath;


            //answerTextElement = driver.FindElementByXPath(fullXpath);
            //if (answerTextElement.Text == answer)
            //{
            //    return fullXpath;
            //}

            //tr = 4;
            //trPath = @"tr[" + tr + "]/td[2]/span";
            //fullXpath = baseXpath + trPath;


            //answerTextElement = driver.FindElementByXPath(fullXpath);
            //if (answerTextElement.Text == answer)
            //{
            //    return fullXpath;
            //}


            //tr = 5;
            //trPath = @"tr[" + tr + "]/td[2]/span";
            //fullXpath = baseXpath + trPath;


            //answerTextElement = driver.FindElementByXPath(fullXpath);
            //if (answerTextElement.Text == answer)
            //{
            //    return fullXpath;
            //}

            MessageBox.Show("Could not find a valid Xpath to answer in webpage");
            return string.Empty;

        }

        static string getAnswerElement(int tr, string answer)
        {
            string baseXpath = @"/html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/";
            string trPath = @"tr[" + tr + "]/td[2]/span";
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

    }
    
    
}
