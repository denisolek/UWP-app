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
