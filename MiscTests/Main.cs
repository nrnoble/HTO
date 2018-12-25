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
    public static class HTOCore
    {
        static Regex ReFindQuestionID = new Regex(@"\[....-.....\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static ChromeDriver driver = null;
        static MainForm mf = null;

        static int skipcount = 0;

        const string HTO_LOGIN_URL = "https://www.hamradiolicenseexam.com/login.htm";
        static string currentQuestionPool = string.Empty;
        static string techQuestions = File.ReadAllText(@".\TechPool.txt");
        static string GeneralQuestions = File.ReadAllText(@".\GeneralPool.txt");
        static string ExtraQuestions = File.ReadAllText(@".\ExtraPool.txt");

      

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            //Random rnd = new Random(DateTime.Now.Millisecond);
            //for (int i = 0; i < 10; i++)
            //{
            //    var sleepTime = rnd.Next(500, 5000);

            //    Console.WriteLine("Sleeping for " + sleepTime + " ms");
            //    System.Threading.Thread.Sleep(sleepTime);
            //}


            //Application.SetCompatibleTextRenderingDefault(false);
            //mf = new MainForm();
            //Application.Run(mf);

            HTOAuto.Initialization();

            var test = "asdfdfa";
            /// HTOAuto.StartAutomation();






        }

        /// <summary>
        /// Application main loop after application low level app initialization
        /// </summary>
        /// <param name="mf">Main form of application that appears at startup</param>
        //public static void MainLoop(MainForm mf)
        //{

        //    // Using the Chrome broswer
        //    driver = new ChromeDriver();
            


        //    // get User & Pwd from Main form and login into HTO
        //    var user = mf.userID.Text.Trim();
        //    var password = mf.password.Text.Trim();
        //    Login(user, password, HTO_LOGIN_URL);
            
        //}

        //public static void Login(string userEmail, string password, string loginURL)
        //{
        //    //var sleepTime1 = 25;
        //    var enterKey = "\r\n";
        //    var tabKey = "\t";

        //    Actions actions = new Actions(driver);

           

        //    // set driver timeout and launch browser
        //    driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
        //    driver.Navigate().GoToUrl(loginURL);




        //    var kbd = driver.Keyboard;
            

        //    OpenQA.Selenium.IWebElement LoginBox = driver.FindElementByName("loginemailaddress");
        //    OpenQA.Selenium.IWebElement LoginPassword = driver.FindElementByName("loginpassword");
        //    OpenQA.Selenium.IWebElement LoginButton = driver.FindElementByName("loginbutton");
           
        //    actions.SendKeys(LoginBox, userEmail);
        //    actions.SendKeys(LoginPassword, password);
        //    actions.Click(LoginButton);
        //    actions.Perform();

        //    OpenQA.Selenium.IWebElement choosetopicsbutton = driver.FindElementByName("choosetopicsbutton");
        //    choosetopicsbutton.Click();

        //    var checkedButton = mf.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

        //    var poolValue = "218";
        //    switch (checkedButton.Text)
        //    {
            
        //        case "Tech":
        //            poolValue = "218";
        //            break;

        //        case "General":
        //            poolValue = "315";
        //            break;

        //        case "Extra":
        //            poolValue = "416";
        //            break;

        //        default:
        //            break;
        //    }

        //    Console.WriteLine(checkedButton.Text + "= "+ poolValue);

        //    var elements = driver.FindElements(By.Name("choosetopic"));
        //    foreach (var elment in elements)
        //    {

        //        if (elment.Selected == true)
        //        {
        //            elment.Click();
        //        }
        //    }

        //    foreach (var elment in elements)
        //    {
        //        if (elment.GetAttribute("Value") == poolValue)
        //        {
        //            elment.Click();
        //            break;
        //        }
        //    }






        //    // LoginBox.Text = userEmail;
        //    // var inputValueAtrib = LoginBox.GetAttribute("value");
        //    // driver.ExecuteScript("document.getElementsByTagName('loginemailaddress').setAttribute('value', 'nrnoble@hotmail.com')");
        //    // driver.ExecuteScript("document.getElementsByTagName('loginemailaddress').setAttribute('value', 'nrnoble@hotmail.com')");
        //    // html/body/form/table/tbody/tr/td[2]/main/blockquote/table/tbody/tr/td[1]/p[3]/input
        //    // var obj = driver.ExecuteScript("document.getElementsByName('loginemailaddress')");
        //    // var obj = driver.ExecuteScript("document.getElementsByName('loginemailaddress')");


        //    // System.Threading.Thread.Sleep(sleepTime1);
        //    // kbd.SendKeys(tabKey);
        //    // System.Threading.Thread.Sleep(sleepTime1);
        //    // kbd.SendKeys(password);
        //    // System.Threading.Thread.Sleep(sleepTime1);
        //    // kbd.SendKeys(tabKey);
        //    // System.Threading.Thread.Sleep(sleepTime1);
        //    // kbd.SendKeys(enterKey);
        //    // System.Threading.Thread.Sleep(sleepTime1);


        //}




        //static float NextFloat(Random random)
        //{
        //    double mantissa = (random.NextDouble() * 2.0) - 1.0;
        //    // choose -149 instead of -126 to also generate subnormal floats (*)
        //    double exponent = Math.Pow(2.0, random.Next(-126, 128));
        //    return (float)(mantissa * exponent);
        //}

    }
}
