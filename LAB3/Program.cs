using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LAB3
{
    class Program
    {
        static void Main(string[] args)
        {
            StockBroker John = new StockBroker("John");
            StockBroker Bill = new StockBroker("Bill");

            //Stock(string name, decimal startingValue, int maxChange, decimal threshold)
            Stock s1 = new Stock("Stock1", 100, 10, 40);
            Stock s2 = new Stock("Stock2", 10, 5, 15);
            Stock s3 = new Stock("Stock3", 1000, 10, 40);

            //assign stocks
            John.addStock(s1);
            John.addStock(s2);
            John.addStock(s3);
            Bill.addStock(s2);

            //threading
            Thread s1Thread = new Thread(new ThreadStart(s1.Activate));
            Thread s2Thread = new Thread(new ThreadStart(s2.Activate));
            Thread s3Thread = new Thread(new ThreadStart(s3.Activate));

            //strat simulation

            try
            {
                s1Thread.Start();
                s2Thread.Start();
                s3Thread.Start();
                s1Thread.Join();
                s2Thread.Join();
                s3Thread.Join();
            }
            catch (ThreadStateException e)
            {
                Console.WriteLine(e);  // Display text of exception
            }

        }
    }
}
