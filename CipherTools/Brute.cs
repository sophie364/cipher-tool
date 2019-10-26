using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace CipherTools
{
    public partial class Brute : Form
    {
        public static string keywordsFile = "keywords.txt"; // text file storing the keywords

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;

        private void SetFont()
        {
            byte[] fontData = Properties.Resources.RobotoCondensed_Light;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.RobotoCondensed_Light.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.RobotoCondensed_Light.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 16.0F);

            label1.Font = myFont;

            myFont = new Font(fonts.Families[0], 14.0F);
            textBoxOut.Font = myFont;
        }

        public Brute(string[] solutions, string originalText)
        {
            InitializeComponent();
            SetFont();

            // the text that the user entered is displayed first and new lines are made
            textBoxOut.Text += "Original text: " + originalText + "\r\n\r\n";

            DisplaySolutions(solutions);
        }
       
        private void DisplaySolutions(string[] solutions)
        {
            bool potentialAnswerFound = false; // will turn true if any of the keywords are spotted in any of the solutions

            List<string> potentialAnswers = new List<string>(); // will store solutions that contain any of the keywords
            List<string> compareAnswers = new List<string>(); // compares potential answers
            List<int> commonWordCount = new List<int>(); // stores how often the keywords appear in each solution

            List<string> keywords = new List<string>(); // will store each keyword in the text file

            if (File.Exists(keywordsFile)) // if there is a keyword file to read from
            {
                using (FileStream file = new FileStream(keywordsFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        {
                            keywords.Add(reader.ReadLine()); // each keyword is stored in the list
                        }
                        reader.Close();
                    }
                    file.Close();
                }
            }

            for (int i = 0; i < solutions.Length; i++)
            {
                textBoxOut.Text += (i + 1).ToString() + ". " + solutions[i] + "\r\n\r\n"; // displays the key/solution number
                                                                                             // and the actual solution

                string currentSolution = solutions[i].ToUpper(); // temporarily stores each solution
                currentSolution = Regex.Replace(currentSolution, @"\s", ""); 
                
                // checks each solution for keywords and counts how many times they occur
                if (File.Exists(keywordsFile))
                {
                    foreach (string keyword in keywords)
                    {
                        if (currentSolution.Contains(keyword.ToUpper()))
                        {
                            bool alreadyAdded = false; // checks if the solution has already been added to the list of possible solutions
                            foreach (string answer in compareAnswers)
                            {
                                string currentAnswer = Regex.Replace(answer, @"\s", "");
                                if (currentAnswer == currentSolution)
                                {
                                    alreadyAdded = true;

                                    int wordCount = Regex.Matches(Regex.Escape(currentSolution), keyword.ToUpper()).Count;
                                    int index = compareAnswers.IndexOf(answer);

                                    commonWordCount[index] += wordCount; // commonWordCount stores the total occurences of all keywords
                                }
                            }
                            if (alreadyAdded == false)
                            {
                                potentialAnswerFound = true; // this will cause a new screen to open later displaying the suggested solutions
                                // counts how many times the keyword appears in the text
                                commonWordCount.Add(Regex.Matches(Regex.Escape(currentSolution), keyword.ToUpper()).Count);

                                potentialAnswers.Add((i+1).ToString() + ". " + solutions[i]);
                                compareAnswers.Add(solutions[i].ToUpper());
                            }
                        }
                    }
                }
            }

            if (potentialAnswerFound == true)
            {
                PotentialAnswers potAns = new PotentialAnswers(potentialAnswers, commonWordCount);
                potAns.Show();
                potAns.TopMost = true;
            }
        }

        private void textBoxOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) // Ctrl + A shortcut for select all
            {
                textBoxOut.SelectAll();

                // prevents 'ding' noise when Ctrl + A is pressed
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
