using System;
using System.Collections.Generic;
using System.Text;

namespace Ents.Sandbox.Components
{
    public struct Position : IComponent
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"Position -> x: {x}, y: {y}";
        }
    }
}
