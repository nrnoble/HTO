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
using System.Runtime.InteropServices;

namespace HTO2
{
    class Program
    {
        try
        const int SWP_NOZORDER = 0x4;
        const int SWP_NOACTIVATE = 0x10;

        [DllImport("kernel32")]
        static extern IntPtr GetConsoleWindow();


        [DllImport("user32")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int x, int y, int cx, int cy, int flags);



        enum PoolQuestions
        {
            Tech = 218,
            General = 315,
            Extra = 416
        }

        
        static Regex ReFindQuestionID = new Regex(@"\[....-.....\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static ChromeDriver driver = new ChromeDriver();

        static int skipcount = 0;
        static int count = 0;


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


            OpenQA.Selenium.IWebElement viewCoursesButton = driver.FindElementByName("gotoviewcoursesbutton");
            viewCoursesButton.Click();

             selectQuestionPool(PoolQuestions.Tech).Click();

            //OpenQA.Selenium.IWebElement studyButton = driver.FindElementByName("studybutton");
            //studyButton.Click();

            //AnswerCurrentQuestion();
            ////System.Threading.Thread.Sleep(1000);

            //for (int i = 0; i <= 10000; i++)
            //{
            //    AnswerCurrentQuestion();
            //    Console.WriteLine("Question Count: " + i + " Skip Count: " + skipcount);
            //}


        }


