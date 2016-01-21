using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace CurrencyMVVM.Data.Utilities
{
    public class NumericValidationBehavior : Behavior<Entry>
    {
        #region :: ~ Internal objects ~ ::

        private Color defaultTextColor;

        #endregion :: ^ Internal objects ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Utility methods ~ ::

        protected override void OnAttachedTo(Entry entry)
        {
            this.defaultTextColor = entry.TextColor;

            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }


        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        #endregion :: ^ Utility methods ^ ::

        //      ---     ---     ---     ---     ---

        #region :: ~ Event handlers ~ ::

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            decimal result;
            bool isValid = Decimal.TryParse(args.NewTextValue, out result);
            ((Entry)sender).TextColor = isValid ? this.defaultTextColor : Color.Red;
        }

        #endregion :: ^ Event handlers ^ ::
    }
}
