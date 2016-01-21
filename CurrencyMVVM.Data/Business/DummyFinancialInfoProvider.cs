using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyMVVM.Data.Model;


namespace CurrencyMVVM.Data.Business
{
    public class DummyFinancialInfoProvider : IFinancialInfoProvider
    {
        #region :: ~ Internal objects ~ ::

        private static Random random = new Random();

        #endregion :: ^ Internal objects ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Methods ~ ::

        public async Task<IEnumerable<CurrencyPair>> GetActualCurrencyPairsAsync(string[] names)
        {
            IEnumerable<CurrencyPair> actualCurrencyPairs = await Task.Delay(random.Next(500, 3000)).ContinueWith(_ =>
            {
                List<CurrencyPair> currencyPairs = new List<CurrencyPair>();

                int minValue;
                int maxValue;

                foreach (string name in names)
                {
                    if (name.Contains("EUR"))
                    {
                        minValue = 86;
                        maxValue = 92;
                    }
                    else
                    {
                        minValue = 77;
                        maxValue = 83;
                    }

                    // симулируем спред (т.е. разницу между bid и ask)
                    decimal delta = random.Next(300);

                    // вычисляем bid и ask
                    decimal bid = random.Next(minValue, maxValue) - Math.Round(delta / 300m, 2);
                    decimal ask = bid + delta / 100m;

                    currencyPairs.Add(new CurrencyPair(name, bid, ask));
                }

                return currencyPairs;
            });

            return actualCurrencyPairs;
        }

        #endregion :: ^ Methods ^ ::
    }
}
