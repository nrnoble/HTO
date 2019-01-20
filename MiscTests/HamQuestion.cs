using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HTO;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace HTO
{
    class HamQuestion
    {

        static readonly Regex ReFindQuestionId = new Regex(@"\[....-.....\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private AllOfficialQuestionsText _questionsPools;

        private string _CorrectAnswer = String.Empty;
        private string _year = String.Empty;
        private string _qId = String.Empty;
        private string _LongQID = String.Empty;
        private string _answerLetter = String.Empty;
        private string _rawQuestionText = String.Empty;
        private string _pageHTML = String.Empty;
        private string _activeQuestionPool = string.Empty;

    

        public HamQuestion(string pageHtml, AllOfficialQuestionsText questionsPools)
        {
            this._pageHTML = pageHtml;
            this._questionsPools = questionsPools;
        }

     


        private string RawQuestionText
        {
            get
            {
                if (this._rawQuestionText == string.Empty)
                {
                    _rawQuestionText = GetRawQestionText();
                }

                return this._rawQuestionText;
            }
        }

        private string LongQiD
        {
            get
            {
                if (this._LongQID == String.Empty)
                {
                   this._LongQID = this.getLongQuestionID();
                }

                return this._LongQID;
            }

        }

        public string HamQuestionID
        {
            get
            {
                if (this._qId == string.Empty)
                {
                    _qId = getOfficialQuestionID();
                }

                return _qId;
            }

        }

        private string Year
        {
            get
            {
                if (this._year == string.Empty)
                {
                    this._year = getQuestionYear();

                }

                return this._year;
            }

        }

        private string ActiveQuestionPool
        {
            get
            {
                if (this._activeQuestionPool == string.Empty)
                {
                    this._activeQuestionPool = SetActiveQuestionPool();
                }

                return _activeQuestionPool;
            }

        }


        public string Question
        {
            get
            {
                var qText = this.RawQuestionText;
                var rgMatchLines = new Regex(@"^.*$", RegexOptions.Multiline);
                //string answerLine = Regex.Match(this.RawQuestionText, this.AnswerLetter + @"\.(.+)").Groups[1].Value.Trim();
                var results = rgMatchLines.Match(qText);
                return results.NextMatch().Value;
            }
        }

        public string Answer
        {
            get
            {
                if (this._CorrectAnswer == string.Empty)
                {
                    this._CorrectAnswer = GetCorrectAnswerText();
                }

                return this._CorrectAnswer;
            }
        }

        public string AnswerLetter
        {
            get
            {
                if (this._answerLetter == string.Empty)
                {
                    this._answerLetter = getAnswerLetter();
                }
                return this._answerLetter;
            }

            
        }

        private string getAnswerLetter()
        {

            string answerLetter = Regex.Match(this.RawQuestionText, @"\(([^)]*)\)").Groups[1].Value;
            Console.WriteLine("Going to select answer: " + answerLetter);
            return answerLetter;
        }

        public string QuestionFullText
        {
            get { return RawQuestionText; }
        }

        private string GetRawQestionText()
        {
        

            // Get the question question ID that will match the official ID in the question pool.
            //string Qid = this.LongQiD;
            Console.WriteLine("HamQuestionID: " + this.HamQuestionID);
            //string RxFindStr = Qid + @"\(.\).+?~~";
            string rxFindStr = this.HamQuestionID + @".+?\(.+?~~";
            //Console.WriteLine(RxFindStr);
            Regex rxfindQuestionInPool = new Regex(rxFindStr, RegexOptions.Singleline);

            // Location all Question IDs 
            MatchCollection matches = rxfindQuestionInPool.Matches(this.ActiveQuestionPool);

            String questionFullText = string.Empty;
            if (matches.Count == 0)
            {
                // TODO: Search for OK button, and click it then restart answering question.
                //       If no OK button, then select skip button.
                //       if no skip button, the hault execution.
                MessageBox.Show("Match count is zero for finding '" + this.HamQuestionID + "' in the question pool");
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

            return questionFullText;
        }

        /// <summary>
        /// this helper function will search HTO's HTML looking for the Question ID
        /// returns the question ID of the active question.
        /// If no ID is found, an empty string is returned.
        /// </summary>
        /// <param name="PageHTML"></param>
        /// <returns></returns>
        public String getLongQuestionID()
        {
            // use a regular expression to find the full question ID. ie
            // static Regex ReFindQuestionID = new Regex(@"\[....-.....\]", RegexOptions.Compiled | RegexOptions.IgnoreCase)
            MatchCollection matches = ReFindQuestionId.Matches(this._pageHTML);


            String match1 = string.Empty;
            String match2 = string.Empty;
            String LongQID = String.Empty;

            if (matches.Count == 0)
            {
                return string.Empty;
            }

            var m1 = matches[0];
            match1 = m1.Value;
            LongQID = match1;

            if (matches.Count > 1)
            {
                var m2 = matches[1];
                match2 = m2.Value;
                LongQID = match2;
            }
            LongQID = LongQID.Replace('[', ' ');
            LongQID = LongQID.Replace(']', ' ');
            LongQID = LongQID.Trim();

            return LongQID;

        }
        
        private string getOfficialQuestionID()
        {
            return this.LongQiD.Substring(5, 5);
        }
        
        private string SetActiveQuestionPool()
        {
           // string questionYear = getQuestionYear();
            if (this.Year == string.Empty)
            {
                // if Year is empty this get executed something major has gone wrong.

                Console.WriteLine("No valid year found. Exiting application");
                Application.Exit();
            }

            {
                // this section selects the active question pool based on 'year' fount in the questionID

                if (this.Year == "2018")
                {
                    this._activeQuestionPool =  this._questionsPools.Tech;
                    Console.WriteLine("Year: " + this.Year + ". Setting to Tech pool questionsPools");
                }
                else if (this.Year == "2015")
                {
                    this._activeQuestionPool = this._questionsPools.General;
                    Console.WriteLine("Year: " + this.Year + ". Setting to General pool questionsPools");
                }
                else if (this.Year == "2016")
                {
                    this._activeQuestionPool = this._questionsPools.Extra;
                    Console.WriteLine("Year: " + this.Year + ". Setting to Extra pool questionsPools");
                }
                else
                {
                    MessageBox.Show("The current year is invalid: " + this.Year + ".  Exit app here.");
                    System.Windows.Forms.Application.Exit();
                }
            }

            return this._activeQuestionPool;
        }

        private String getQuestionYear()
        {
            return this.LongQiD.Substring(0, 4);
        }

        public string GetCorrectAnswerText()
        {

            
            Console.WriteLine("longID Found: " + this.LongQiD);
            if (this.LongQiD == string.Empty)
            {
                Console.WriteLine("longID not Found: " + this.LongQiD);
                return string.Empty;
            }

            // Strip off the Alpha Option (A,B,C,D) from line
            string answerLine = Regex.Match(this.RawQuestionText, this.AnswerLetter + @"\.(.+)").Groups[1].Value.Trim();
            Console.WriteLine("Correct answer: " + answerLine);

            return answerLine;

        }

    }


}
