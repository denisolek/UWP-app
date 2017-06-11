using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.ViewModels
{
    class CurrencyCalcViewModel : ViewModel
    {
        private List<string> availableCurrencies;
        public List<string> AvailableCurrencies
        {
            get { return availableCurrencies; }
            set { availableCurrencies = value; }
        }

        private string choosenCurrency;
        public string ChoosenCurrency
        {
            get { return choosenCurrency; }
            set
            {
                choosenCurrency = value;
                NotifyPropertyChanged();
            }
        }

        private string amountToCalculate;
        public string AmountToCalculate
        {
            get { return amountToCalculate; }
            set
            {
                amountToCalculate = value;
                NotifyPropertyChanged();
            }
        }

        public CurrencyCalcViewModel()
        {
            AvailableCurrencies = new List<string>();
            AvailableCurrencies.AddRange(new List<string> { "AUD", "BGN", "BRL", "CAD", "CHF", "CNY", "CZK", "DKK", "EUR", "GBP", "HKD", "HRK", "HUF", "IDR", "ILS", "INR", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "PLN", "RON", "RUB", "SEK", "SGD", "THB", "TRY", "USD", "ZAR" });
        }

        public void Calculate()
        {

        }
    }
}
