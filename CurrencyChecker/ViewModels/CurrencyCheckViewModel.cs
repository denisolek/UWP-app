using CurrencyChecker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.ViewModels
{
    class CurrencyCheckViewModel : ViewModel
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

        private ObservableCollection<Currency> currencyList;
        public ObservableCollection<Currency> CurrencyList
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
            string test = ChoosenCurrency;
            Task<string> getStringTask = client.GetStringAsync("http://api.fixer.io/latest?base=PLN");
            string urlContents = await getStringTask;
            return urlContents;
        }

        public CurrencyCheckViewModel()
        {
            AvailableCurrencies = new List<string>();
            AvailableCurrencies.AddRange(new List<string> { "AUD", "BGN", "BRL", "CAD", "CHF", "CNY", "CZK", "DKK", "GBP", "HKD", "HRK", "HUF", "IDR", "ILS", "INR", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "RON", "RUB", "SEK", "SGD", "THB", "TRY", "USD", "ZAR", "EUR", "PLN" });
        }

        public void ParseToCurrencyList(string json)
        {
            dynamic jobject = JsonConvert.DeserializeObject(json);

            CurrencyList = new ObservableCollection<Currency>();

            foreach (var item in jobject.rates)
            {
                var testName = item.Name;
                var testValue = item.Value;
                CurrencyList.Add(new Currency { Name = testName, Rate = testValue });
            }
        }

        public async void updateCurrencyList()
        {
            string apiResponse = await GetCurrencyList();
            ParseToCurrencyList(apiResponse);
        }
    }
}
