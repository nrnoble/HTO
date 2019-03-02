using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
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
using Keys = OpenQA.Selenium.Keys;
using Microsoft.VisualBasic;
using AutoHotkey.Interop;

namespace HTO
{

    public static partial class HTOAuto
    {
        static readonly Regex ReFindQuestionId = new Regex(@"\[....-.....\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static ChromeDriver driver = null;
        //static FirefoxDriver driver = null;
        static MainForm mf = null;
        private static HamQuestion hq;
        
        const string HtoLoginUrl = "https://www.hamradiolicenseexam.com/login.htm";
        const string TechQuestionPath = @".\TechPool.txt";
        const string GeneralQuestionPath = @".\GeneralPool.txt";
        const string ExtraQuestionPath = @".\ExtraPool.txt";

        static AllOfficialQuestionsText QuestionPool = new AllOfficialQuestionsText(TechQuestionPath, GeneralQuestionPath, ExtraQuestionPath);
        static string _selectedQuestionPool = string.Empty;

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


        public static void StartAutomation()
        {

            // Using the Chrome browser
            Console.WriteLine("Starting Brower");
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            
            // get User & Pwd from Main form and login into HTO
            var user = mf.userID.Text.Trim();
            var password = mf.password.Text.Trim();
            Login(user, password, HtoLoginUrl);
            
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
            Console.WriteLine("Goto " + loginURL);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 12);
            driver.Navigate().GoToUrl(loginURL);

            IWebElement body = driver.FindElement(By.TagName("body"));
            body.SendKeys(OpenQA.Selenium.Keys.Control + 'T');

            // Get the elements of the login elements from HTML

            OpenQA.Selenium.IWebElement LoginBox = driver.FindElementByName("loginemailaddress");
            OpenQA.Selenium.IWebElement LoginPassword = driver.FindElementByName("loginpassword");
            OpenQA.Selenium.IWebElement LoginButton = driver.FindElementByName(HTOMenuButtons.Login);
            
            
            //Execute auto login
            {
                Console.WriteLine("logging onto HTO as " + userEmail);
                actions.SendKeys(LoginBox, userEmail);
                actions.SendKeys(LoginPassword, password);
                actions.Click(LoginButton);
                actions.Perform();
                
            }
        }

        public static void LoginTest()
        {
            
            var control = Strings.Chr(20);
            var loginURL = "https://www.google.com";

            // Using the Chrome browser
            Console.WriteLine("Starting Brower");
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            

            // used for sending clicks and and characters
            Actions actions = new Actions(driver);


            // set driver timeout and launch browser

            Console.WriteLine("Goto: " + loginURL);
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 12);
            driver.Navigate().GoToUrl(loginURL);

            IWebElement body = driver.FindElement(By.Name("q"));
            body.SendKeys("HAM T8B06" + Keys.Enter);

            // IWebElement body2 = driver.FindElement(By.Name("q"));
            // body2.SendKeys(control.ToString() + 't');
           // driver.Action.key_down(:control)
           //     .Send_keys("a")
           //     .Key_up(:control)
           //     .perform

           var actionX = new Actions(driver).KeyDown(Keys.LeftControl).SendKeys("T").KeyUp(Keys.LeftControl).Build();
           actionX.Perform();
           var ahk = AutoHotkeyEngine.Instance;

           ahk.ExecRaw("Send, ^u");
           ahk.ExecRaw("Send, `t`t`t`t`t`t`t");
           ahk.ExecRaw("MsgBox, Hello World!");
           

            // Get the elements of the login elements from HTML

            //OpenQA.Selenium.IWebElement LoginBox = driver.FindElementByName("loginemailaddress");
            //OpenQA.Selenium.IWebElement LoginPassword = driver.FindElementByName("loginpassword");
            //OpenQA.Selenium.IWebElement LoginButton = driver.FindElementByName(HTOMenuButtons.Login);


            ////Execute auto login
            //{
            //    Console.WriteLine("logging onto HTO as " + userEmail);
            //    actions.SendKeys(LoginBox, userEmail);
            //    actions.SendKeys(LoginPassword, password);
            //    actions.Click(LoginButton);
            //    actions.Perform();

            //}

            Console.WriteLine("Test Done");
        }


        public static void SelectMainTopics()
        {
            // click on Choose topic Menu button on left of screen
            // OpenQA.Selenium.IWebElement choosetopicsbutton = driver.FindElementByName("choosetopicsbutton");
            // choosetopicsbutton.Click();


            Console.WriteLine("Selecting main menu button :"+ HTOMenuButtons.ChooseTopics);
            ClickOnButton(HTOMenuButtons.ChooseTopics);


            // get the selected radio Button
            var checkedButton = mf.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            //Get the selected Pool 
            var poolValue = "218";
            string selectedTopicMsg = string.Empty;
            switch (checkedButton.Text)
            {

                case "Tech":
                    poolValue = "218";

                    selectedTopicMsg = "Selecting Tech topics";
                    break;

                case "General":
                    poolValue = "315";
                    selectedTopicMsg = "Selecting General topics";
                    break;

                case "Extra":
                    poolValue = "416";
                    selectedTopicMsg = "Selecting Extra topics";
                    break;

                default:
                    break;
            }

            // Console.WriteLine(checkedButton.Text + "= " + poolValue);



            // Initialize by  unchecking all HTO topics 
            var elements = driver.FindElements(By.Name("choosetopic"));

            Console.WriteLine("Deselecting active topics");
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
                    Console.WriteLine(selectedTopicMsg);
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
            //hq = new HamQuestion(driver.PageSource, QuestionPool);
            
            //hq = new HamQuestion();
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
            //string answer = GetCorrectAnswerText(html);

            hq = new HamQuestion(driver.PageSource, QuestionPool);
            Console.WriteLine("Question: " + hq.Question);

            string answer = hq.Answer;

            if (hq.Answer == string.Empty)
            {

                Console.WriteLine("getAnswerElement() has failed. Returning null");
                return null;
            }


            //var originalanswer = answer;

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


    public class AllOfficialQuestionsText
    {
        public AllOfficialQuestionsText(string techFilePath, string generalFilePath, string extraFilePath)
        {
            Tech = File.ReadAllText(techFilePath);
            General = File.ReadAllText(generalFilePath); 
            Extra = File.ReadAllText(extraFilePath);
        }

        public string Tech { get; }
        public string General { get; } 
        public string Extra { get; }

    }

}

