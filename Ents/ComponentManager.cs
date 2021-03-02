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

        public void AddComponent(Entity entity, Type type, params object[] args)
        {
            // Check if there is already a container for this type
            _components.Add(type, new DenseList<IComponent>());
            
            _components[type].Add(entity.id, (IComponent)Activator.CreateInstance(type, args));

            //_components.Add(type, )
            //_components.Add(new DenseList<>)
        }

        public void RemoveComponent()
        {

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
}
