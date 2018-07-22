using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Venki_Tech_Test.Controllers;

namespace Venki_Tech_Test.UnitTest
{
    //TODO > Complete Test Cases for all the Logical methods
    [TestFixture]
    public class TestQuestionFixture
    {
        //[Test]
        //[TestCase("'10 O''''CLOCK NWS','ACCESS HOLLYWD','Jeop','C O''''BRIEN-NBC''''''''Program','Frasier''''s','Barney','Just Shoot me','Wheel','Sesame Street',")] 
        //public void ValidateGetProgramNames(string expectedOutput)
        //{
        //    HomeController homeController = new HomeController();
        //    var actualResult = homeController.GetProgramNames();
        //    Assert.AreEqual(expectedOutput, actualResult);
        //}

        [Test]
        [TestCase(1, 1, false)]
        [TestCase(1, 7, true)]
        public void ValidateUpdateMarketPop(int cellId, int marketId, bool successTest)
        {
            HomeController homeController = new HomeController();
            bool actualResult = homeController.UpdateMarketPop(cellId, marketId);
            Assert.AreEqual(actualResult, successTest);
        }
    }
}