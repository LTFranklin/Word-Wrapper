using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Diagnostics;
using BusinessLogic;
using IO;

namespace PageComposition {

  class Program {

    static void Main(String[] args) {
      try {  // do not remove this try-catch statement, do not add any code outside try-block
        //Reads in from the file
        PageInput pageInput = PageInputXml.LoadInput("input.xml");
        //does the work
        Page page = pageInput.Compose();
        //Outputs to file
        page.ToFile("page.txt");
      }
      catch (Exception e) {
        // do not modify the code in this catch block except to comment two lines at end of block
        Console.WriteLine("Unhandled exception: " + e.Message);
        // comment following two lines before final build and submission
        //Console.WriteLine("Press any key to exit program.");
        //Console.ReadKey();
      }
    }
  }
}
