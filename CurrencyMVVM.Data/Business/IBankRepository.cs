using System.Collections.Generic;
using CurrencyMVVM.Data.Model;


namespace CurrencyMVVM.Data.Business
{
    public interface IBankRepository
    {
        IList<Bank> FindAll();
    }
}