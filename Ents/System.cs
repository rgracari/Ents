using System;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    public abstract class System
    {
        private Query _query;

        public Query Query
        {
            get => _query;
        }

        public System(Query query)
        {
            _query = query;
        }

        public abstract void Update();
    }
}
