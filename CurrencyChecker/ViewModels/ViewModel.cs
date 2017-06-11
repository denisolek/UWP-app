using CurrencyChecker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private List<string> availableCurrencies;
        public List<string> AvailableCurrencies
        {
            get { return availableCurrencies; }
            set { availableCurrencies = value; }
        }

        public ViewModel()
        {
            AvailableCurrencies = new List<string>();
            AvailableCurrencies.AddRange(new List<string> { "AUD", "BGN", "BRL", "CAD", "CHF", "CNY", "CZK", "DKK", "EUR", "GBP", "HKD", "HRK", "HUF", "IDR", "ILS", "INR", "JPY", "KRW", "MXN", "MYR", "NOK", "NZD", "PHP", "PLN", "RON", "RUB", "SEK", "SGD", "THB", "TRY", "USD", "ZAR" });
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

        public async Task<string> GetCurrencyList()
        {
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync("http://api.fixer.io/latest?base=" + ChoosenCurrency);
            string urlContents = await getStringTask;
            return urlContents;
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
    }
}
