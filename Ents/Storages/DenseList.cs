using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ents.Storage
{
    public class DenseListOutOfBoundException : Exception
    {
        public DenseListOutOfBoundException(string message)
            : base(message)
        {
        }
    }

    public class DenseList<T>
    {
        private List<int?> _lookup;
        private List<T> _data;
        private const int MAX_SIZE = 1_000_000;

        public int DataCount { get => _data.Count; }

        public int LookupCount { get => _lookup.Count; }

        public DenseList()
        {
            _lookup = new List<int?>();
            _lookup.AddRange(new int?[4]); // Default size of 4
            _data = new List<T>();
        }

        public void Add(int id, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("This method does not accept null data");
            }

            if (id > MAX_SIZE)
            {
                throw new DenseListOutOfBoundException($"The Id Must be lowet than: {MAX_SIZE}");
            }

            if (id > _lookup.Count)
            {
                int diff = id - _lookup.Count;
                _lookup.AddRange(new int?[diff + 1]);
            }

            _data.Add(item);
            _lookup[id] = _data.Count - 1;
        }

        public void Remove(int id)
        {
            _data.RemoveAt(GetDataId(id));
        }

        public T Get(int id)
        {
            return _data[GetDataId(id)];
        }

        private int GetDataId(int id)
        {
            return _lookup[id] ?? throw new ArgumentException("There is no data associated with this id");
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Index - Lookup table - Data");
            for (int i = 0; i < _lookup.Count; i++)
            {
                if (_lookup[i].HasValue)
                {
                    int idData = _lookup[i].GetValueOrDefault();

                    stringBuilder.Append($"{i} -> [{_lookup[i]}]");
                    stringBuilder.AppendLine($" -> [{_data[idData]}]");
                }
                else
                {
                    stringBuilder.AppendLine($"{i} -> [{_lookup[i]}]");
                }
            }
            return stringBuilder.ToString();
        }
    }
}
