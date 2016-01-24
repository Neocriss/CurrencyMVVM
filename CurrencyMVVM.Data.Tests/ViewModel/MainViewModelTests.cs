using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMVVM.Data.Business;
using CurrencyMVVM.Data.Model;
using CurrencyMVVM.Data.ViewModel;
using NSubstitute;


namespace CurrencyMVVM.Data.Tests.ViewModel
{
    [TestFixture]
    public class MainViewModelTests
    {
        private static class PrePrepared
        {
            public const string USD_RUB_NAME = "USD/RUB";
            public const string EUR_RUB_NAME = "EUR/RUB";

            public const string BANK_NAME = "ВТБ 24";

            public const decimal USD_RUB_Bid_75_01 = 75.01m;
            public const decimal USD_RUB_Ask_76_02 = 76.02m;
            public const decimal EUR_RUB_Bid_85_03 = 85.03m;
            public const decimal EUR_RUB_Ask_86_04 = 86.04m;

            public static readonly IFinancialInfoProvider FakeProvider = Substitute.For<IFinancialInfoProvider>();
            public static readonly IBankRepository FakeBankRepository = Substitute.For<IBankRepository>();
        }


        //      ---     ---     ---     ---     ---


        [OneTimeSetUp]
        public void Init()
        {
            PrePrepared.FakeProvider.GetActualCurrencyPairsAsync(
                Arg.Is<string[]>(x => x.Contains(PrePrepared.USD_RUB_NAME) && x.Contains(PrePrepared.EUR_RUB_NAME))
                ).Returns(new List<CurrencyPair>()
                    {
                        new CurrencyPair(PrePrepared.USD_RUB_NAME, PrePrepared.USD_RUB_Bid_75_01, PrePrepared.USD_RUB_Ask_76_02),
                        new CurrencyPair(PrePrepared.EUR_RUB_NAME, PrePrepared.EUR_RUB_Bid_85_03, PrePrepared.EUR_RUB_Ask_86_04)
                    }
                );

            PrePrepared.FakeBankRepository.FindAll().Returns(
                new List<Bank>() {new Bank(PrePrepared.BANK_NAME, PrePrepared.FakeProvider)});
        }


        //      ---     ---     ---     ---     ---


        [Test]
        public void ResultString_CalcExchangeCommandExecution_ShouldContainExpectedString()
        {
            const ulong dollarsToExchange = 10;

            decimal expectedResult = dollarsToExchange*PrePrepared.USD_RUB_Bid_75_01;
            string expectedString = $"{expectedResult:F2}";

            MainViewModel mainViewModel = new MainViewModel(PrePrepared.FakeBankRepository);


            mainViewModel.CalcExchangeCommand.Execute(dollarsToExchange.ToString());


            Assert.That(mainViewModel.ResultString, Does.Contain(expectedString));
        }
    }
}
