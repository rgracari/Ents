using System;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    public enum Access
    {
        Read,
        Write
    }

    public struct Request<T> where T : IComponent
    {
        private Access _access;

        public Request(Access access = Access.Read)
        {
            _access = access;
        }
    }
}
