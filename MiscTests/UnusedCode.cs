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
namespace MiscTests
{
    class UnusedCode
    {
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

    }
}
