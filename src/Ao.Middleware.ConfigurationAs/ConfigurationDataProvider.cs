using Ao.Middleware.DataWash;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Ao.Middleware.ConfigurationAs
{
    public class ConfigurationDataProvider : ConfigurationDataProvider<string, object?>
    {
        public ConfigurationDataProvider(IConfiguration configuration, INamedInfo? name)
            : base(StringObjectDataConverter.Instance, configuration, name)
        {
        }
    }
    public class ConfigurationDataProvider<TKey, TValue> : IDataProvider<TKey, TValue>
    {
        public ConfigurationDataProvider(IStringObjectDataConverter<TKey, TValue> dataConverter, IConfiguration configuration, INamedInfo? name)
        {
            Name = name;
            DataConverter = dataConverter ?? throw new ArgumentNullException(nameof(dataConverter));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public INamedInfo? Name { get; }

        public IStringObjectDataConverter<TKey, TValue> DataConverter { get; }

        public IConfiguration Configuration { get; }

        public TValue this[TKey key] => DataConverter.ValueConverter.Convert(Configuration[DataConverter.StringConverter.Convert(key)]);

        public IEnumerable<TKey> Keys =>
            Configuration.AsEnumerable().Select(x => DataConverter.KeyConverter.Convert(x.Key));

        public IEnumerable<TValue> Values =>
            Configuration.AsEnumerable().Select(x => DataConverter.ValueConverter.Convert(x.Key));

        public int Count => Configuration.AsEnumerable().Count();

        public bool ContainsKey(TKey key)
        {
            var strKey = DataConverter.StringConverter.Convert(key);
            return Configuration.AsEnumerable().Any(x => x.Key == strKey);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Configuration.AsEnumerable()
                .Select(x => new KeyValuePair<TKey, TValue>(
                    DataConverter.KeyConverter.Convert(x.Key),
                    DataConverter.ValueConverter.Convert(x.Value))).GetEnumerator();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            var strKey = DataConverter.StringConverter.Convert(key);
            var f = Configuration.AsEnumerable().Where(x => x.Key == strKey)
                .Select(x => new { val = DataConverter.ValueConverter.Convert(x.Value) })
                .FirstOrDefault();
            if (f == null)
            {
                value = default;
                return false;
            }
            value = f.val;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
