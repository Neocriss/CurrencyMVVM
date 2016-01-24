using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMVVM.Data.Utilities;
using Xamarin.Forms;


namespace CurrencyMVVM.Data.Tests.Utilities
{
    [TestFixture]
    public class ULongNumericValidationBehaviorTests
    {
        [Test]
        public void OnEntryTextChanged_SettingNonNumericalTextToEntry_TextColoredToRed()
        {
            ULongNumericValidationBehavior behavior = new ULongNumericValidationBehavior();
            Entry entry = new Entry();
            entry.Behaviors.Add(behavior);

            entry.Text = "abc1";

            Assert.That(entry.TextColor, Is.EqualTo(Color.Red));
        }
    }
}
