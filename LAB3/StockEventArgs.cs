using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3
{
    class StockEventArgs : EventArgs
    {
        public StockEventArgs(string stockName, decimal currentValue, int numberChanges)
        {
            _stockName = stockName;
            _currentValue = currentValue;
            _numberChanges = numberChanges;
        }
        public string _stockName { get; set;  }
        public decimal _currentValue { get; set; }
        public int _numberChanges { get; set; }
    }
}
