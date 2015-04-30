using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Framework.Runtime.Json
{
    internal class JsonObject
    {
        private readonly IDictionary<string, object> _data;

        public JsonObject(IDictionary<string, object> data)
        {
            _data = data;
        }

        public ICollection<string> Keys
        {
            get { return _data.Keys; }
        }

        public object Value(string key)
        {
            object result;
            if (!_data.TryGetValue(key, out result))
            {
                result = null;
            }

            return result;
        }

        public T ValueAs<T>(string key, Func<object, T> converter)
        {
            return converter(Value(key));
        }

        public JsonObject ValueAsJsonObject(string key)
        {
            return ValueAs<JsonObject>(key, value =>
            {
                var dict = value as IDictionary<string, object>;

                if (dict == null)
                {
                    return null;
                }
                else
                {
                    return new JsonObject(dict);
                }
            });
        }

        public string ValueAsString(string key)
        {
            return ValueAs(key, value => value as string);
        }

        public bool ValueAsBoolean(string key, bool defaultValue = false)
        {
            return ValueAs(key, value =>
            {
                bool result;
                if (value != null && (value is string) && bool.TryParse((string)value, out result))
                {
                    return result;
                }

                return defaultValue;
            });
        }

        public bool? ValueAsNullableBoolean(string key)
        {
            return ValueAs<bool?>(key, value =>
            {
                bool result;
                if (value != null && value is string && bool.TryParse((string)value, out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            });
        }

        public object[] ValueAsArray(string key)
        {
            return ValueAs(key, value => (value as IList<object>)?.ToArray());
        }

        public T[] ValueAsArray<T>(string key)
        {
            return ValueAs(key, value => (value as IList<object>)?.Cast<T>().ToArray());
        }
    }
}
