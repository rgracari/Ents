using Ents.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    /// <summary>
    /// Allow the user to easly manipulate the entities and the components of the ECS.
    /// This class is a kind of a facade pattern for the EntityManager and the ComponentManager.
    /// With simple methods in this class you indirectly manipulate theses classes.
    /// </summary>
    public class World
    {
        private EntityManager _entities;
        private ComponentManager _components;

        /// <summary>
        /// Basic constructor. By default it instanciates the EntityManager and the ComponentManager.
        /// </summary>
        public World()
        {
            _entities = new EntityManager();
            _components = new ComponentManager();
        }

        /// <summary>
        /// Create an unique entity and match him with a list empty of types components.
        /// </summary>
        /// <returns>A brand new Entity struct with an unique ID.</returns>
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

        /// <summary>
        /// Add a component to an existing entity.
        /// </summary>
        /// <param name="entity">The Entity that will own the component.</param>
        /// <param name="componentType">The defined type of the component.</param>
        /// <param name="args">The arguments needed by the component when construted.</param>
        public void AddComponent(Entity entity, Type componentType, params object[] args)
        {
            _entities.AddComponent(entity, componentType);
            _components.AddComponent(entity, componentType, args);
        }

        /// <summary>
        /// Retrieve the component asked by its type from an Entity. The component must exist.
        /// </summary>
        /// <typeparam name="T">The type of the component wanted to be retrived. Must be a IComponent.</typeparam>
        /// <param name="entity">The Entity that own the component.</param>
        /// <returns>The component trying to be get.</returns>
        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            return _components.GetComponent<T>(entity);
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
