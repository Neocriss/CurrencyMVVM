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
    public class AddingTailConverterTests
    {
        [Test]
        [TestCase(57.4)]
        public void Convert_ConcatinateStringToValue_ReturnsConcatinatedString<T>(T value)
        {
            AddingTailConverter converter = new AddingTailConverter();

            var result = converter.Convert(value, null, "and tail", null);

            Assert.That(result, Does.StartWith("57").And.EndsWith("40 and tail")); // Is.EqualTo("57,40 and tail"));
        }


        [Test]
        public void Convert_ConcatinateTwoStrings_ReturnsConcatinatedString()
        {
            AddingTailConverter converter = new AddingTailConverter();

            var result = converter.Convert("head", null, "and tail", null);

            Assert.That(result, Is.EqualTo("head and tail"));
        }
    }
}
