using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.Collections;

namespace Ao.Middleware.DataWash
{
    public abstract class RecordReaderProvider<TKey, TValue> : DatasProviderCapturer<TKey,TValue>, IRecordReaderProvider<TKey, TValue>, IDataProvider<TKey, TValue>
    {
        public TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out var val))
                {
                    return val;
                }
                return default;
            }
        }

        public IDataReader Reader { get; }

        public INamedInfo? Name { get; }

        public virtual IEnumerable<TKey> Keys
        {
            get
            {
                if (nameKeys != null)
                {
                    return nameKeys;
                }
                return Enumerable.Range(0, Reader.FieldCount).Select(x => ToKey(Reader.GetName(x)));
            }
        }

        public virtual IEnumerable<TValue> Values => Enumerable.Range(0, Reader.FieldCount).Select(x => ToValue(Reader[x]));

        public int Count => Reader.FieldCount;

        public bool FreezeHeader { get; }

        private TKey[]? nameKeys;

        protected TKey[]? NameKeys => nameKeys;

        protected RecordReaderProvider(IDataReader reader, INamedInfo? name, bool freezeHeader,bool captureDatasProvider)
            :base(captureDatasProvider)
        {
            Reader = reader;
            Name = name;
            FreezeHeader = freezeHeader;
        }

        protected RecordReaderProvider(IDataReader reader, INamedInfo? name, bool freezeHeader)
            : this(reader, name, freezeHeader, false)
        {
        }
        protected RecordReaderProvider(IDataReader reader, INamedInfo? name)
            : this(reader, name, true, false)
        {
        }

        public int FindIndex(TKey key)
        {
            if (nameKeys == null)
            {
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    if (Equals(ToKey(Reader.GetName(i)), key))
                    {
                        return i;
                    }
                }
                return -1;
            }
            return Array.IndexOf(nameKeys, key);
        }

        public bool ContainsKey(TKey key)
        {
            if (nameKeys != null)
            {
                return Array.FindIndex(nameKeys, x => Equals(x, key)) != -1;
            }
            var k = FromKey(key);
            for (int i = 0; i < Reader.FieldCount; i++)
            {
                if (Reader.GetName(i) == k)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnDispose()
        {
            Reader.Dispose();
            captureDatasProvider?.Dispose();
            GC.SuppressFinalize(this);
        }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < Reader.FieldCount; i++)
            {
                var name = nameKeys != null ? nameKeys[i] : ToKey(Reader.GetName(i));
                yield return new KeyValuePair<TKey, TValue>(name, ToValue(Reader[i]));
            }
        }

        public IDataProvider<TKey, TValue>? Read()
        {
            if (Reader.Read())
            {
                if (FreezeHeader && nameKeys == null)
                {
                    nameKeys = new TKey[Reader.FieldCount];
                    for (int i = 0; i < Reader.FieldCount; i++)
                    {
                        nameKeys[i] = ToKey(Reader.GetName(i));
                    }
                }
                if (captureDatasProvider!=null)
                {
                    var provider = new PoolMapDataProvider<TKey, TValue>(Reader.FieldCount,null);
                    for (int i = 0; i < Reader.FieldCount; i++)
                    {
                        var key = nameKeys != null ? nameKeys[i] : ToKey(Reader.GetName(i));
                        provider[key] = ToValue(Reader[i]);
                    }
                    AddCaptureDataProvider(provider);
                }
                return this;
            }
            return null;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var idx = FindIndex(key);
            if (idx == -1)
            {
                value = default;
                return false;
            }
            value = ToValue(Reader[idx]);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public abstract TKey ToKey(string key);

        public abstract string FromKey(TKey key);

        public abstract TValue ToValue(object value);

        public virtual bool Reset()
        {
            return false;
        }
    }

    public class RecordReaderProvider : RecordReaderProvider<string, object>
    {
        public RecordReaderProvider(IDataReader reader, INamedInfo? name) : base(reader, name)
        {
        }

        public RecordReaderProvider(IDataReader reader, INamedInfo? name, bool freezeHeader) : base(reader, name, freezeHeader)
        {
        }

        public override string FromKey(string key)
        {
            return key;
        }

        public override string ToKey(string key)
        {
            return key;
        }

        public override object ToValue(object value)
        {
            return value;
        }
    }
}
