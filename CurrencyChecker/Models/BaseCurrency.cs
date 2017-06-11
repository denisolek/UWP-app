using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Models
{
    public class BaseCurrency
    {
        public string @base { get; set; }
        public string date { get; set; }
        public Rates rates { get; set; }
    }
}
