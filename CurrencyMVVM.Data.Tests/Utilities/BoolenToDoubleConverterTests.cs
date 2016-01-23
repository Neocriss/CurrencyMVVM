using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMVVM.Data.Utilities;


namespace CurrencyMVVM.Data.Tests.Utilities
{
    [TestFixture]
    public class BoolenToDoubleConverterTests
    {
        [Test]
        public void Convert_BoolenTrueToDouble_ReturnsDoubleOne()
        {
            BoolenToDoubleConverter converter = new BoolenToDoubleConverter();

            var result = converter.Convert(true, null, null, null);

            Assert.That(result, Is.EqualTo(1.0));
        }


        [Test]
        public void Convert_BoolenFalseToDouble_ReturnsDoubleZero()
        {
            BoolenToDoubleConverter converter = new BoolenToDoubleConverter();

            var result = converter.Convert(false, null, null, null);

            Assert.That(result, Is.EqualTo(0.0));
        }
    }
}
