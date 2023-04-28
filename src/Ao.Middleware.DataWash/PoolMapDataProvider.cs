using Collections.Pooled;
using System.Collections.Generic;
using System.Data;
using System;
using System.Linq;

namespace Ao.Middleware.DataWash
{
    public abstract class MyDataReader<TInput> : IDataReader
    {
        private IEnumerable<IEnumerable<TInput>> _data;
        private IEnumerator<IEnumerable<TInput>> _enumerator;
        private IEnumerable<TInput>? current;
        private int _currentIndex = -1;

        public MyDataReader(IEnumerable<IEnumerable<TInput>> data)
        {
            _data = data;
        }
        public abstract string GetName(TInput input);

        public abstract object GetValue(TInput input);

        public object this[int i]=>GetValue(current.ElementAt(i));

        public object this[string name] => GetValue(current.First(x=>GetName(x)==name));

        public int Depth => _currentIndex;

        public bool IsClosed => false;

        public int RecordsAffected => 0;

        public int FieldCount => current.Count();

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public bool GetBoolean(int i)
        {
            return Convert.ToBoolean(GetValue(current.ElementAt(i)));
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            _currentIndex++;
            return _currentIndex < _data.Count();
        }
    }


    public class PoolMapDataProvider<TValue> : PooledDictionary<string, TValue>, <TKey, TValue>
    {
        public PoolMapDataProvider()
        {
            System.Data.IDataReader
        }

        public PoolMapDataProvider(INamedInfo? name)
        {
            Name = name;
        }

        public PoolMapDataProvider(int capacity, INamedInfo? name) 
            : base(capacity)
        {
            Name = name;
        }

        public INamedInfo? Name { get; }
    }
}
