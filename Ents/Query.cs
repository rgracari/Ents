using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    public class Query
    {
        //private List<Request> _requests;

        public Query()
        {
            //_requests = new List<Request>();
        }

        public IEnumerable<Entity> Iterate(World world)
        {
            foreach (Entity entity in world.GetEntities())
            {
                yield return entity;
            }
        }
    }
}
