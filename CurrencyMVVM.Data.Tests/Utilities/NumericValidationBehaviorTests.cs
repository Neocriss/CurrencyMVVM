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
    public class NumericValidationBehaviorTests
    {
        [Test]
        public void OnEntryTextChanged_SettingNonNumericalTextToEntry_TextColoredToRed()
        {
            NumericValidationBehavior behavior = new NumericValidationBehavior();
            Entry entry = new Entry();
            entry.Behaviors.Add(behavior);

            entry.Text = "abc1";

            Assert.That(entry.TextColor, Is.EqualTo(Color.Red));
        }
    }
}
