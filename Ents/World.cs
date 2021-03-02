using Ents.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    public class World
    {
        private EntityManager _entities;

        private ComponentManager _components;

        public World()
        {
            _entities = new EntityManager();
            _components = new ComponentManager();
        }

        /*public Entity CreateEntity()
        {
            
        }*/
    }
}
