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
            Task<string> getStringTask = client.GetStringAsync("http://api.fixer.io/latest?base=PLN");
            string urlContents = await getStringTask;
            return urlContents;
        }

        public CurrencyCheckViewModel()
        {
            GetCurrencyList();
            AvailableCurrencies = new List<string>();
            AvailableCurrencies.AddRange(new List<string> { "AUD", "BGN", "BRL", "CAD", "CHF", "CNY", "CZK", "DKK", "GBP", "HKD", "HRK", "HUF", "IDR", "ILS", "INR", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "RON", "RUB", "SEK", "SGD", "THB", "TRY", "USD", "ZAR", "EUR", "PLN" });
        }

        public void RatesToList(Rates rates)
        {
            CurrencyList = new ObservableCollection<Currency>();

            CurrencyList.Add(new Currency { Name = "AUD", Rate = rates.AUD });
            CurrencyList.Add(new Currency { Name = "BGN", Rate = rates.BGN });
            CurrencyList.Add(new Currency { Name = "BRL", Rate = rates.BRL });
            CurrencyList.Add(new Currency { Name = "CAD", Rate = rates.CAD });
            CurrencyList.Add(new Currency { Name = "CHF", Rate = rates.CHF });
        }

        public async void updateCurrencyList()
        {
            string apiResponse = await GetCurrencyList();

            BaseCurrency baseCurrency = JsonConvert.DeserializeObject<BaseCurrency>(apiResponse);

            Rates rates = baseCurrency.rates;
            RatesToList(rates);
        }
    }
}
