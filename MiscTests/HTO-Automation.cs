using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;
using System.CodeDom;




namespace HTO
{

    public static class HTOAuto
    {
        static Regex ReFindQuestionID = new Regex(@"\[....-.....\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static ChromeDriver driver = null;
        static MainForm mf = null;

        static int questionSkippedCount = 0;

        const string HTO_LOGIN_URL = "https://www.hamradiolicenseexam.com/login.htm";
        static string selectedQuestionPool = string.Empty;
        static string techQuestions = File.ReadAllText(@".\TechPool.txt");
        static string GeneralQuestions = File.ReadAllText(@".\GeneralPool.txt");
        static string ExtraQuestions = File.ReadAllText(@".\ExtraPool.txt");


        // display form
        public static void Initialization()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mf = new MainForm();
            Application.Run(mf);
        }


        public static void StartAutomation()
        {

            // Using the Chrome broswer
            driver = new ChromeDriver();

            // get User & Pwd from Main form and login into HTO
            var user = mf.userID.Text.Trim();
            var password = mf.password.Text.Trim();
            Login(user, password, HTO_LOGIN_URL);

            SelectMainTopics();
            ClickOnButton("studybutton");
            AnswerCurrentQuestion();



        }


        /// <summary>
        /// Auto Login to HTO
        /// </summary>
        /// <param name="userEmail">User account</param>
        /// <param name="password">Account Password</param>
        /// <param name="loginURL">HTO Login page</param>
        public static void Login(string userEmail, string password, string loginURL)
        {
  
            // used for sending clicks and and characters
            Actions actions = new Actions(driver);


            // set driver timeout and launch browser
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
            driver.Navigate().GoToUrl(loginURL);


            // Get the elements of the login elements from HTML
            OpenQA.Selenium.IWebElement LoginBox = driver.FindElementByName("loginemailaddress");
            OpenQA.Selenium.IWebElement LoginPassword = driver.FindElementByName("loginpassword");
            OpenQA.Selenium.IWebElement LoginButton = driver.FindElementByName("loginbutton");

            //Execute auto login
            actions.SendKeys(LoginBox, userEmail);
            actions.SendKeys(LoginPassword, password);
            actions.Click(LoginButton);
            actions.Perform();

           
        }


        public static void SelectMainTopics()
        {
            // click on Choose topic Menu button on left of screen
            OpenQA.Selenium.IWebElement choosetopicsbutton = driver.FindElementByName("choosetopicsbutton");
            choosetopicsbutton.Click();

            // get the selected radio Button
            var checkedButton = mf.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            //Get the selected Pool 
            var poolValue = "218";
            switch (checkedButton.Text)
            {

                case "Tech":
                    poolValue = "218";
                    break;

                case "General":
                    poolValue = "315";
                    break;

                case "Extra":
                    poolValue = "416";
                    break;

                default:
                    break;
            }

            // Console.WriteLine(checkedButton.Text + "= " + poolValue);



            // Initialize by  unchecking all HTO topics 
            var elements = driver.FindElements(By.Name("choosetopic"));
            foreach (var elment in elements)
            {

                if (elment.Selected == true)
                {
                    elment.Click();
                }
            }

            // Check only one of the main HTO topic to be drilled.
            foreach (var elment in elements)
            {
                if (elment.GetAttribute("Value") == poolValue)
                {
                    elment.Click();
                    break;
                }
            }

        }


        static IWebElement selectQuestionSelection(string value)
        {
            return ReviewCourseOptions(ReviewCourseOptionsEmun.QuestionSelection, value);
        }


        static IWebElement selectSkippedQuestions(string value)
        {
            return ReviewCourseOptions(ReviewCourseOptionsEmun.SkippedQuestions, value);
        }


        static IWebElement selectSortOrder(string value)
        {
            return ReviewCourseOptions(ReviewCourseOptionsEmun.SortOrder, value);
        }


        static IWebElement selectQuestionPool(string value)
        {


            return ReviewCourseOptions("viewcoursestoptopic", value);

            // // 218 is tech
            // // 315 is general
            // // 416 is Extra



            //// int val = (int)value;



            // //Console.WriteLine(val);

            // try
            // {
            //     var viewCourseButtons = driver.FindElements(By.Name("viewcoursestoptopic"));
            //     foreach (var element in viewCourseButtons)
            //     {
            //         //Console.WriteLine(element.GetAttribute("Value"));
            //         if (element.GetAttribute("Value") == value)
            //         {
            //             return element;
            //         }
            //     }

            //     return null;
            // }
            // catch (Exception)
            // {
            //     Console.WriteLine(" Exception thrown in 'static IWebElement selectQuestionPool(string value)'");
            //     return null;
            // }            
        }


