using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMVVM.Data.Business;
using CurrencyMVVM.Data.Model;
using NSubstitute;


namespace CurrencyMVVM.Data.Tests.Model
{
    [TestFixture]
    public class BankTests
    {
        private const string BANK_NAME = "ВТБ 24";

        private const decimal USD_BID = 77m, USD_ASK = 79m;
        private const decimal EUR_BID = 87m, EUR_ASK = 89m;

        private const string USD_RUB_NAME = "USD/RUB";
        private const string EUR_RUB_NAME = "EUR/RUB";

        private readonly IFinancialInfoProvider prePreparedFakeProvider = Substitute.For<IFinancialInfoProvider>();


        //      ---     ---     ---     ---     ---


        [OneTimeSetUp]
        public void Init()
        {
            this.prePreparedFakeProvider.GetActualCurrencyPairsAsync(
                Arg.Is<string[]>(x => x.Contains(BankTests.USD_RUB_NAME) && x.Contains(BankTests.EUR_RUB_NAME))
                ).Returns(new List<CurrencyPair>()
                    {
                        new CurrencyPair(BankTests.USD_RUB_NAME, BankTests.USD_BID, BankTests.USD_ASK),
                        new CurrencyPair(BankTests.EUR_RUB_NAME, BankTests.EUR_BID, BankTests.EUR_ASK)
                    }
                );
        }


        //      ---     ---     ---     ---     ---

        
        [Test]
        public void Bank_PassingNullNameArgument_ShouldThrowArgumentNullException()
        {
            var fakeProvider = Substitute.For<IFinancialInfoProvider>();

            Assert.Throws(typeof (ArgumentNullException), () => new Bank(null, fakeProvider));
        }



        [Test]
        public void Bank_PassingNullProviderArgument_ShouldThrowArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new Bank(BankTests.BANK_NAME, null));
        }



        [Test]
        public void Bank_CreateNewInstance_CurrencyPairFieldsHasCorrectNames()
        {
            var fakeProvider = Substitute.For<IFinancialInfoProvider>();

            Bank bank = new Bank(BankTests.BANK_NAME, fakeProvider);
            
            Assert.That(bank.USDtoRUB.Name, Is.EqualTo(BankTests.USD_RUB_NAME));
            Assert.That(bank.EURtoRUB.Name, Is.EqualTo(BankTests.EUR_RUB_NAME));
        }



        [Test]
        public void RefreshData_Execution_ShouldUpdateCurrencyPairFields()
        {
            Bank bank = new Bank(BankTests.BANK_NAME, this.prePreparedFakeProvider);


            bank.RefreshData();


            Assert.That(bank.USDtoRUB.Bid, Is.EqualTo(BankTests.USD_BID));
            Assert.That(bank.USDtoRUB.Ask, Is.EqualTo(BankTests.USD_ASK));

            Assert.That(bank.EURtoRUB.Bid, Is.EqualTo(BankTests.EUR_BID));
            Assert.That(bank.EURtoRUB.Ask, Is.EqualTo(BankTests.EUR_ASK));
        }



        [Test]
        public void RefreshData_Execution_ShouldRaiseEvent()
        {
            bool eventHasRaised = false;

            Bank bank = new Bank(BankTests.BANK_NAME, this.prePreparedFakeProvider);
            bank.DataRefreshed += (sender, args) => { eventHasRaised = true; };


            bank.RefreshData();


            Assert.That(eventHasRaised, Is.True);
        }



        [Test]
        public void IsDataInitialized_CreateNewBankInstance_ShouldReturnsFalse()
        {
            var fakeProvider = Substitute.For<IFinancialInfoProvider>();


            Bank bank = new Bank(BankTests.BANK_NAME, fakeProvider);


            Assert.That(bank.IsDataInitialized, Is.False);
        }



        [Test]
        public void IsDataInitialized_RefreshDataExecution_ShouldReturnsTrue()
        {
            Bank bank = new Bank(BankTests.BANK_NAME, this.prePreparedFakeProvider);


            bank.RefreshData();

            
            Assert.That(bank.IsDataInitialized, Is.True);
        }
    }
}
