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

        public async void UpdateCurrencyList()
        {
            string apiResponse = await GetCurrencyList();
            ParseToCurrencyList(apiResponse);
        }
    }
}
