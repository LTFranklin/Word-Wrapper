using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;

namespace UnitTest {
  [TestClass]
  public class UnitTest1 {
    [TestMethod]
    public void TestMethod1() {
      PageInput pageIn = new PageInput(Format.Fill, 5, 0, 0, new List<String>(new String[] { "abc", "abc" }));
      Page page = pageIn.Compose();
      String actual = page.ToString();
      Assert.AreEqual("abc\nabc\n", actual);
    }
  }
}
