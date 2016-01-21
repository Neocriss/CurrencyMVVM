using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMVVM.Data.ViewModel;
using Xamarin.Forms;


namespace CurrencyMVVM
{
    public partial class MainPage : ContentPage
    {
        #region :: ~ Constructors ~ ::

        public MainPage()
        {
            this.InitializeComponent();

            this.BindingContext = ((ViewModelLocator)Application.Current.Resources["Locator"]).Main;
        }

        #endregion :: ^ Constructors ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Properties ~ ::

        public MainViewModel Vm => (MainViewModel)BindingContext;

        #endregion :: ^ Properties ^ ::
    }
}
