using System;
using System.Collections.Generic;
using CurrencyMVVM.Data.Business;
using GalaSoft.MvvmLight;


namespace CurrencyMVVM.Data.Model
{
    public sealed class Bank : ObservableObject
    {
        #region :: ~ Internal objects ~ ::

        private string _name = null;
        private CurrencyPair _usd_to_rub = new CurrencyPair("USD/RUB");
        private CurrencyPair _eur_to_rub = new CurrencyPair("EUR/RUB");
        private object _tag = null;
        private bool _isDataInitialized = false;

        private IFinancialInfoProvider infoProvider = null;
        public event EventHandler DataRefreshed;

        #endregion :: ^ Internal objects ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Constructors ~ ::
        
        public Bank(string name, IFinancialInfoProvider infoProvider)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Bank must have a valid name");

            if (infoProvider == null)
                throw new ArgumentNullException(nameof(infoProvider));

            this._name = name;
            this.infoProvider = infoProvider;
        }

        #endregion :: ^ Constructors ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Properties ~ ::

        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для Name,
        /// представляющее собой ;
        /// изменение этого свойства поднимает событие PropertyChanged
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { Set(() => Name, ref this._name, value); }
        }



        /// <summary>Свойство:
        /// устанавливает и возвращает значение для USDtoRUB,
        /// представляющее собой валютную пару USD/RUB
        /// </summary>
        public CurrencyPair USDtoRUB
        {
            get { return this._usd_to_rub; }
            private set { this._usd_to_rub = value; }
        }



        /// <summary>Свойство:
        /// устанавливает и возвращает значение для EURtoRUB,
        /// представляющее собой валютную пару EUR/RUB
        /// </summary>
        public CurrencyPair EURtoRUB
        {
            get { return this._eur_to_rub; }
            private set { this._eur_to_rub = value; }
        }


        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для Tag, представляющее собой
        /// любые данные о данном банке, которые следует закрепить за ним;
        /// изменение этого свойства поднимает событие PropertyChanged 
        /// </summary>
        public object Tag
        {
            get { return this._tag; }
            set { Set(() => Tag, ref this._tag, value); }
        }


        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для IsDataInitialized,
        /// представляющее собой флаг показывающий, что все данные банка инициализированы
        /// изменение этого свойства поднимает событие PropertyChanged 
        /// </summary>
        public bool IsDataInitialized
        {
            get { return this._isDataInitialized; }
            set { Set(() => IsDataInitialized, ref this._isDataInitialized, value); }
        }

        #endregion :: ^ Properties ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Methods ~ ::

        public async void RefreshData()
        {
            IEnumerable<CurrencyPair> currencyPairs;

            try
            {
                currencyPairs = await infoProvider.GetActualCurrencyPairsAsync(new string[] { this.USDtoRUB.Name, this.EURtoRUB.Name });
            }
            catch (Exception)
            {
                throw new Exception("a fake FinancialInfoProvider has broken");
            }

            foreach (var currencyPair in currencyPairs)
            {
                switch (currencyPair.Name)
                {
                    case "USD/RUB":
                        this.USDtoRUB.UpdateValuesBy(currencyPair);
                        break;

                    case "EUR/RUB":
                        this.EURtoRUB.UpdateValuesBy(currencyPair);
                        break;
                }
            }

            this.IsDataInitialized = true;
            this.OnDataRefreshed();
        }


        public override string ToString() { return this.Name; }

        #endregion :: ^ Methods ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Utility methods ~ ::
        
        private void OnDataRefreshed()
        {
            var handler = this.DataRefreshed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion :: ^ Utility methods ^ ::
    }
}