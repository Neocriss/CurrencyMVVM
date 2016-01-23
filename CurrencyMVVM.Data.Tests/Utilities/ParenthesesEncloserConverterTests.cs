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
    public class ParenthesesEncloserConverterTests
    {
        [Test]
        public void Convert_NullArgument_ReturnsEllipsis()
        {
            ParenthesesEncloserConverter converter = new ParenthesesEncloserConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.That(result, Is.EqualTo("..."));
        }


        [Test]
        [TestCase("abc")]
        [TestCase(57.3)]
        public void Convert_PassingValue_ReturnsStringWithValueEnclosedByPrentheses<T>(T value)
        {
            ParenthesesEncloserConverter converter = new ParenthesesEncloserConverter();

            var result = converter.Convert(value, null, null, null);

            Assert.That(result, Does.StartWith("(").And.EndsWith(")"));
        }
    }
}
