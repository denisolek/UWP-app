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
        
        public async void UpdateCurrencyList()
        {
            string apiResponse = await GetCurrencyList();
            ParseToCurrencyList(apiResponse);
        }
    }
}
