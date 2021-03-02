using Ents.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ents
{
    public class ComponentManager
    {
        private Dictionary<Type, DenseList<IComponent>> _components;
   
        public ComponentManager()
        {
            _components = new Dictionary<Type, DenseList<IComponent>>();
        }

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

        public bool EntityHasComponent(Entity entity, Type componentType)
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
