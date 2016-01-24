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
    public class SortableObservableBanksCollectionTests
    {
        private static class PrePrepared
        {
            public const string USD_RUB_NAME = "USD/RUB";
            public const string EUR_RUB_NAME = "EUR/RUB";

            private const string VTB24_NAME = "ВТБ 24";
            private const string GAZPROMBANK_NAME = "Газпромбанк";
            private const string ALFABANK_NAME = "Альфа-Банк";

            //      ---     ---     ---     ---     ---

            public static readonly IFinancialInfoProvider FakeVTB24Provider = Substitute.For<IFinancialInfoProvider>();

            public const decimal USD_RUB_Bid_75 = 75m;
            public const decimal USD_RUB_Ask_76 = 76m;
            public const decimal EUR_RUB_Bid_85 = 85m;
            public const decimal EUR_RUB_Ask_86 = 86m;

            //      ---     ---     ---     ---     ---

            public static readonly IFinancialInfoProvider FakeGazprombankProvider = Substitute.For<IFinancialInfoProvider>();

            public const decimal USD_RUB_Bid_77 = 77m;
            public const decimal USD_RUB_Ask_78 = 78m;
            public const decimal EUR_RUB_Bid_87 = 87m;
            public const decimal EUR_RUB_Ask_88 = 88m;

            //      ---     ---     ---     ---     ---

            public static readonly IFinancialInfoProvider FakeAlfaBankProvider = Substitute.For<IFinancialInfoProvider>();

            public const decimal USD_RUB_Bid_79 = 79m;
            public const decimal USD_RUB_Ask_80 = 80m;
            public const decimal EUR_RUB_Bid_89 = 89m;
            public const decimal EUR_RUB_Ask_90 = 90m;

            //      ---     ---     ---     ---     ---

            public static readonly List<Bank> BankList1 = new List<Bank>(3)
            {
                new Bank(PrePrepared.VTB24_NAME, FakeVTB24Provider),
                new Bank(PrePrepared.GAZPROMBANK_NAME, FakeGazprombankProvider),
                new Bank(PrePrepared.ALFABANK_NAME, FakeAlfaBankProvider)
            };

            public static readonly List<Bank> BankList2 = new List<Bank>(3)
            {
                new Bank(PrePrepared.VTB24_NAME, FakeVTB24Provider),
                new Bank(PrePrepared.GAZPROMBANK_NAME, FakeGazprombankProvider),
                new Bank(PrePrepared.ALFABANK_NAME, FakeAlfaBankProvider)
            };

            public static readonly List<Bank> BankList3 = new List<Bank>(3)
            {
                new Bank(PrePrepared.VTB24_NAME, FakeVTB24Provider),
                new Bank(PrePrepared.GAZPROMBANK_NAME, FakeGazprombankProvider),
                new Bank(PrePrepared.ALFABANK_NAME, FakeAlfaBankProvider)
            };
        }


        //      ---     ---     ---     ---     ---


        [OneTimeSetUp]
        public void Init()
        {
            PrePrepared.FakeVTB24Provider.GetActualCurrencyPairsAsync(
                Arg.Is<string[]>(x => x.Contains(PrePrepared.USD_RUB_NAME) && x.Contains(PrePrepared.EUR_RUB_NAME))
                ).Returns(new List<CurrencyPair>()
                    {
                        new CurrencyPair(PrePrepared.USD_RUB_NAME, PrePrepared.USD_RUB_Bid_75, PrePrepared.USD_RUB_Ask_76),
                        new CurrencyPair(PrePrepared.EUR_RUB_NAME, PrePrepared.EUR_RUB_Bid_85, PrePrepared.EUR_RUB_Ask_86)
                    }
                );

            PrePrepared.FakeGazprombankProvider.GetActualCurrencyPairsAsync(
                Arg.Is<string[]>(x => x.Contains(PrePrepared.USD_RUB_NAME) && x.Contains(PrePrepared.EUR_RUB_NAME))
                ).Returns(new List<CurrencyPair>()
                    {
                        new CurrencyPair(PrePrepared.USD_RUB_NAME, PrePrepared.USD_RUB_Bid_77, PrePrepared.USD_RUB_Ask_78),
                        new CurrencyPair(PrePrepared.EUR_RUB_NAME, PrePrepared.EUR_RUB_Bid_87, PrePrepared.EUR_RUB_Ask_88)
                    }
                );

            PrePrepared.FakeAlfaBankProvider.GetActualCurrencyPairsAsync(
                Arg.Is<string[]>(x => x.Contains(PrePrepared.USD_RUB_NAME) && x.Contains(PrePrepared.EUR_RUB_NAME))
                ).Returns(new List<CurrencyPair>()
                    {
                        new CurrencyPair(PrePrepared.USD_RUB_NAME, PrePrepared.USD_RUB_Bid_79, PrePrepared.USD_RUB_Ask_80),
                        new CurrencyPair(PrePrepared.EUR_RUB_NAME, PrePrepared.EUR_RUB_Bid_89, PrePrepared.EUR_RUB_Ask_90)
                    }
                );
        }


        //      ---     ---     ---     ---     ---

        
        [Test]
        public void SortableObservableBanksCollection_PassingNullKeySelectorArgument_ShouldThrowArgumentNullException()
        {
            Assert.Throws(typeof (ArgumentNullException), () => new SortableObservableBanksCollection(null));
        }



        [Test]
        public void SortableObservableBanksCollection_PassingNullListArgument_ShouldThrowArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new SortableObservableBanksCollection(null, bank => bank.USDtoRUB.Bid));
        }



        [Test]
        public void IsDescendingSortOrder_CreateNewInstanceAndPassingTrue_ShouldReturnsTrue()
        {
            var collection1 = new SortableObservableBanksCollection(bank => bank.USDtoRUB.Bid, true);
            var collection2 = new SortableObservableBanksCollection(new List<Bank>(), bank => bank.USDtoRUB.Bid, true);

            Assert.That(collection1.IsDescendingSortOrder, Is.True);
            Assert.That(collection2.IsDescendingSortOrder, Is.True);
        }



        [Test]
        public void IsDataInitialized_InitializeDataExecution_ShouldReturnsTrue()
        {
            var collection = new SortableObservableBanksCollection(PrePrepared.BankList1, bank => bank.USDtoRUB.Bid);

            collection.InitializeData();

            Assert.That(() => collection.IsDataInitialized, Is.True.After(1000));
        }



        [Test]
        public void IsSorted_InitializeDataExecution_ShouldReturnsTrue()
        {
            var collection = new SortableObservableBanksCollection(PrePrepared.BankList2, bank => bank.USDtoRUB.Bid);

            collection.InitializeData();

            Assert.That(() => collection.IsSorted, Is.True.After(1000));
        }



        [Test]
        public void LambdaExpression_InitializeDataExecution_SortingByDescendingShouldWorkProperly()
        {
            var collection = new SortableObservableBanksCollection(PrePrepared.BankList3, bank => bank.USDtoRUB.Bid, true);

            collection.InitializeData();

            Assert.That(() => collection[0].USDtoRUB.Bid, Is.EqualTo(PrePrepared.USD_RUB_Bid_79).After(1000));
        }
    }
}
