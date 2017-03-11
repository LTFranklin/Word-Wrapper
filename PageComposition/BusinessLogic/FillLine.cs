using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic {

  public class FillLine : Line {

    internal FillLine(FillPage page) : base(page) {
    }

    internal override int Length() {
      int result = 0;
      //adds the length of each word to the counter, +1 for the space
      foreach (String word in content) {
        result += word.Length;
        result += 1;  
      }
      return result;  
    }
        //remove this, its uneeded
    internal override bool Overflow() {
      return this.WrapOverflow();
    }

    internal bool WrapOverflow() {
      //returns true if the length is greater then the word wrap
      return Length() >= ((FillPage)page).wrap;
    }

    internal override void IntoText(StringBuilder text) {
      //adds a space after every word
      foreach (String word in content) {
        text.Append(word.ToString());
        text.Append(" ");
      }
      //removes the last space
      text.Remove(text.Length - 1, 1);
    }

  }
}
