using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4_testirov
{
    class Valute
    {
        public string[] name = new[] { "USD", "EUR", "RUB", "BYN", "INR", "KZT", "CAD", "CNY", "UZS" };
        public string id; //номер валюты
        public List<string> his = new List<string>();
        public List<string> date = new List<string>();
    }
}
