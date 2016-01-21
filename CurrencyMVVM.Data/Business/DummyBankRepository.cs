using System.Collections.Generic;
using CurrencyMVVM.Data.Model;


namespace CurrencyMVVM.Data.Business
{
    public class DummyBankRepository : IBankRepository
    {
        #region :: ~ Internal objects ~ ::

        private List<Bank> _banks = new List<Bank>();
        private readonly IFinancialInfoProvider infoProvider = null;

        #endregion :: ^ Internal objects ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Constructors ~ ::

        public DummyBankRepository(IFinancialInfoProvider infoProvider)
        {
            this.infoProvider = infoProvider;

            string[] bankNames =
            {
                "Сбербанк России",
                "Альфа-Банк",
                "Россельхозбанк",
                "Газпромбанк",
                "ВТБ 24",
                "Банк Москвы",
                "Промсвязьбанк",
                "Райффайзенбанк",
                "Совкомбанк",
                "ФК Открытие"
            };

            foreach (string bankName in bankNames)
            {
                this._banks.Add(new Bank(bankName, this.infoProvider));
            }
        }

        #endregion :: ^ Constructors ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Properties ~ ::



        #endregion :: ^ Properties ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Methods ~ ::



        #endregion :: ^ Methods ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Event handlers ~ ::



        #endregion :: ^ Event handlers ^ ::

        #region :: ~ Methods ~ ::

        public IList<Bank> FindAll()
        {
            return this._banks;
        }

        #endregion :: ^ Methods ^ ::
    }
}