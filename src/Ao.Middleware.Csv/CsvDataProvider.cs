using Ao.Middleware.DataWash;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.Middleware.Csv
{
    public class CsvDataProvider<TKey,TValue>:IDataProvider<TKey,TValue>
    {
        public CsvReader CsvReader { get; }

        public void A()
        {
        }

    }
}
