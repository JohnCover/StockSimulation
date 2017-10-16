using System;
using System.IO;
using System.Threading;


namespace LAB3
{
    
    
    class Stock
    {
        /*public delegate void StockNotification(string stockName, decimal currentValue, int numberChanges);
        public delegate void FileWriteNotification(DateTime date, string stockName, decimal initialValue, decimal currentValue);
        public event StockNotification OnStockThreshold;
        public event FileWriteNotification OnFileWrite;*/
        public event EventHandler<StockEventArgs> OnStockThreshold;
        private static int usingResource = 0;
        string _name;
        int _maxChange;
        int _numerOfChanges = 0;
        decimal _startingValue, _threshold, _currentValue;
        Random rng = new Random();
        public static DateTime Now { get; }
        
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\" + "stock.txt");

        public Stock(string name, decimal startingValue, int maxChange, decimal threshold)
        {
            _name = name;
            _startingValue = startingValue;
            _currentValue = startingValue;
            _maxChange = maxChange;
            _threshold = threshold;
        }

        public void Activate()
        {
            for(int i = 0; i < 60; i++)
            {
                Thread.Sleep(500);
                ChangeStockValue();
            }
        }
        
        public void ChangeStockValue()
        {
            _currentValue += rng.Next(_maxChange);
            _numerOfChanges++;
            if (_threshold < (_currentValue - _startingValue)) //event condition
            {
                OnStockThreshold(this, new StockEventArgs(this._name, this._currentValue, this._numerOfChanges)); //raise event & pass data to subscribers
                WriteToFile();
            }
                
        }

        public bool WriteToFile()
        {
            //OnFileWrite(DateTime.Now, this._name, this._startingValue, this._currentValue);
            //0 indicates that the method is not in use.
            if (0 == Interlocked.Exchange(ref usingResource, 1))
            {
                //Code to access a resource that is not thread safe would go here.
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path, true))
                    {
                        file.WriteLine(DateTime.Now + "\t" + this._name + "\t" + this._startingValue + "\t" + this._currentValue);
                    }
                //Release the lock
                Interlocked.Exchange(ref usingResource, 0);
                return true;
            }
            else
            {
                Console.WriteLine("   {0} was denied the lock", Thread.CurrentThread.Name);
                return false;
            }

        }

    }
}
