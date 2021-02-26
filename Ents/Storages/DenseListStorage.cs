using System;
using System.Collections.Generic;
using System.Text;

namespace Ents.Storage
{
    /// <summary>
    /// DenseListStorage allows you to create a dense datastructure
    /// </summary>
    public class DenseListStorage<T>
    {
        private List<int> _lookup;
        private List<T> _data;

        /// <summary>
        /// The basic constructor of DenseListStorage
        /// </summary>
        public DenseListStorage()
        {
            _lookup = new List<int>();
            _data = new List<T>();
        }
    }
}
