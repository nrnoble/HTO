using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageShare
{
    public static class Program
    {

        static ChromeDriver driver = new ChromeDriver();
        static string baseUrl = "http://www.image-share.com/upload/1000/";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            
            // var driver = new ChromeDriver();
            var imageUrl = baseUrl + "274.jpg";
            driver.Navigate().GoToUrl(imageUrl);

            for (int i = 0; i < 278; i++)
            {
             imageUrl = baseUrl + i + ".jpg";
             driver.Navigate().GoToUrl(imageUrl);
             
            }

        }

        public static void GetOneImage(int imageNumber)
        {
            var imageUrl = baseUrl + imageNumber + ".jpg";
            driver.Navigate().GoToUrl(imageUrl);
        }


    }
}
