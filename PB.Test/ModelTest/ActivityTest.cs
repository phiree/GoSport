using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PB.Model;
using PB.Library;
namespace PB.Test.ModelTest
{
    [TestFixture]
   public class ActivityTest
    {
        [Test]
        public void ActivityConstructorTest()
        {
            string errMsg;
            Activity act = new Activity("errorType",
                false, null, null, null, null, null, null,0,false,0, out errMsg);
            Assert.AreEqual("活動類型有誤", WebResourceManager.GetString("ActivityTypeError"));

        }
    }
}
