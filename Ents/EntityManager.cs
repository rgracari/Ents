using System;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    /// <summary>
    /// A class that allows to easly manage the entities.
    /// At runtime all entities will have an unique Id and an list of component types.
    /// You must create Entity from the EntityManager. He is smart enough to recycle Ids
    /// and get quite fast the components from an entity.
    /// </summary>
    public class EntityManager
    {
        private Dictionary<Entity, List<Type>> _entities;
        private Queue<int> _queueIds;
        private int _idCount;

        /// <summary>
        /// Get the number of all the entities that has been registerd in the Entity Manager.
        /// </summary>
        public int Count { get => _entities.Count; }

        public EntityManager()
        {
            _entities = new Dictionary<Entity, List<Type>>();
            _queueIds = new Queue<int>();
            _idCount = 0;
        }

        /// <summary>
        /// Create an unique entity and match him with a list empty of types components.
        /// </summary>
        /// <returns>An brand new Entity struct with an unique ID.</returns>
        public Entity Create()
        {
            if (_queueIds.Count <= 0)
            {
                _queueIds.Enqueue(_idCount);
                _idCount += 1;

                if (_idCount > 2_000_000)
                {
                    throw new EntityIdOutOfRange("You can't have more than 2 000 000 entities.");
                }
            }

            Entity newEntity = new Entity(_queueIds.Dequeue());
            _entities.Add(newEntity, new List<Type>());
            return newEntity;
        }

        /// <summary>
        /// Destroy an given entity that must exists for the entityManager. The entity Id will now
        /// be free to be taken for another entity.
        /// </summary>
        /// <param name="entity">The entity needed to be destroy. (must exist)</param>
        public void Destroy(Entity entity)
        {
            CheckIfEntityExists(entity);

            bool hasBeenRemoved = _entities.Remove(entity);

            if (!hasBeenRemoved)
            {
                throw new UnableToDestroyEntity();
            }

            _queueIds.Enqueue(entity.id);
        }

        /// <summary>
        /// Adding a component type in the list of component types associated to an entity.
        /// </summary>
        /// <remarks>
        /// You shoud must use typeof(YourComponent) for the component type.
        /// </remarks>
        /// <param name="entity">The entity that will receive the component type. (Must exist)</param>
        /// <param name="componentType">The type of the component that will be inserted to the entity. (Must inherit from IComponent)</param>
        public void AddComponent(Entity entity, Type componentType)
        {
            CheckIfEntityExists(entity);

            if (!(typeof(IComponent).IsAssignableFrom(componentType)))
            {
                throw new ComponentNotImplementIComponent("The component");
            }

            _entities[entity].Add(componentType);
        }

        /// <summary>
        /// Remove an given component type from an entity.
        /// </summary>
        /// <param name="entity">The entity that will loose the component type. (Must exist)</param>
        /// <param name="componentType">The type of the component that will be removed from the entity. (Must inherit from IComponent)</param>
        /// <returns>If the component has been removed successfuly return true otherwise false.</returns>
        public bool RemoveComponent(Entity entity, Type componentType)
        {
            CheckIfEntityExists(entity);

            return _entities[entity].Remove(componentType);
        }

        /// <summary>
        /// Check if an given entity has a special compononent type.
        /// </summary>
        /// <param name="entity">An entity that must be created from the entityManager hitself.</param>
        /// <param name="componentType">The type of an component. (Must inherit from IComponent)</param>
        /// <returns>True if the component exists in the entity, false otherwise.</returns>
        public bool EntityHasComponent(Entity entity, Type componentType)
        {
            CheckIfEntityExists(entity);

            return _entities[entity].Exists((type) => type == componentType);
        }

        /// <summary>
        /// Check if the entity has been created already.
        /// If it's the case the entity id will not be available until it's been destroyed.
        /// </summary>
        /// <param name="entity">The entity we want to test.</param>
        /// <returns>True if it already exists False otherwise.</returns>
        public bool IsEntityAlive(Entity entity)
        {
            return _entities.ContainsKey(entity);
        }

        // Mettre des commentaires
        public List<Type> GetEntityComponents(Entity entity)
        {
            CheckIfEntityExists(entity);

            return _entities[entity];
        }

        private void CheckIfEntityExists(Entity entity)
        {
            bool isEntityExist = _entities.ContainsKey(entity);

            if (!isEntityExist)
            {
                throw new EntityIdDoesNotExist("The specified entity doesn't exist in the the entity manager.");
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var ent in _entities)
            {
                stringBuilder.Append($"[{ent.Key.id}]: ");
                foreach (Type type in ent.Value)
                {
                    stringBuilder.Append($"{type.Name}, ");
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }

    public class EntityIdOutOfRange : Exception
    {
        public EntityIdOutOfRange(string message)
            : base(message)
        {
        }
    }

    public class UnableToDestroyEntity : Exception
    {
        public UnableToDestroyEntity()
        {
        }
    }

    public class EntityIdDoesNotExist : Exception
    {
        public EntityIdDoesNotExist(string message)
            : base(message)
        {
        }
    }

    public class ComponentNotImplementIComponent : Exception
    {
        public ComponentNotImplementIComponent(string message)
            : base(message)
        {
        }
    }
}
