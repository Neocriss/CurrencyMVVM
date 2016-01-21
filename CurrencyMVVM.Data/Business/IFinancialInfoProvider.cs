using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyMVVM.Data.Model;


namespace CurrencyMVVM.Data.Business
{
    public interface IFinancialInfoProvider
    {
        Task<IEnumerable<CurrencyPair>> GetActualCurrencyPairsAsync(string[] names);
    }
}
