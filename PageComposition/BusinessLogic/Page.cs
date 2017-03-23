using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

        internal void Add(List<String> words)
        {
            //adds each word to the page
            foreach (String w in words)
            {
                this.Add(w);
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
                text.Append("\r\n");
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
