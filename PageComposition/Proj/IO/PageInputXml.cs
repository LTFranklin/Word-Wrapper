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

namespace IO {
  public class PageInputXml {

    static String delimStr = " ";
    static char[] delimChars = delimStr.ToCharArray();


    public static PageInput LoadInput(String pathname) {
      XmlDocument pagInputXml = new XmlDocument();
      try {
        using (TextReader reader = File.OpenText(pathname)) {
          pagInputXml.Load(reader);
        }
      }
      catch (Exception e) {
        Console.WriteLine(e.Message);
      }
      PageInput pageIn = new PageInput();
      XmlNodeList formatnl = pagInputXml.SelectNodes("//pageinput/format");
      if (formatnl.Count == 1) {
        String formatstr = formatnl[0].FirstChild.Value;
        switch (formatstr) {
          case "Fill": {
              pageIn.format = Format.Fill;
              break;
            }
          case "FillSoft": {
              pageIn.format = Format.FillSoft;
              break;
            }
          case "FillAdjust": {
              pageIn.format = Format.FillAdjust;
              break;
            }
          case "LineMoment": {
              pageIn.format = Format.LineMoment;
              break;
            }
          case "FillSet": {
              pageIn.format = Format.FillSet;
              break;
            }
          default: {
              throw new Exception("Unknown format: " + formatstr);
            }
        }
      }
      else {
        throw new Exception("Xml error");
      }
      XmlNodeList wrapnl = pagInputXml.SelectNodes("//pageinput/wrap");
      if (formatnl.Count == 1) {
        pageIn.wrap = Int32.Parse(wrapnl[0].InnerText);
      }
      else {
        throw new Exception("Xml error");
      }
      XmlNodeList wrapsoftnl = pagInputXml.SelectNodes("//pageinput/wrapsoft");
      if (wrapsoftnl.Count == 1) {
        pageIn.wrapSoft = Int32.Parse(wrapsoftnl[0].InnerText);
      }
      else {
        throw new Exception("Xml error");
      }
      XmlNodeList columnmomentnl = pagInputXml.SelectNodes("//pageinput/columnmoment");
      if (columnmomentnl.Count == 1) {
        pageIn.columnMoment = Int32.Parse(columnmomentnl[0].InnerText);
      }
      else {
        throw new Exception("Xml error");
      }
      XmlNodeList wordsnl = pagInputXml.SelectNodes("//pageinput/words");
      String text = wordsnl[0].InnerText;
        String[] textParts;
        textParts = text.Split(delimChars);
        for (int i = 0; i < textParts.Length; i++ ) {
          String s = textParts[i];
          pageIn.words.Add(s);
        }
      return pageIn;
    }
  }
}
