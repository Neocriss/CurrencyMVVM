using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMVVM.Data.Business;
using CurrencyMVVM.Data.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;


namespace CurrencyMVVM.Data.ViewModel
{
    public class MainViewModel : CurrencyAppViewModelBase
    {
        #region :: ~ Internal objects ~ ::

        private readonly IBankRepository bankRepository;
        private bool _isResultVisible = false;
        private string _resultString = null;
        private double _boxViewDelimiterHeight = MainViewModel.DeviceResolution / 90;
        private double _dollarsToExchangeEntryWidth = MainViewModel.DeviceResolution * 50 / 72;

        #endregion :: ^ Internal objects ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Constructors ~ ::

        public MainViewModel(IBankRepository bankRepository)
        {
            this.bankRepository = bankRepository;
            this.Banks = new SortableObservableBanksCollection(this.bankRepository.FindAll(), bank => bank.USDtoRUB.Bid, true);
            this.Banks.InitializeData();
            this.CalcExchangeCommand = new RelayCommand<string>(CalcExchange);
        }

        #endregion :: ^ Constructors ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Properties ~ ::

        public SortableObservableBanksCollection Banks { get; private set; }



        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для IsResultVisible,
        /// представляющее собой флаг определяющий видимость нижней части страницы
        /// в которой отображается результат вычислений;
        /// изменение этого свойства поднимает событие PropertyChanged 
        /// </summary>
        public bool IsResultVisible
        {
            get { return this._isResultVisible; }
            set { Set(() => IsResultVisible, ref this._isResultVisible, value); }
        }



        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для ResultString,
        /// представляющее собой результат вычислений либо описание ошибки в виде строки;
        /// изменение этого свойства поднимает событие PropertyChanged 
        /// </summary>
        public string ResultString
        {
            get { return this._resultString; }
            set { Set(() => ResultString, ref this._resultString, value); }
        }



        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для BoxViewDelimiterHeight,
        /// представляющее собой высоту разделителя в платформо-независимых точках;
        /// изменение этого свойства поднимает событие PropertyChanged
        /// </summary>
        public double BoxViewDelimiterHeight
        {
            get { return _boxViewDelimiterHeight; }
            set { Set(() => BoxViewDelimiterHeight, ref _boxViewDelimiterHeight, value); }
        }



        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для DollarsToExchangeEntryWidth,
        /// представляющее собой ширину поля ввода количества долларов
        /// подлежащих обмену (значение представлено в платформо-независимых точках);
        /// изменение этого свойства поднимает событие PropertyChanged
        /// </summary>
        public double DollarsToExchangeEntryWidth
        {
            get { return _dollarsToExchangeEntryWidth; }
            set { Set(() => DollarsToExchangeEntryWidth, ref _dollarsToExchangeEntryWidth, value); }
        }

        #endregion :: ^ Properties ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Commands ~ ::

        public RelayCommand<string> CalcExchangeCommand { get; private set; }

        #endregion :: ^ Commands ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Utility methods ~ ::

        private void CalcExchange(string valueToExchangeString)
        {
            if (this.Banks.Count < 1) return;

            decimal valueToExchange = 0.0m;

            if (decimal.TryParse(valueToExchangeString, out valueToExchange) && valueToExchange > 0)
            {
                this.IsResultVisible = true;
                decimal result = valueToExchange * this.Banks[0].USDtoRUB.Bid;
                this.ResultString = $"Максимальная сумма {result:F2} рублей";
            }
            else
            {
                this.ResultString = "Введите положительное число и попробуйте снова...";
            }
        }

        #endregion :: ^ Utility methods ^ ::
    }
}
