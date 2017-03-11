using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic {

  public class FillPage : Page {

    internal int wrap;

    internal FillPage(int wrap) {
      this.wrap = wrap;
      content = new List<Line>();
      AddLine();
    }

    internal override void AddLine() {
      currentLine = new FillLine(this);
      content.Add(currentLine);
    }

    internal override bool Overflow() {
      foreach (Line line in content) {
        if (line.Overflow()) {
          return true;
        }
      }
      return false;
    }
  }
}
