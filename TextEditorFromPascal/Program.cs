using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
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

namespace TextEditorFromPascal
{
    static class ProgramX
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void MainX()
        {
            string baseUrl = "http://www.image-share.com/upload/1038/";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
