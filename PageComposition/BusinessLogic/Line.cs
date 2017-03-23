using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{

    public class Line
    {

        internal List<String> content = new List<String>();

        internal Page page;

        internal Line(Page page)
        {
            this.page = page;
        }
        
        internal bool Add(String word)
        {
            //adds the word to the page
            content.Add(word);
            //if the line length is not too big
            if (!Overflow())
            {
                //its fine
                return true;
            }
            else
            {
                //remove the word and return that it was a problem
                content.RemoveAt(content.Count - 1);
                return false;
            }
        }

        internal int Length()
        {
            int result = 0;
            //adds the length of each word to the counter, +1 for the space
            foreach (String word in content)
            {
                result += word.Length;
                result += 1;
            }
            //removes the last space on the line as it would be removed if it was the last character
            return result - 1;
        }
        //remove this, its uneeded
        internal bool Overflow()
        {
            //if there is only 1 word on the line it is always okay
            if (content.Count() == 1)
            {
                return false;
            }
            else
            {
                //returns true if the length is greater than or equal to the word wrap
                return Length() > ((Page)page).wrap;
            }
        }

        //used to reorganise the string according to the soft wrap
        internal string SoftFill(string word)
        {
            //inserts any word that overfilled the last line
            if (word != null)
            {
                content.Insert(0, word);
            }
            //checks if the length is longer then the softwrap
            if ((Length() > ((Page)page).wrapSoft) && content.Count != 1)
            {
                //if so it returns the last item in the list
                string overfill = content.Last();
                content.Remove(content.Last());
                return overfill;
            }
            return null;
        }

        internal void IntoText(StringBuilder text)
        {
            //adds a space after every word
            foreach (String word in content)
            {
                text.Append(word.ToString());
                text.Append(" ");
            }
            //removes the last space
            text.Remove(text.Length - 1, 1);
        }

        public override String ToString()
        {
            StringBuilder text = new StringBuilder();
            this.IntoText(text);
            return text.ToString();
        }
    }
}
