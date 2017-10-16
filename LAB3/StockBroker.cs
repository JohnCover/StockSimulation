using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3
{
    class StockBroker
    {
        string _brokerName;
        List<Stock> stocks;

        public StockBroker(string name)
        {
            _brokerName = name;
            stocks = new List<Stock>();
        }

        public void addStock(Stock s)
        {
            s.OnStockThreshold += Notify;
            stocks.Add(s);
        }

        public void Notify(object sender, StockEventArgs args)
        {
            Console.WriteLine(_brokerName +"\t"+ args._stockName +"\t"+ args._currentValue + "\t" + args._numberChanges);
        }

    }
}
