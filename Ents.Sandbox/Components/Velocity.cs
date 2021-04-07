using System;
using System.Collections.Generic;
using System.Text;

namespace Ents.Sandbox.Components
{
    public struct Velocity : IComponent
    {
        public int x;
        public int y;

        public Velocity(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"Velocity -> x: {x}, y: {y}";
        }
    }
}
