using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;


namespace CurrencyMVVM.Data.Model
{
    /// <summary>Тип:
    /// обозреваемая коллекция банков в которой элементы сортируются по заданному ключу после того,
    /// как все будут инициализированны; в момент сортировки вычисляется числовая разница между заданнными ключами
    /// первого элемента в списке и каждого последующего;
    /// </summary>
    public sealed class SortableObservableBanksCollection : ObservableCollection<Bank>
    {
        #region :: ~ Internal objects ~ ::

        public const string IsSortedPropertyName = "IsSorted";

        private Func<Bank, decimal> keySelector = null;
        private bool _isSorted = false;

        // нельзя делать сортировку элементов коллекции во время перечисление этих элементов;
        // данный флаг позволяет избежать начала сортировки до того как будет закончено
        // перечисление элементов начатое в методе InitializeData()
        private bool isInitializingOrSorting = false;

        #endregion :: ^ Internal objects ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Constructors ~ ::

        public SortableObservableBanksCollection(Func<Bank, decimal> keySelector, bool descending = false)
        {
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector), "You have to provide a valid keySelector for sorting Banks");

            this.keySelector = keySelector;
            this.IsDescendingSortOrder = descending;
        }


        public SortableObservableBanksCollection(IList<Bank> banksList, Func<Bank, decimal> keySelector,
            bool descending = false) : base(banksList)
        {
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector), "You have to provide a valid keySelector for sorting Banks");

            this.keySelector = keySelector;
            this.IsDescendingSortOrder = descending;

            foreach (Bank bank in banksList)
            {
                bank.DataRefreshed += Bank_DataRefreshed;
            }
        }

        #endregion :: ^ Constructors ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Properties ~ ::
        
        public bool IsDescendingSortOrder { get; private set; }


        /// <summary>Свойство:
        /// вычисляет и возвращает значение IsDataInitialized,
        /// которое показывает инициализированны ли все данные в коллекции или нет
        /// </summary>
        public bool IsDataInitialized
        {
            get
            {
                int initializedBanksCount = this.Count(bank => bank.IsDataInitialized);
                return initializedBanksCount == this.Count;
            }
        }


        /// <summary>Обозреваемое свойство:
        /// устанавливает и возвращает значение для IsSorted,
        /// представляющее собой флаг показывающий отсортированна ли коллекция или нет;
        /// изменение этого свойства поднимает событие PropertyChanged
        /// </summary>
        public bool IsSorted
        {
            get { return _isSorted; }

            private set
            {
                if (this._isSorted == value) return;

                this._isSorted = value;
                OnPropertyChanged(new PropertyChangedEventArgs(IsSortedPropertyName));
            }
        }

        #endregion :: ^ Properties ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Methods ~ ::

        public void InitializeData()
        {
            if (this.isInitializingOrSorting || this.IsDataInitialized) return;

            this.isInitializingOrSorting = true;

            foreach (Bank bank in this)
                bank.RefreshData();

            this.isInitializingOrSorting = false;

            if (!this.isInitializingOrSorting && !this.IsSorted && this.IsDataInitialized)
                this.SortThenUpdate();
        }

        #endregion :: ^ Methods ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Utility methods ~ ::

        private void SortThenUpdate()
        {
            this.isInitializingOrSorting = true;

            List<Bank> sortedBanks = null;

            if (this.IsDescendingSortOrder)
                sortedBanks = this.OrderByDescending(this.keySelector).ToList();
            else
                sortedBanks = this.OrderBy(this.keySelector).ToList();

            base.ClearItems();

            for (int i = 0; i < sortedBanks.Count; i++)
            { 
                base.InsertItem(i, sortedBanks[i]);
                this[i].Tag = this.keySelector(this[i]) - this.keySelector(this[0]);
            }

            this.IsSorted = true;

            this.isInitializingOrSorting = false;
        }


        protected override void InsertItem(int index, Bank item)
        {
            item.DataRefreshed += Bank_DataRefreshed;

            base.InsertItem(index, item);
            this.IsSorted = false;
        }


        protected override void SetItem(int index, Bank item)
        {
            if (this[index] != item)
            {
                this[index].DataRefreshed -= Bank_DataRefreshed;
                item.DataRefreshed += Bank_DataRefreshed;

                base.SetItem(index, item);
                this.IsSorted = false;
            }
        }


        protected override void RemoveItem(int index)
        {
            this[index].DataRefreshed -= Bank_DataRefreshed;

            base.RemoveItem(index);
        }


        protected override void ClearItems()
        {
            foreach (Bank bank in this)
            {
                bank.DataRefreshed -= Bank_DataRefreshed;
            }

            base.ClearItems();
        }

        #endregion :: ^ Utility methods ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Event handlers ~ ::

        private void Bank_DataRefreshed(object sender, EventArgs e)
        {
            if (!this.isInitializingOrSorting && !this.IsSorted && this.IsDataInitialized)
                this.SortThenUpdate();
        }

        #endregion :: ^ Event handlers ^ ::
    }
}