using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{

    public class FillPage : Page
    {

        internal int wrap;
        internal int wrapSoft;

        internal FillPage(int wrap, int wrapSoft)
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

        internal override void AddLine()
        {
            //creates a new line
            currentLine = new FillLine(this);
            //adds the content on the current line to it (should be empty?)
            content.Add(currentLine);
        }

        internal override void reorganise()
        {

            string word = null;
            string overfill;
            //if word ends as an empty string then no action was taken, thus everything is correctly positioned
            do
            {
                word = null;
                //counts what line the method is working on
                int count = 1;
                foreach (Line l in content)
                {
                    //if the count is >= half the total number of lines
                    if (count > (content.Count() / 2))
                    {
                        //run the softfill method on that line, sending the last lines overfill with it
                        overfill = l.SoftFill(word);
                        //set the word to the lines overfill
                        word = overfill;
                    }
                    count++;
                }
                if (word != null)
                {
                    AddLine();
                    Add(word);
                }
            }
            while (word != null);
        }

        internal override bool Overflow()
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
    }
}
