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

            



        }



    }
}
