using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PB.Library;
namespace PB.Test.LibTest
{[TestFixture]
  public  class ResourceManagerTEst
    {
    [Test]
    public void GetStringTest()
    {
        Assert.AreEqual("活動類型有誤",  WebResourceManager.GetString("ActivityTypeError"));

    }
    }
}
