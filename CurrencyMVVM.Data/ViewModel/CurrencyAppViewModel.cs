using GalaSoft.MvvmLight;
using Xamarin.Forms;


namespace CurrencyMVVM.Data.ViewModel
{
    public class CurrencyAppViewModelBase : ViewModelBase
    {
        #region :: ~ Internal objects ~ ::

        // объявляем разрешение экрана в платформо-независимых единицах,
        //  которое понадобится для определения размеров ряда элементов
        protected static readonly double DeviceResolution = Device.OnPlatform(160, 160, 240);

        #endregion :: ^ Internal objects ^ ::
    }
}