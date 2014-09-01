using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatR.Helpers.Collections
{
    class MultiMap<T>
    {
        protected Dictionary<string, List<T>> _values;

        public MultiMap()
        {
            _values = new Dictionary<string, List<T>>();
        }

        public void Add(string key, T item)
        {
            if (_values.ContainsKey(key))
            {
                _values[key].Add(item);
            }
            else
            {
                var newList = new List<T>();
                newList.Add(item);
                _values.Add(key, newList);
            }

        }

        public IEnumerable<string> Keys
        {
            get
            {
                return this._values.Keys;
            }
        }

        public IEnumerable<T> Values
        {
            get
            {
                return _values.Values.SelectMany(x => x.ToList<T>()).ToList();
            }
        }

        public List<T> this[string key]
        {
            get
            {
                List<T> list;
                if (!this._values.TryGetValue(key, out list))
                {
                    list = new List<T>();
                    this._values[key] = list;
                }
                return list;
            }
        }
    }
}