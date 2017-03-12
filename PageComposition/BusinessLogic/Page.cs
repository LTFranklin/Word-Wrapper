using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BusinessLogic
{

    public abstract class Page
    {

        internal List<Line> content;

        internal Line currentLine;

        internal abstract bool Overflow();

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

        internal abstract void AddLine();

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

        public override String ToString()
        {
            StringBuilder outText = new StringBuilder();
            IntoText(outText);
            return outText.ToString();
        }

    }
}
