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

        //only works for single spaces
        internal void FillAdjust(int wrap)
        {
            if (content.Count != 1)
            {
                //finds the length of the line (could move this into the calling method and send it to the above mehtod instead if it is the correct length)
                int length = content.Count() - 1;
                foreach (string s in content)
                {
                    length += s.Length;
                }
                //if the length is shorter then the wrap
                if (length < wrap)
                {
                    //store the diference as the number of sapces needing to be added
                    int spaceCount = wrap - length;
                    int last, highest, current, pos;
                    //while there is still spaces to be added
                    while (spaceCount != 0)
                    {
                        last = highest = current = pos = 0;
                        //for each word
                        for (int i = 0; i < content.Count(); ++i)
                        {
                            //find the number of vowels and add it to the current total
                            int wordVowels = VowelCount(content[i]);
                            current += wordVowels;
                            //if the value isnt the first word, is higher then the current higest and there isnt a space between already
                            //try making the ends with a variable that increase the numebr spaces in it each time
                            if (last !=0 && current > highest && !content[i - 1].EndsWith(" "))
                            {
                                //track the position
                                pos = i - 1;
                                highest = current;
                            }
                            //remove the last value and strore the current value as last for the next word
                            current -= last;
                            last = wordVowels;
                        }
                        //add a space at the end of the tracked postion
                        content[pos] += " ";
                        spaceCount -= 1;
                    }
                }
            }
        }

        internal int VowelCount(string word)
        {
            int count = 0;
            foreach (char c in word)
            {
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                {
                    count += 1;
                }
            }
            return count;
        }

        //doesnt need to return(remove before submission)
        //assumes spaces should be equal where possible (check this is true)
        internal int MomentAdjust(int wrap, int mPos)
        {
            int moment = CalculateMoment(mPos);
            if (content.Count != 1)
            {
                int length = this.Length();

                int pos = 0;

                //if the moment is less then 0 and the length is shorter the the wrap 
                while (moment < 0 && length < wrap)
                {
                    content[pos] += " ";
                    ++length;
                    //increments the position if it is less then the line length and the moment position, else it resets it to the start of the line
                    if (pos < content.Count() - 2 && pos < mPos)
                    {
                        ++pos;
                    }
                    else
                    {
                        pos = 0;
                    }
                    moment = CalculateMoment(mPos);
                }
            }
            return moment;
        }

        internal int CalculateMoment(int mPos)
        {
            int val = 0;
            //position of the word on the line
            int wPos = 0;
            foreach(string s in content)
            {
                val += CalculateWordMoment(s, mPos, wPos);
                //used to account for the standard spaces between words
                if(wPos == 0)
                {
                    ++wPos;
                }
                wPos += s.Length;
            }

            return val;
        }

        internal int CalculateWordMoment(string word, int mPos, int wPos)
        {
            //moves all chars an array and sets them to upper case
            char[] inArr = word.ToUpper().ToCharArray();

            int val = 0;
            //for each letter in the word
            for (int i = 0; i < inArr.Length; ++i)
            {
                if (64 < (int)inArr[i] && (int)inArr[i] < 90)
                {
                    //increase the value by the letter's value (assumes unicode) times the distance from the column (words pso on the line + chars pos in the word - moment)
                    val += (((int)inArr[i] - 64) * (wPos + i - mPos));
                }
            }

            return val;
        }



        public override String ToString()
        {
            StringBuilder text = new StringBuilder();
            this.IntoText(text);
            return text.ToString();
        }
    }
}
