using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMVVM.Data.Business;
using CurrencyMVVM.Data.Model;
using GalaSoft.MvvmLight;

namespace CurrencyMVVM.Data.ViewModel
{
    public class MainViewModel : CurrencyAppViewModelBase
    {
        #region :: ~ Internal objects ~ ::

        private readonly IBankRepository bankRepository;
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
        }

        #endregion :: ^ Constructors ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Properties ~ ::

        public SortableObservableBanksCollection Banks { get; private set; }


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



        #endregion :: ^ Commands ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Methods ~ ::



        #endregion :: ^ Methods ^ ::
    }
}
