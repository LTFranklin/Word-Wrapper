using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace BusinessLogic
{

    public class Page
    {
        internal List<Line> content;
        internal Line currentLine;
        internal int wrap;
        internal int wrapSoft;

        internal Page(int wrap, int wrapSoft)
        {
            //sets the wrap length
            this.wrap = wrap;
            //sets the softwrap length
            this.wrapSoft = wrapSoft;
            //creates a list of lines for the page
            content = new List<Line>();
            //and adds one to be used
            AddLine();
        }

        //used to get actual words from the input
        static internal List<string> FilterWords(List<string> rawWords)
        {
            List<string> words = new List<string>();
            string puncTest = @"([^a-z])";
            string vowelTest = @"([aeiou])";
            //adds each word to the page
            foreach (String w in rawWords)
            { 
                //if it contains a vowel and no punctuation
                if (!Regex.IsMatch(w, puncTest) && Regex.IsMatch(w, vowelTest))
                {
                    char last = ' ';
                    bool valid = false;
                    //if the word is more then 3 chars and there is less then 2 vowels
                    if (w.Length > 3 && Regex.Matches(w, vowelTest).Count < 2)
                    {
                        //its not a word
                        valid = false;
                    }
                    else
                    {
                        //for each vowel
                        foreach (Match m in Regex.Matches(w, vowelTest))
                        {
                            //store the vowel
                            char[] c = m.Groups[0].ToString().ToCharArray();
                            //and compare to the last vowel in the vowel (blank space is its the first vowel so it always passes)
                            //If its in alphabetical order
                            if ((int)c[0] >= (int)last)
                            {
                                //set the vowel to the last variable for later comparisions
                                last = c[0];
                                //set the word to be valid
                                valid = true;
                            }
                            //else the word is not valid as the vowels arent alphabetical
                            else
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                    //if the word is valid add it to the page
                    if (valid)
                    {
                        words.Add(w);
                    }
                }
            }
            return words;
        }

        //adds words in a normal wrap format
        internal void Add(List<String> words)
        {
            foreach (string s in words)
            {
                this.Add(s);
            }
        }
        //HOW THE HELL DO I KNOW IF THIS WORKS CORRECTLY?
        //adds words for setwrap
        internal void SetAdd(List<string> words)
        {
            //while words remain
            while(words.Count != 0)
            {
                bool added = false;
                //for each word
                for (int i = 0; i < words.Count; i++)
                {
                    //if the word could be added to the line
                    if(currentLine.Add(words[i]))
                    {
                        added = true;
                        //remove it from the list
                        words.Remove(words[i]);
                        //if there is less then 3 spaces remaining no words can fit on that line
                        if((wrap - currentLine.Length()) < 3)
                        {
                            //create a new line
                            AddLine();
                        }
                    }
                }
                //if nothings been added to the line
                if (!added)
                {
                    //create a new line
                    AddLine();
                }
            }
        }

        internal void Add(String word)
        {
            //if the word could not be added to the current line
            if (!currentLine.Add(word))
            {
                //create a new line and add the word to it
                AddLine();
                Add(word);
            }
        }

        internal void IntoText(StringBuilder text)
        {
            //adds a new line at the end of every line (added \r)
            foreach (Line line in content)
            {
                line.IntoText(text);
                text.Append("\n");
            }
        }

        public void ToFile(String fileName)
        {
            StringBuilder outText = new StringBuilder();
            //formats the extra parts (spaces and \n)
            IntoText(outText);
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(outText.ToString());
                }
            }
            catch (Exception e)
            {
                String message = "Failed to write output file: " + e.Message;
                Console.WriteLine(message);
                throw new Exception(message);
            }
        }

        internal void AddLine()
        {
            //creates a new line
            currentLine = new Line(this);
            //adds the content on the current line to it (should be empty?)
            content.Add(currentLine);
        }

        internal void SoftWrap()
        {
            bool changes = true;
            string word = null;
            string overfill;
            //if word ends as an empty string then no action was taken, thus everything is correctly positioned
            do
            {
                changes = false;
                word = null;
                //counts what line the method is working on
                int count = 0;
                foreach (Line l in content)
                {
                    //if the count is >= half the total number of lines
                    if (count > (content.Count() / 2))
                    {
                        //run the softfill method on that line, sending the last lines overfill with it
                        overfill = l.SoftFill(word);
                        //set the word to the lines overfill
                        word = overfill;
                        if(overfill != null)
                        {
                            changes = true;
                        }
                    }
                    count++;
                }
                if (word != null)
                {
                    AddLine();
                    Add(word);
                }
            }
            while (changes);
        }

        internal void FillAdjust()
        {
            foreach (Line l in content)
            {
                l.FillAdjust(wrap);
            }
        }

        internal void LineMoment(int moment)
        {
            int[] arr = new int[20];
            int counter = 0;
            foreach(Line l in content)
            {
                arr[counter] = l.MomentAdjust(wrap, moment);
                ++counter;
            }
        }

        internal bool Overflow()
        {
            foreach (Line line in content)
            {
                if (line.Overflow())
                {
                    return true;
                }
            }
            return false;
        }

        public override String ToString()
        {
            StringBuilder outText = new StringBuilder();
            IntoText(outText);
            return outText.ToString();
        }


    }
}
