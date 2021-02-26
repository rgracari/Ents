using System;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    public class Query<T1, T2> where T1 : struct where T2 : struct
    {
        public List<Velocity> velocities = new List<Velocity>();
        public List<Position> positions = new List<Position>();

        public Query()
        {
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());
            velocities.Add(new Velocity());

            positions.Add(new Position());
            positions.Add(new Position());
            positions.Add(new Position());
            positions.Add(new Position());
            positions.Add(new Position());
            positions.Add(new Position());
            positions.Add(new Position());
            positions.Add(new Position());
            positions.Add(new Position());
            positions.Add(new Position());
            positions.Add(new Position());
        }

        public void Execute()
        {
            for (int i = 0; i < positions.Count; i++)
            {
                //Each(positions[i] as T1, velocities[i] as T2);
            }
        }

        public virtual void Each(ref T1 position, ref T2 velocity)
        {

        }
    }
}
