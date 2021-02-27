using System;
using System.Collections.Generic;
using System.Text;

namespace Ents.Storage
{
    /// <summary>
    /// DenseListStorage allows you to create a dense datastructure
    /// </summary>
    public class DenseList<T>
    {
        private List<int> _lookup;
        private List<T> _data;

        /// <summary>
        /// The basic constructor of DenseListStorage
        /// </summary>
        public DenseList()
        {
            _lookup = new List<int>();
            _data = new List<T>();
        }

        /// <summary>
        /// Adding an item in the storage class
        /// </summary>
        /// <param name="entity">Entity is needed to get the parameters</param>
        /// <param name="item">The data you want to insert in the dense storage</param>
        public void Add(Entity entity, T item)
        {
            //_data.Add(item);
            if (_lookup.Count < entity.id)
            {
                int diff = entity.id - _lookup.Count;
                _lookup.AddRange(new int[diff]);

                //List<string> llist = new List<string>();
                //_lookup.ForEach((sa) => Console.WriteLine(sa));
                //Console.WriteLine("Lookup not big enough");
            }
            //_lookup.Insert(entity.id, _data.Count);
        }

        public void Remove(Entity entity)
        {
            _data.RemoveAt(_lookup[entity.id]);
            _lookup.RemoveAt(entity.id);
        }

        public T Get(Entity entity)
        {
            return _data[_lookup[entity.id]];
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _lookup.Count; i++)
            {
                stringBuilder.AppendLine($"[{i}] -> [{_lookup[i]}] -> [{i}]");
            }
            return stringBuilder.ToString();
        }
    }
}
