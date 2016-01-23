using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMVVM.Data.Model;


namespace CurrencyMVVM.Data.Tests.Model
{
    [TestFixture]
    public class CurrencyPairTests
    {
        [Test]
        public void CurrencyPair_PassingNameInLowerCase_ShouldChangeNameToUpperCase()
        {
            string name = "usd/rub";

            CurrencyPair currencyPair = new CurrencyPair(name);

            Assert.That(currencyPair.Name, Is.EqualTo(name.ToUpper()));
        }


        [Test]
        public void CurrencyPair_PassingNullArgument_ShouldThrowArgumentNullException()
        {
            Assert.Throws(typeof (ArgumentNullException), () => new CurrencyPair(null));
        }


        [Test]
        public void CurrencyPair_PassingIncorrectArgument_ShouldThrowArgumentException([Values("", "   ", "usd rub")]string name)
        {
            Assert.Throws(typeof(ArgumentException), () => new CurrencyPair(name));
        }


        [Test]
        public void UpdateValuesBy_NullArgument_DoesNotThrowAnException()
        {
            CurrencyPair currencyPair = new CurrencyPair("USD/RUB");

            Assert.DoesNotThrow(() => currencyPair.UpdateValuesBy(null));
        }


        [Test]
        public void UpdateValuesBy_NullArgument_ShouldNotUpdateItsFields()
        {
            const decimal bid = 77;
            const decimal ask = 79;
            CurrencyPair currencyPair = new CurrencyPair("USD/RUB", bid, ask);

            currencyPair.UpdateValuesBy(null);

            Assert.That(currencyPair.Bid, Is.EqualTo(bid));
            Assert.That(currencyPair.Ask, Is.EqualTo(ask));
        }


        [Test]
        public void UpdateValuesBy_PassingSameCurrencyPairArgument_ShouldUpdateItsFields()
        {
            const decimal bid = 77;
            const decimal ask = 79;
            CurrencyPair mainCurrencyPair = new CurrencyPair("USD/RUB");
            CurrencyPair sourceCurrencyPair = new CurrencyPair("USD/RUB", bid, ask);
            
            mainCurrencyPair.UpdateValuesBy(sourceCurrencyPair);

            Assert.That(mainCurrencyPair.Bid, Is.EqualTo(bid));
            Assert.That(mainCurrencyPair.Ask, Is.EqualTo(ask));
        }


        [Test]
        public void UpdateValuesBy_PassingOtherCurrencyPairArgument_ShouldThrowArgumentException()
        {
            const decimal bid = 87;
            const decimal ask = 89;
            CurrencyPair mainCurrencyPair = new CurrencyPair("USD/RUB");
            CurrencyPair sourceCurrencyPair = new CurrencyPair("EUR/RUB", bid, ask);

            Assert.Throws(typeof(ArgumentException), () => mainCurrencyPair.UpdateValuesBy(sourceCurrencyPair));
        }
    }
}
