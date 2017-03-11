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
      foreach (String word in content) {
        result += word.Length;
        result += 1;  
      }
      return result;  
    }

    internal override bool Overflow() {
      return this.WrapOverflow();
    }

    internal bool WrapOverflow() {
      return Length() >= ((FillPage)page).wrap;
    }

    internal override void IntoText(StringBuilder text) {
      foreach (String word in content) {
        text.Append(word.ToString());
        text.Append(" ");
      }
      text.Remove(text.Length - 1, 1);
    }

  }
}
