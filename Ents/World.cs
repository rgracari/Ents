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

        public Entity CreateEntity()
        {
            return _entities.Create();    
        }

        public void DestroyEntity(Entity entity)
        {
            List<Type> entityComponents = _entities.GetEntityComponents(entity);

            foreach (Type type in entityComponents)
            {
                _components.RemoveComponent(entity, type);
            }

            _entities.Destroy(entity);
        }

        public void AddComponent(Entity entity, Type componentType, params object[] args)
        {
            _entities.AddComponent(entity, componentType);
            _components.AddComponent(entity, componentType, args);
        }

        public void RemoveComponent(Entity entity, Type componentType)
        {
            _entities.RemoveComponent(entity, componentType);
            _components.RemoveComponent(entity, componentType);
        }

        public override string ToString()
        {
            return $"Entities:\n{_entities}\nComponents:\n {_components}";
        }
    }
}
