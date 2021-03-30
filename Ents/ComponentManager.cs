using Ents.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    /// <summary>
    /// Manage all the components in a defined world with this class.
    /// 
    /// The ComponentManager automaticly creates storage for differents types of
    /// IComponent when needed internally.
    /// All the components need to refer to an existing Entity.
    /// </summary>
    public class ComponentManager
    {
        private Dictionary<Type, DenseList<IComponent>> _components;
   
        public ComponentManager()
        {
            _components = new Dictionary<Type, DenseList<IComponent>>();
        }

        /// <summary>
        /// Store a defined component that refers to an actual Entity in the ComponentManager.
        /// The compoent can be accessed and modified latter.
        /// </summary>
        /// <param name="entity">The Entity that own the component.</param>
        /// <param name="componentType">The defined type of the component.</param>
        /// <param name="args">The arguments needed by the component when construted.</param>
        public void AddComponent(Entity entity, Type componentType, params object[] args)
        {
            if (componentType == null)
            {
                throw new ComponentMustBeNotNull("Component must be not null and implement the IComponent interface");
            }

            CreateDenseListIfTypeIsNotRegistered(componentType);
            ComponentTypeAssignableFromComponentInterface(componentType);

            if (_components[componentType].HasData(entity.id) == true)
            {
                throw new ComponentAlreadyAssociatedToEntity("There is already a component of the same type associated to this id.");
            }

            _components[componentType].Add(entity.id, (IComponent)Activator.CreateInstance(componentType, args));
        }

        /// <summary>
        /// Remove a component in the storage where it belong to.
        /// You will be not able to access it from the entity.
        /// </summary>
        /// <param name="entity">The Entity that own the component.</param>
        /// <param name="componentType">The defined type of the component.</param>
        public void RemoveComponent(Entity entity, Type componentType)
        {
            if (componentType == null)
            {
                throw new ComponentMustBeNotNull("Component must be not null and implement the IComponent interface");
            }

            if (!IsDenseListOfTypeAlreadyExists(componentType))
            {
                throw new DenseListOfTypeDoesNotExists("The DenseList of the type requested doesn't exists yet.");
            }

            try
            {
                _components[componentType].Remove(entity.id);
            }
            catch
            {
                throw new EntityDoesNotHaveComponent("The entity does not have the component.");
            }
        }

        /// <summary>
        /// Retrieve the component asked by its type from an Entity. The component must exist.
        /// </summary>
        /// <typeparam name="T">The type of the component wanted to be retrived. Must be a IComponent.</typeparam>
        /// <param name="entity">The Entity that own the component.</param>
        /// <returns>The component </returns>
        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            if (!HasComponent(entity, typeof(T)))
            {
                throw new EntityDoesNotHaveComponent("The entity does not have the component.");
            }

            return (T)_components[typeof(T)].Get(entity.id);
        }

        /// <summary>
        /// Check if the Entity is associated to a specified Component.
        /// </summary>
        /// <param name="entity">The Entity that own the component.</param>
        /// <param name="componentType">The defined type of the component.</param>
        /// <returns>True if the entity has the component False otherwise.</returns>
        public bool HasComponent(Entity entity, Type componentType)
        {
            if (!_components.ContainsKey(componentType))
            {
                return false;
            }

            return _components[componentType].HasData(entity.id);
        }

        private void ComponentTypeAssignableFromComponentInterface(Type type)
        {
            if (!(typeof(IComponent).IsAssignableFrom(type)))
            {
                throw new ComponentNotImplementIComponent("The component");
            }
        }

        private bool IsDenseListOfTypeAlreadyExists(Type type)
        {
            return _components.ContainsKey(type);
        }

        private void CreateDenseListIfTypeIsNotRegistered(Type type)
        {
            if (!IsDenseListOfTypeAlreadyExists(type))
            {
                _components.Add(type, new DenseList<IComponent>());
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in _components)
            {
                stringBuilder.AppendLine($"{item.Key}: {item.Value}");
            }
            return stringBuilder.ToString();
        }
    }

    public class DenseListOfTypeDoesNotExists : Exception
    {
        public DenseListOfTypeDoesNotExists(string message)
            : base(message)
        {
        }
    }

    public class ComponentAlreadyAssociatedToEntity : Exception
    {
        public ComponentAlreadyAssociatedToEntity(string message)
            : base(message)
        {
        }
    }

    public class ComponentMustBeNotNull : Exception
    {
        public ComponentMustBeNotNull(string message)
            : base(message)
        {
        }
    }

    public class EntityDoesNotHaveComponent : Exception
    {
        public EntityDoesNotHaveComponent(string message)
            : base(message)
        {
        }
    }
}
