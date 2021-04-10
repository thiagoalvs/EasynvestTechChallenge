using EasynvestTechDemo.Shared.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasynvestTechDemo.Tests.Shared
{
    [TestClass]
    public class StringExtensionsTest
    {

        [TestMethod]
        public void DeveConverterDeStringParaDateTime()
        {
            string rawDate = "2022-11-15T00:00:00";

            Assert.IsTrue(rawDate.ToDateTime() == new DateTime(2022, 11, 15));
        }

        [TestMethod]
        public void DeveFalharAoTentarConverterUmaStringInvalida()
        {
            string rawDate = "InvalidString";

            Assert.ThrowsException<InvalidOperationException>(() => rawDate.ToDateTime());
        }

    }
}
