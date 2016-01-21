using System;
using GalaSoft.MvvmLight;


namespace CurrencyMVVM.Data.Model
{
    public sealed class CurrencyPair : ObservableObject
    {
        #region :: ~ Internal objects ~ ::

        private string _name = null;
        private decimal _bid = 0m;      // <-- покупка [предложение цены на покупку] (как правило меньше чем Ask)
        private decimal _ask = 0m;      // <-- продажа [предложение цены на продажу] (как правило больше чем Bid)

        #endregion :: ^ Internal objects ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Constructors ~ ::

        public CurrencyPair(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length != 7)
                throw new ArgumentNullException(nameof(name), "CurrencyPair must have a valid name like \"USD/RUB\"");

            this._name = name.ToUpper();
        }



        public CurrencyPair(string name, decimal bid, decimal ask) : this(name)
        {
            if (bid < 0)
                throw new ArgumentOutOfRangeException(nameof(bid), "the value must be positive");
            else if (ask < 0)
                throw new ArgumentOutOfRangeException(nameof(ask), "the value must be positive");

            this._bid = bid;
            this._ask = ask;
        }

        #endregion :: ^ Constructors ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Properties ~ ::

        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для Name,
        /// представляющее собой имя и одновременно идентификатор валютной пары;
        /// изменение этого свойства поднимает событие PropertyChanged
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { Set(() => Name, ref this._name, value); }
        }



        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для Bid,
        /// представляющее собой цену за которую банк готов купить валюту;
        /// изменение этого свойства поднимает событие PropertyChanged
        /// </summary>
        public decimal Bid
        {
            get { return this._bid; }
            set { Set(() => Bid, ref this._bid, value); }
        }



        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для Ask,
        /// представляющее собой цену по которой банк готов продать валюту;
        /// изменение этого свойства поднимает событие PropertyChanged 
        /// </summary>
        public decimal Ask
        {
            get { return this._ask; }
            set { Set(() => Ask, ref this._ask, value); }
        }

        #endregion :: ^ Properties ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Methods ~ ::

        public void UpdateValuesBy(CurrencyPair currencyPair)
        {
            this.Bid = currencyPair.Bid;
            this.Ask = currencyPair.Ask;
        }



        public override string ToString() { return this.Name; }

        #endregion :: ^ Methods ^ ::
    }
}