        static IWebElement selectQuestionPool(PoolQuestions value)
        {

            // 218 is tech
            // 315 is general
            // 416 is Extra

            string str = value.ToString();

            Console.WriteLine("");

            try
            {
                var viewCourseButtons = driver.FindElements(By.Name("viewcoursestoptopic"));
                foreach (var element in viewCourseButtons)
                {
                    //Console.WriteLine(element.GetAttribute("Value"));
                    if (element.GetAttribute("Value") == value.ToString())
                    {
                        return element;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                Console.WriteLine(" Exception thrown in 'static IWebElement selectQuestionPool(string value)'");
                return null;
            }            
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


        //should not be required
        static string getBaseXpath()
        {
            // /td/table/tbody
                                       // /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/
            string firstQuestionXpath = @"/html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/";
            string addionalQuestionsXpath = @"/html/body/form/table/tbody/tr/td[2]/main/table[2]/tbody/tr/td/";
            //string baseXpath = firstQuestionXpath;


            if (doesXpathExist("/html/body/form/table/tbody/") == true)
            {
               // return addionalQuestionsXpath + "/";
                return addionalQuestionsXpath;
            }
            else if (doesXpathExist(firstQuestionXpath) == true)
            {
                //return firstQuestionXpath + "/";
                return firstQuestionXpath;
            }
            return string.Empty;
        }

        //should not be required
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

        //should not be required
        static bool doesXpathExist(string xPath)
        {

            string pageHTML = (string)driver.ExecuteScript("return document.documentElement.outerHTML");

            var escapedXpath = Regex.Escape(xPath);

           Match  matchResults = Regex.Match(pageHTML, escapedXpath);


            var element = driver.FindElementByXPath(xPath);

            if (matchResults.Success == true)
            {
                Console.WriteLine(xPath + " Element is Present");
                return true;
            }
            else
            {
                Console.WriteLine(xPath + "Element is Absent");
                return false;
            }
            

        }


        //should not be required
        static bool doesXpathExist1(string xPath)
        {
            IWebElement element = null;
            var size = element.Size;
                 
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


        static IWebElement getAnswerElement(string answer)
        {
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> answerTextElement = null ;
            try
            {
                 answerTextElement = driver.FindElements(By.XPath("//span[contains(text(), '" + answer + "') and contains(@class, 'unselectedAnswer')] "));
            }
            catch (Exception)
            {
                return null;
            }

            foreach (var element in answerTextElement)
            {
                if (element.Text.Trim() == answer.Trim())
                {
                    return element;
                }
            }
            return null; 
    
        }



        // nolonger needed
        static string getAnswerElement(int tr, string answer)
        {


            // string baseXpath = getBaseXpath();
            //                     
            //                      /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/ -- base path
            //                      /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/td/table/tbody/tr[2]/td[2]/span
            //                      /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/tr[2]/td[2]/span
            //                      /html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/
            // string baseXpath = @"/html/body/form/table/tbody/tr/td[2]/main/table/tbody/tr/td/table/tbody/";

            // string trPath = @"table/tbody/tr[" + tr + "]/td[2]/span";
            // string fullXpath = baseXpath + trPath;

            // var element5 = driver.FindElements(By.XPath("//span[contains(text(), 'CW') and contains(@class, 'unselectedAnswer')] "));
            var answerTextElement = driver.FindElements(By.XPath("//span[contains(text(), '" + answer + "') and contains(@class, 'unselectedAnswer')] "));
            answerTextElement[0].Click();

            string fullXpath = getXPath(answerTextElement[0]);

            if (answerTextElement[0].Text.Trim() == answer.Trim())
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
                return string.Empty;
            }

            var m1 = matches[0];
            match1 = m1.Value;
            QID = match1;

            if (matches.Count > 1)
            {
                var m2 = matches[1];
                match2 = m2.Value;
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
            if (qestionYear == string.Empty)
            {
                Console.WriteLine("No valid year found. Exiting application");
                Application.Exit();

            }





            if (qestionYear == "2018")
            {
                currentQuestionPool = techQuestions;
                Console.WriteLine("Year: " + qestionYear + ". Setting to Tech pool questions");
            }
            else if (qestionYear == "2015")
            {
                currentQuestionPool = GeneralQuestions;
                Console.WriteLine("Year: " + qestionYear + ". Setting to General pool questions");
            }
            else if (qestionYear == "2016")
            {
                currentQuestionPool = ExtraQuestions;
                Console.WriteLine("Year: " + qestionYear + ". Setting to Extra pool questions");


            }
            else
            {
                MessageBox.Show("The current year is invalid: " + qestionYear + ".  Exit app here." );
                Application.Exit();
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
                Application.Exit();

                //TODO: Search for OK box, and click it then restart answering question.
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

            // var xPathtoAnswerText = getAnswerElementXpath(answerLine);
            // Console.WriteLine("xPathtoAnswerText: " + xPathtoAnswerText);

            var answerElement = getAnswerElement(answerLine);
            if (answerElement != null)
            {
                answerElement.Click();
            }
            else
            {
                Console.WriteLine("Skipping question: " + Qid);
                skipcount++;
                ClickOnSkipButton();
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


        /// <summary>
        /// Sets the console window location and size in pixels
        /// </summary>
        public static void SetWindowPosition(int x, int y, int width, int height)
        {
            Console.WindowWidth = 132;
            Console.WindowHeight = 35;
            Console.BufferWidth = 134;
            Console.BufferHeight = 300;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Console bufferHeight: " + Console.BufferHeight);
            Console.WriteLine("Console bufferHeight: " + Console.BufferWidth);
            Console.SetWindowPosition(1,2);

          //  var screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
          //  var width = screen.Width;
          //  var height = screen.Height;
          //  SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, SWP_NOZORDER | SWP_NOACTIVATE);
        }

        public static IntPtr Handle
        {
            get
            {
                //Initialize();
                return GetConsoleWindow();
            }
        }

 
        static String getXPath(IWebElement element)
        {
            String jscript = "function getPathTo(node) {" +
                "  var stack = [];" +
                "  while(node.parentNode !== null) {" +
                "    stack.unshift(node.tagName);" +
                "    node = node.parentNode;" +
                "  }" +
                "  return stack.join('/');" +
                "}" +
                "return getPathTo(arguments[0]);";
            return (String)driver.ExecuteScript(jscript, element);
        }



    }





}