        static IWebElement ReviewCourseOptions(string optionName, string value)
        {
            try
            {
                var viewCourseButtons = driver.FindElements(By.Name(optionName));
                foreach (var element in viewCourseButtons)
                {
                    //Console.WriteLine(element.GetAttribute("Value"));
                    if (element.GetAttribute("Value") == value)
                    {
                        return element;
                    }
                }

                return null;
            }

            catch (Exception)
            {
                Console.WriteLine(" Exception thrown in 'static IWebElement ReviewCourseOptions(string optionName, string value)'");
                return null;
            }
        }


        //should not be required
        public static bool doesXpathExist(string xPath)
        {

            string pageHTML = (string)driver.ExecuteScript("return document.documentElement.outerHTML");

            var escapedXpath = Regex.Escape(xPath);

            Match matchResults = Regex.Match(pageHTML, escapedXpath);


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


        public static IWebElement getAnswerElement(string answer)
        {
            //  string  qanswer = "You receive reports of \"hum\" on your station's transmitted signal";
            //  string correctAnswer =  "You receive reports of \"hum\" on your station's transmitted signal";
            //string correctAnswer = "It will change depending on the resistor's temperature coefficient";
            //string  qanswer = "The third party’s amateur license has been revoked and not reinstated";
            //StringTest(answer);
            //  StringComparer(correctAnswer, qanswer);


            //answer = answer.Substring(0, 39);
            //  string literalAnswer = answer;
            var originalanswer = answer;


            // string literalAnswer = ToLiteral(answer);

            //   string test = "It will change depending on the resistor's temperature coefficient";



            int correct = 39; // correct answer
            int incorrect = 8217;

            //if (answer.Contains("'"))
            //{
            //    Console.WriteLine(answer);
            //}

            string str1 = ((char)correct).ToString();
            string str2 = @"\" + str1;
            //        string literalAnswer = answer.Replace((char)incorrect, (char)correct);
            string literalAnswer = answer.Replace((char)incorrect, (char)correct);
            literalAnswer = answer.Replace(str1, str2);
            //  string literalAnswer = answer.Replace(@"’", @"\’");
            //  literalAnswer = literalAnswer.Replace("\'", "\\\"");

            //    StringComparer(literalAnswer, answer);


            string xPath = "//span[contains(text(), \"" + answer + "\") and contains(@class, 'unselectedAnswer')] ";
            // string xPath = "//span[contains(text(), \'" + literalAnswer + "\') and contains(@class, 'unselectedAnswer')] ";



            if (answer.Contains('"'))
            {
                xPath = "//span[contains(text(), \'" + answer + "\') and contains(@class, 'unselectedAnswer')] ";
            }




            Console.WriteLine("Search Answer xPath: " + xPath);
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> answerTextElement = null;
            try
            {
                answerTextElement = driver.FindElements(By.XPath(xPath));
                if (answerTextElement.Count == 0)
                {

                    answer = stringPreProcessor(answer);
                    xPath = "//span[contains(text(), \"" + answer + "\") and contains(@class, 'unselectedAnswer')] ";
                    if (answer.Contains('"'))
                    {
                        xPath = "//span[contains(text(), \'" + answer + "\') and contains(@class, 'unselectedAnswer')] ";
                    }
                    answerTextElement = driver.FindElements(By.XPath(xPath));
                }


            }
            catch (Exception)
            {
                return null;
            }


            //Console.WriteLine("");  
            //Console.WriteLine("");  
            //Console.WriteLine("");  

            //  Console.WriteLine(answerTextElement.Count + " elements have been found containing: " + xPath);
            //  StringComparer(originalanswer, answer);
            foreach (var element in answerTextElement)
            {
                Console.WriteLine(element.Text.Trim());
                if (element.Text.Trim() == answer.Trim())
                {
                    return element;
                }
            }
            return null;



        }



        public static String getQuestionID(String PageHTML)
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
            QID = QID.Replace('[', ' ');
            QID = QID.Replace(']', ' ');
            QID = QID.Trim();

            return QID;

        }


        public static String getQuestionYear(String LongQid)
        {
            string year = String.Empty;
            year = LongQid.Substring(0, 4);
            return year;
        }


        public static string getOfficalQid(String LongQid)
        {
            String officialQid = String.Empty;
            officialQid = LongQid.Substring(5, 5);
            return officialQid;
        }
        

        public static void AnswerCurrentQuestion()
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
                selectedQuestionPool = techQuestions;
                Console.WriteLine("Year: " + qestionYear + ". Setting to Tech pool questions");
            }
            else if (qestionYear == "2015")
            {
                selectedQuestionPool = GeneralQuestions;
                Console.WriteLine("Year: " + qestionYear + ". Setting to General pool questions");
            }
            else if (qestionYear == "2016")
            {
                selectedQuestionPool = ExtraQuestions;
                Console.WriteLine("Year: " + qestionYear + ". Setting to Extra pool questions");


            }
            else
            {
                MessageBox.Show("The current year is invalid: " + qestionYear + ".  Exit app here.");
                Application.Exit();
            }







