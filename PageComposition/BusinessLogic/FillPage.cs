using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic {

  public class FillPage : Page {

    internal int wrap;

    internal FillPage(int wrap) {
      //sets the wrap length
      this.wrap = wrap;
      //creates a list of lines for the page
      content = new List<Line>();
      //and adds one to be used
      AddLine();
    }

    internal override void AddLine() {
      //creates a new line
      currentLine = new FillLine(this);
      //adds the content on the current line to it (should be empty?)
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
