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
using System.Threading;

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

        public enum Exam
        {
            Tech = 218,
            General = 315,
            Extra = 416
        }



        // display form
        public static void Initialization()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mf = new MainForm();
            Application.Run(mf);
        }

        public static void StartBrowswer()
        {
            driver = new ChromeDriver();

        }

       
        public static void StartAutomation()
        {

           // Using the Chrome broswer
            driver = new ChromeDriver();
           
           // Thread thread = new Thread(new ThreadStart(StartBrowswer));
           // thread.Start();

      

            // get User & Pwd from Main form and login into HTO
            var user = mf.userID.Text.Trim();
            var password = mf.password.Text.Trim();
            Login(user, password, HTO_LOGIN_URL);

            SelectMainTopics();
            ClickOnButton("studybutton");
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
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 12);
            driver.Navigate().GoToUrl(loginURL);


            // Get the elements of the login elements from HTML
            
                OpenQA.Selenium.IWebElement LoginBox = driver.FindElementByName("loginemailaddress");
                OpenQA.Selenium.IWebElement LoginPassword = driver.FindElementByName("loginpassword");
                OpenQA.Selenium.IWebElement LoginButton = driver.FindElementByName(HTOMenuButtons.Login);
            
            
            //Execute auto login
            {
                actions.SendKeys(LoginBox, userEmail);
                actions.SendKeys(LoginPassword, password);
                actions.Click(LoginButton);
                actions.Perform();
            }
        }


        public static void SelectMainTopics()
        {
            // click on Choose topic Menu button on left of screen
            // OpenQA.Selenium.IWebElement choosetopicsbutton = driver.FindElementByName("choosetopicsbutton");
            // choosetopicsbutton.Click();

            ClickOnButton(HTOMenuButtons.ChooseTopics);


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

        /// <summary>
        ///  Core function that answers an individual HTO question in browser
        /// </summary>
        public static bool AnswerCurrentQuestion()
        {


            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            var handle = driver.CurrentWindowHandle;

            Console.WriteLine("CurrentWindowHandle: " + handle);
            // string html = (string)driver.ExecuteScript("return document.documentElement.outerHTML");
            // string answerLine =  GetCorrectAnswerText(html);

            // This section locates the HTML element that is the correct answer
            // and then sends a 'click" message.
            {
                var answerElement = getAnswerElement();
                if (answerElement != null)
                {
                    // Click on correct answer
                    answerElement.Click();
                    IncreamentQuestionCounter();
                    mf.Refresh();
                    return true;
                }
                else
                {
                    Console.WriteLine("Problem answering this question");
                    return false;
                    // If correct answer can not be found, then click on skipbutton.
                    // Console.WriteLine("Skipping question: " + Qid);
                    // questionSkippedCount++;
                    //  ClickOnSkipButton();
                    // TODO: check for skip button
                    // If skip button does not exist, then look for OK button and click.
                }
            }
        }
        

        public static string GetCorrectAnswerText(string html)
        {
            //TODO: This function should be broken down into several smaller functions.



            // string html = (string)driver.ExecuteScript("return document.documentElement.outerHTML");
            String longQid = getQuestionID(html);
            Console.WriteLine("longID Found: " + longQid);
            if (longQid == string.Empty)
            {
                Console.WriteLine("longID not Found: " + longQid);
                return string.Empty;
            }



            string qestionYear = getQuestionYear(longQid);
            if (qestionYear == string.Empty)
            {
                // if Year is empty this get executed something signficantly has gone wrong.

                Console.WriteLine("No valid year found. Exiting application");
                Application.Exit();
            }

            {
                // this section selects the active question pool based on 'year' fount in the questionID

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
            }


            // Get the question question ID that will match the official ID in the question pool.
            string Qid = getOfficalQid(longQid);
            Console.WriteLine("QID: " + Qid);
            //string RxFindStr = Qid + @"\(.\).+?~~";
            string RxFindStr = Qid + @".+?\(.+?~~";
            //Console.WriteLine(RxFindStr);
            Regex RxfindQuestionInPool = new Regex(RxFindStr, RegexOptions.Singleline);

            // Location all Question IDs 
            MatchCollection matches = RxfindQuestionInPool.Matches(selectedQuestionPool);

            String questionFullText = string.Empty;
            if (matches.Count == 0)
            {
                // TODO: Search for OK button, and click it then restart answering question.
                //       If no OK button, then select skip button.
                //       if no skip button, the hault execution.
                MessageBox.Show("Match count is zero for finding '" + Qid + "' in the question pool");
                return string.Empty;
                // Application.Exit();
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
                return string.Empty;
                // MessageBox.Show("There are 3 or more matches found, there should be no more than 2. Exit here");
                // Application.Exit();
            }


            string answerLetter = Regex.Match(questionFullText, @"\(([^)]*)\)").Groups[1].Value;
            Console.WriteLine("Going to select answer: " + answerLetter);

            // Strip off the Alpha Option (A,B,C,D) from line
            string answerLine = Regex.Match(questionFullText, answerLetter + @"\.(.+)").Groups[1].Value.Trim();
            Console.WriteLine("Correct answer: " + answerLine);

            return answerLine;

            // This section locates the HTML element that is the correct answer
            // and then sends a 'click" message.
            //{
            //    var answerElement = getAnswerElement(answerLine);
            //    if (answerElement != null)
            //    {
            //        // Click on correct answer
            //        answerElement.Click();
            //    }
            //    else
            //    {
            //        // If correct answer can not be found, then click on skipbutton.
            //        Console.WriteLine("Skipping question: " + Qid);
            //        questionSkippedCount++;
            //        ClickOnSkipButton();
            //        // TODO: check for skip button
            //        // If skip button does not exist, then look for OK button and click.
            //    }
            //}
        }


        public static void DoPracticeExam(Exam exam)
        {
            bool status = ClickOnButton(HTOMenuButtons.MainMenu);

            status = ClickOnButton(HTOMenuButtons.PracticeExam);
            if (status == false)
            {
                return;
            }
            

            var checkedButton = mf.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            var selectedPool = checkedButton.Text;
            var totalExamQuestions = 35;
            if (selectedPool == "General")
            {
                driver.FindElementById("generatenewpracticeexamtopicT315").Click();
            }
            else if (selectedPool == "Extra")
            {
                driver.FindElementById("generatenewpracticeexamtopicT416").Click();
                totalExamQuestions = 50;
            }
            else
            {
                driver.FindElementById("generatenewpracticeexamtopicT218").Click();
            }

            status = ClickOnButton("generatenewpracticeexambutton");
            if (status == false)
            {
                return;
            }
            


            for (int i = 0; i < totalExamQuestions; i++)
            {
                status = HTOAuto.AnswerCurrentQuestion();
                if (status == false)
                {
                    Console.WriteLine("Unable to answer this question. Exiting exam early");
                    // exitpracticeexamearlybutton
                    ClickOnButton("exitpracticeexamearlybutton");
                    break;
                }
                else
                {
                   // IncreamentQuestionCounter();
                }

                // RandomSleep(1000, 5000);
            }
        }


        public static void IncreamentQuestionCounter()
        {
            string qcountStr = mf.questionCountLabel.Text;
            int qcount = int.Parse(qcountStr);
            qcount++;
            mf.questionCountLabel.Text = qcount.ToString();
        }


        public static void RandomSleep(int minMs, int maxMs)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
         
                var sleepTime = rnd.Next(minMs, maxMs);
               Console.WriteLine("Sleeping for " + sleepTime + " ms");
                System.Threading.Thread.Sleep(sleepTime);
         

        }


        static IWebElement SelectQuestionSelection(string value)
        {
            return ReviewCourseOptions(ReviewCourseOptionsEmun.QuestionSelection, value);
        }


        static IWebElement SelectSkippedQuestions(string value)
        {
            return ReviewCourseOptions(ReviewCourseOptionsEmun.SkippedQuestions, value);
        }


        static IWebElement SelectSortOrder(string value)
        {
            return ReviewCourseOptions(ReviewCourseOptionsEmun.SortOrder, value);
        }


        static IWebElement SelectQuestionPool(string value)
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


        public static IWebElement getAnswerElement()
        {

            string html = (string)driver.ExecuteScript("return document.documentElement.outerHTML");
            string answer = GetCorrectAnswerText(html);

            if (answer == string.Empty)
            {

                Console.WriteLine("getAnswerElement() has failed. Returning null");
                return null;
            }


            var originalanswer = answer;

            // use double qoute " in xpath unless the answer line contain  doublequotes in the answer  
            string xPath = "//span[contains(text(), \"" + answer + "\") and contains(@class, 'unselectedAnswer')] ";
    
            // check for double quote in answer, if true, then use a single quote.
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


        /// <summary>
        /// this helper function will search HTO's HTML looking for the Question ID
        /// returns the question ID of the active question.
        /// If no ID is found, an empty string is returned.
        /// </summary>
        /// <param name="PageHTML"></param>
        /// <returns></returns>
        public static String getQuestionID(String PageHTML)
        {
            // use a regular expression to find the full question ID. ie
            // static Regex ReFindQuestionID = new Regex(@"\[....-.....\]", RegexOptions.Compiled | RegexOptions.IgnoreCase)
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


        /// <summary>
        /// Helper function to get year from question ID
        /// </summary>
        /// <param name="LongQid">LongQid</param>
        /// <returns></returns>
        public static String getQuestionYear(String LongQid)
        {
            string year = String.Empty;
            year = LongQid.Substring(0, 4);
            return year;
        }


        /// <summary>
        /// Helper function that gets the official Question ID that matches
        /// the ID found in the question pool text files
        /// </summary>
        /// <param name="LongQid"></param>
        /// <returns></returns>
        public static string getOfficalQid(String LongQid)
        {
            String officialQid = String.Empty;
            officialQid = LongQid.Substring(5, 5);
            return officialQid;
        }



        //TODO: Replace this with just ClickOnButton
        public static bool ClickOnSkipButton()
        {
            bool status = clickOnElement("skipquestionbutton");

            if (status == false)
            {
                Console.WriteLine("Unable to find Skip button");
                return false;
            }

            return true;

        }


        public static bool ClickOnButton(string buttonName)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            bool status = clickOnElement(buttonName);
            if (status == false)
            {
                ///MessageBox.Show("Unable to find " + buttonName + ". Exiting application now");
                Console.WriteLine("Unable to find " + buttonName + ". returning false");
                return false;
            }

            return true;
        }




        public static bool clickOnElement(string NameOfHTMLItem)
        {
            OpenQA.Selenium.IWebElement element = null;
            try
            {
                element = driver.FindElementByName(NameOfHTMLItem);
                element.Click();
            }
            catch (Exception)
            {
                return false;
            }


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



        // process the answer for quotes so it matches
        // the quote character matches what is found in the hmtl
        // TODO: this possibily could be fixed by a Search\Replace in the Question pool text file.
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


    public static class HTOMenuButtons
    {
        private const string mainMenu = "menubutton";
        private const string chooseTopics = "choosetopicsbutton";
        private const string study = "studybutton";
        private const string viewCourses = "gotoviewcoursesbutton";
        private const string practiceExam = "practiceexambutton";
        private const string myOptions = "edituseroptions"; //edituseroptions864561. What is number at end. Is it user id appended to end?
        private const string myAccount = "editaccountbutton";
        private const string purchase = "oldpurchasebutton";
        private const string topScores = "viewuserlistbutton";
        private const string studyHistory = "gotoviewstudyhistorybutton";
        private const string login = "loginbutton";
        private const string logout = "logoutbutton";

        public static string MainMenu
        {
            get
            {
                return mainMenu;
            }
        }


        public static string ChooseTopics
        {
            get
            {
                return chooseTopics;
            }
        }


        public static string Study
        {
            get
            {
                return study;
            }
        }

        
        public static string ViewCourses
        {
            get
            {
                return viewCourses;
            }
        }


        public static string PracticeExam
        {
            get
            {
                return practiceExam;
            }
        }


        public static string Login
        {
            get
            {
                return login;
            }
        }

        // Disabled because it throws an error
        //public static string MyOptions
        //{
        //    get
        //    {
        //        return myOptions;
        //    }
        //}


        public static string MyAccount
        {
            get
            {
                return myAccount;
            }
        }


        public static string Purchase
        {
            get
            {
                return purchase;
            }
        }

        
        public static string TopScores
        {
            get
            {
                return topScores;
            }
        }

        
        public static string StudyHistory
        {
            get
            {
                return studyHistory;
            }
        }


        public static string Logout
        {
            get
            {
                return logout;
            }
        }


    }

    public static class ButtonTemplate
    {
        private const string a = "";
        private const string b = "";
        private const string c = "";
        private const string d = "";
        private const string e = "";
        private const string f = ""; 
        private const string g = "";
        private const string h = "";
        private const string i = "";
        private const string j = "";
        private const string k = "";
        private const string l = "";

        public static string A
        {
            get
            {
                return a;
            }
        }


        public static string B
        {
            get
            {
                return b;
            }
        }


        public static string C
        {
            get
            {
                return c;
            }
        }


        public static string D
        {
            get
            {
                return d;
            }
        }


        public static string E
        {
            get
            {
                return e;
            }
        }


        public static string F
        {
            get
            {
                return e;
            }
        }



        public static string G
        {
            get
            {
                return g;
            }
        }


        public static string H
        {
            get
            {
                return h;
            }
        }


        public static string I
        {
            get
            {
                return i;
            }
        }


        public static string J
        {
            get
            {
                return j;
            }
        }


        public static string K
        {
            get
            {
                return k;
            }
        }

        public static string L
        {
            get
            {
                return l;
            }
        }


    }



    public static class PracticeExamButtons
    {
        private const string simulatedExam = "simulatedExamRadioButton";
        private const string trueRandom = "trueRandomExamRadioButton";
        private const string c = "focusExamRadioButtons";
        private const string d = "";
        private const string e = "";
        private const string f = ""; 
        private const string g = "";
        private const string h = "";
        private const string i = "";
        private const string j = "";
        private const string k = "";
        private const string l = "";

        public static string SimulatedExam
        {
            get
            {
                return simulatedExam;
            }
        }


        public static string TrueRandom
        {
            get
            {
                return trueRandom;
            }
        }


        public static string C
        {
            get
            {
                return c;
            }
        }


        public static string D
        {
            get
            {
                return d;
            }
        }


        public static string E
        {
            get
            {
                return e;
            }
        }


        public static string F
        {
            get
            {
                return e;
            }
        }



        public static string G
        {
            get
            {
                return g;
            }
        }


        public static string H
        {
            get
            {
                return h;
            }
        }


        public static string I
        {
            get
            {
                return i;
            }
        }


        public static string J
        {
            get
            {
                return j;
            }
        }


        public static string K
        {
            get
            {
                return k;
            }
        }

        public static string L
        {
            get
            {
                return l;
            }
        }


    }

}

