using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{

    public abstract class Line
    {

        internal List<String> content = new List<String>();

        internal Page page;

        internal Line(Page page)
        {
            this.page = page;
        }

        internal abstract int Length();

        internal abstract bool Overflow();

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

        internal abstract string SoftFill(string word);

        internal abstract void IntoText(StringBuilder text);

        public override String ToString()
        {
            StringBuilder text = new StringBuilder();
            this.IntoText(text);
            return text.ToString();
        }

    }
}
