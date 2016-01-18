using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CurrencyMVVM.Data.ViewModel
{
    public class MainViewModel : CurrencyAppViewModelBase
    {
        #region :: ~ Internal objects ~ ::

        private double _boxViewDelimiterHeight = MainViewModel.DeviceResolution / 90;
        private double _dollarsToExchangeEntryWidth = MainViewModel.DeviceResolution * 50 / 72;

        #endregion :: ^ Internal objects ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Constructors ~ ::



        #endregion :: ^ Constructors ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Properties ~ ::

        /// <summary>
        /// Sets and gets the BoxViewDelimiterHeight property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double BoxViewDelimiterHeight
        {
            get
            {
                return _boxViewDelimiterHeight;
            }
            set
            {
                Set(() => BoxViewDelimiterHeight, ref _boxViewDelimiterHeight, value);
            }
        }



        /// <summary>
        /// Sets and gets the DollarsToExchangeEntryWidth property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double DollarsToExchangeEntryWidth
        {
            get
            {
                return _dollarsToExchangeEntryWidth;
            }
            set
            {
                Set(() => DollarsToExchangeEntryWidth, ref _dollarsToExchangeEntryWidth, value);
            }
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
