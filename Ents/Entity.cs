using System;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    /// <summary>
    /// This is an Entity Struct
    /// </summary>
    public struct Entity
    {
        public int id;

        public Entity(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return $"Entity:{id}";
        }
    }
}