            string Qid = getOfficalQid(longQid);
            Console.WriteLine(Qid);
            //string RxFindStr = Qid + @"\(.\).+?~~";
            string RxFindStr = Qid + @".+?\(.+?~~";
            Console.WriteLine(RxFindStr);
            Regex RxfindQuestionInPool = new Regex(RxFindStr, RegexOptions.Singleline);


            MatchCollection matches = RxfindQuestionInPool.Matches(selectedQuestionPool);

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


            var answerElement = getAnswerElement(answerLine);
            if (answerElement != null)
            {
                answerElement.Click();
            }
            else
            {
                Console.WriteLine("Skipping question: " + Qid);
                questionSkippedCount++;
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
        }


        static void ClickOnButton(string buttonName)
        {
            bool status = clickOnElement(buttonName);
            if (status == false)
            {
                MessageBox.Show("Unable to find " + buttonName + ". Exiting application now");
                Application.Exit();
            }
        }



        public static bool clickOnElement(string NameOfHTMLItem)
        {
            OpenQA.Selenium.IWebElement element = driver.FindElementByName(NameOfHTMLItem);
            if (element == null)
            {
                return false;
            }

            element.Click();
            return true;
        }


        public static void clickThisXpath(string fullXpath)
        {
            if (doesXpathExist(fullXpath) == true)
            {
                driver.FindElementByXPath(fullXpath).Click();
            }
        }


        public static String getXPath(IWebElement element)
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


        public static string ToLiteral(string input)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
        }


        public static void StringComparer(String st1, string st2)
        {
            var strln = st1.Length;
            for (int i = 0; i < strln; i++)
            {
                var c1 = st1[i];
                var c2 = st2[i];




                if (c1 != c2)
                {

                    Console.WriteLine(c1 + "!=" + c2);
                    Console.WriteLine((int)c1 + "!=" + (int)c2);

                }
                else
                {
                    Console.WriteLine(st1[i] + "=" + st2[i] + "  " + (int)c1);
                }

            }
        }


        public static string stringPreProcessor(string str)
        {

            var processedStr = string.Empty;
            
            int correct = 39; // correct answer
            int incorrect = 8217;

            for (int i = 0; i < str.Length; i++)
            {
                if ((int)str[i] == incorrect)
                {
                    processedStr = processedStr + (char)correct;
                    continue;
                }
                else
                {
                    processedStr = processedStr + str[i];
                    continue;
                }
            }

            return processedStr;
        }

    }


    public static class PoolQuestionsEnum
    {
        private const string tech = "218";
        private const string general = "315";
        private const string extra = "416";

        //PoolQuestionsEnum()
        //{ }

        public static string Tech
        {
            get
            {
                return tech;
            }
        }


        public static string General
        {
            get
            {
                return general;
            }
        }

        public static string Extra
        {
            get
            {
                return extra;
            }
        }

    }


    public static class SortOrderEnum
    {
        private const string course = "Course";
        private const string question = "Question";
        private const string stale = "Stale";
        private const string lowscore = "Low score";
        private const string stalelow = "Stale and low";
        private const string repeatDue = "Repeat due";


        //PoolQuestionsEnum()
        //{ }

        public static string Course
        {
            get
            {
                return course;
            }
        }


        public static string Question
        {
            get
            {
                return question;
            }
        }

        public static string Stale
        {
            get
            {
                return stale;
            }
        }

        public static string Lowscore
        {
            get
            {
                return lowscore;
            }
        }

        public static string Stalelow
        {
            get
            {
                return stalelow;
            }
        }

        public static string RepeatDue
        {
            get
            {
                return repeatDue;
            }
        }

    }


    public static class SkippedQuestionsEnum
    {
        private const string all = "A";
        private const string noSkips = "N";
        private const string skipsOnly = "Y";


        //PoolQuestionsEnum()
        //{ }

        public static string All
        {
            get
            {
                return all;
            }
        }


        public static string NoSkips
        {
            get
            {
                return noSkips;
            }
        }

        public static string SkipsOnly
        {
            get
            {
                return skipsOnly;
            }
        }
    }


    public static class ReviewCourseOptionsEmun
    {
        private const string questionPool = "viewcoursestoptopic";
        private const string sortOrder = "viewcoursessortorder";
        private const string questionSelection = "";
        private const string skippedQuestions = "viewcoursesincludeskipped";

        public static string QuestionPool
        {
            get
            {
                return questionPool;
            }
        }

        public static string SortOrder
        {
            get
            {
                return sortOrder;
            }
        }

        public static string QuestionSelection
        {
            get
            {
                return questionSelection;
            }
        }


        public static string SkippedQuestions
        {
            get
            {
                return skippedQuestions;
            }
        }

    }


    public static class QpoolCount
    {
        private const int tech = 423;
        private const int general = 462;
        private const int extra = 712;

        //PoolQuestionsEnum()
        //{ }

        public static int Tech
        {
            get
            {
                return tech;
            }
        }


        public static int General
        {
            get
            {
                return general;
            }
        }

        public static int Extra
        {
            get
            {
                return extra;
            }
        }

    }


}

