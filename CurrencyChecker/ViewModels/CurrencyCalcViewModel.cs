using CurrencyChecker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

        private List<Currency> currencyList;
        public List<Currency> CurrencyList
        {
            get { return currencyList; }
            set
            {
                currencyList = value;
                NotifyPropertyChanged();
            }
        }

        async Task<string> GetCurrencyList()
        {
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync("http://api.fixer.io/latest?base=" + ChoosenCurrency);
            string urlContents = await getStringTask;
            return urlContents;
        }

        private ObservableCollection<Currency> calculatedCurrencyList;
        public ObservableCollection<Currency> CalculatedCurrencyList
        {
            get { return calculatedCurrencyList; }
            set
            {
                calculatedCurrencyList = value;
                NotifyPropertyChanged();
            }
        }

        public CurrencyCalcViewModel()
        {
            AvailableCurrencies = new List<string>();
            AvailableCurrencies.AddRange(new List<string> { "AUD", "BGN", "BRL", "CAD", "CHF", "CNY", "CZK", "DKK", "EUR", "GBP", "HKD", "HRK", "HUF", "IDR", "ILS", "INR", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "PLN", "RON", "RUB", "SEK", "SGD", "THB", "TRY", "USD", "ZAR" });
        }

        public void ParseToCalculatedCurrencyList(string json, double amount)
        {
            dynamic jobject = JsonConvert.DeserializeObject(json);

            CalculatedCurrencyList = new ObservableCollection<Currency>();

            foreach (var item in jobject.rates)
            {
                var testName = item.Name;
                var testValue = item.Value * amount;
                CalculatedCurrencyList.Add(new Currency { Name = testName, Rate = testValue });
            }
        }

        public async void Calculate()
        {
            string apiResponse = await GetCurrencyList();
            double amount = 1.0;
            try
            {
                amount = Double.Parse(AmountToCalculate);
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} is outside the range of the Double type.", AmountToCalculate);
            }
            ParseToCalculatedCurrencyList(apiResponse, amount);
        }
    }
}
