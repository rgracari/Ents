using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ents.Storage
{
    /// <summary>
    /// A dense list of data storage. This storage uses an intern lookup table enabling the
    /// data to be packed in an list continuausly.
    /// </summary>
    /// <typeparam name="T">Any datatype needed to be insterted in the structure.</typeparam>
    public class DenseList<T>
    {
        /// <summary>
        /// The lookup table that match an int id to an element in the data list.
        /// The lookup might end up sparse, but by using an intermediate table the data List
        /// can be packed.
        /// <exemple>
        /// Lookup : [null, 0, null, null, 1, null]
        /// Data   : [data1, data2]
        /// (0 and 1) being the indexes of the data list.
        /// </exemple>
        /// </summary>
        private List<int?> _lookup;
        
        /// <summary>
        /// An contiguous list of data of any type that can be indexed by an int.
        /// </summary>
        private List<T> _data;

        /// <value>The max size that the lookup table can take</value>
        private const int MAX_SIZE = 2_000_000;

        public int DataCount { get => _data.Count; }

        public int LookupCount { get => _lookup.Count; }

        public DenseList()
        {
            _lookup = new List<int?>();
            _lookup.AddRange(new int?[4]); // Default size of 4
            _data = new List<T>();
        }

        /// <summary>
        /// Adding an element in the storage.
        /// </summary>
        /// <param name="id">The id that is used to reference the data in the storage.</param>
        /// <param name="data">The data that will be stored in the storage.</param>
        public void Add(int id, T data)
        {
            if (data == null)
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
            if (id == _lookup.Count)
            {
                _lookup.Add(new int?());
            }

            _data.Add(data);
            _lookup[id] = _data.Count - 1;
        }

        /// <summary>
        /// Removing an element in the storage by specifing and id that refers to the it.
        /// </summary>
        /// <param name="id">The id referencing to the element that will be removed from the storage.</param>
        public void Remove(int id)
        {
            _data.RemoveAt(GetDataId(id));
            _lookup[id] = null;
            for (int i = id + 1; i < _lookup.Count; i++)
            {
                _lookup[i] -= 1;
            }
        }

        /// <summary>
        /// Get an element from the storage.
        /// </summary>
        /// <param name="id">The id referencing to the element that will be get.</param>
        /// <returns>The data that refers to the id in the storage.</returns>
        public T Get(int id)
        {
            return _data[GetDataId(id)];
        }

        /// <summary>
        /// Determine if there is data associated to an ID.
        /// Internally we need to know if there is data in the lookup table and in the data table.
        /// </summary>
        /// <param name="id">The id possibly associated to the data.</param>
        /// <returns>True if there is data associted to the id, false otherwise</returns>
        public bool HasData(int id)
        {
            if (id < _lookup.Count && id >= 0)
            {
                if (_lookup[id] != null)
                {
                    return true;
                }
            }
            return false;
        }

        private int GetDataId(int id)
        {
            if (id >= _lookup.Count)
            {
                throw new LookupIsSmallerThanGivenId("The id given is higher than the current lookup");
            }
            return _lookup[id] ?? throw new NoDataAssociatedWithThisLookupId("There is no data associated with this id");
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

    public class NoDataAssociatedWithThisLookupId : Exception
    {
        public NoDataAssociatedWithThisLookupId(string message)
            : base(message)
        {
        }
    }

    public class LookupIsSmallerThanGivenId : Exception
    {
        public LookupIsSmallerThanGivenId(string message)
            : base(message)
        {
        }
    }

    public class DenseListOutOfBoundException : Exception
    {
        public DenseListOutOfBoundException(string message)
            : base(message)
        {
        }
    }
}
