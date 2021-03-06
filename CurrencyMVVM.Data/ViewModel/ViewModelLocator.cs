using System.Diagnostics.CodeAnalysis;
using CurrencyMVVM.Data.Business;
using CurrencyMVVM.Data.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;


namespace CurrencyMVVM.Data.ViewModel
{
    public class ViewModelLocator
    {
        #region :: ~ Constructors ~ ::

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IFinancialInfoProvider, DummyFinancialInfoProvider>();
            SimpleIoc.Default.Register<IBankRepository, DummyBankRepository>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        #endregion :: ^ Constructors ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Properties ~ ::

        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        #endregion :: ^ Properties ^ ::
    }
}