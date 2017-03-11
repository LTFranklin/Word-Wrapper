using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {

  public enum Format { Fill, FillSoft, FillAdjust, LineMoment, FillSet };

  public class PageInput {

    public Format format;

    public int wrap = 0;

    public int wrapSoft = 0;

    public int columnMoment = 0;

    public List<String> words;

    public PageInput() {
      words = new List<String>();
    }

    public PageInput(Format format, int wrap, int wrapSoft, int columnMoment, List<String> words) {
      this.format = format;
      this.wrap = wrap;
      this.wrapSoft = wrapSoft;
      this.columnMoment = columnMoment;
      this.words = words;
    }

    //finds which format is specified
    public Page Compose() {
      switch (format) {
        case Format.Fill: {
            //creates a new page
            FillPage page = new FillPage(wrap);
            //adds the words onto the page
            page.Add(words);
            return page;
          }
        case Format.FillSoft: {
            return null;
          }
        default: {
            throw new Exception("Unknown format.");
          }
      }
    }
  }

}
