using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ents.Tests
{
    public struct WrongPosition
    {

    }

    public struct Position : IComponent
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Velocity : IComponent
    {
        public int x;
        public int y;

        public Velocity(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class ComponentManagerTests
    {
        [Fact]
        public void Constructor_EmptyArgs_ObjectMustExist()
        {
            ComponentManager componentManager = new ComponentManager();

            Assert.NotNull(componentManager);
        }

        [Fact]
        public void AddComponent_BasicComponent_HasComponentMustReturnTrue()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);

            componentManager.AddComponent(entity, componentType);

            Assert.True(componentManager.EntityHasComponent(entity, componentType));
        }

        [Fact]
        public void AddComponent_TwoSameComponentForSameEntity_ThrowComponentAlreadyAssociatedToEntity()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);

            componentManager.AddComponent(entity, componentType);
            
            Assert.Throws<ComponentAlreadyAssociatedToEntity>(() => componentManager.AddComponent(entity, componentType));
        }

        [Fact]
        public void AddComponent_WrongComponentDoesNotImplementRComponent_Throw()
        {
            ComponentManager componentManager = new ComponentManager();
            
            Entity entity = new Entity(0);
            Type componentType = typeof(WrongPosition);

            Assert.Throws<ComponentNotImplementIComponent>(() => componentManager.AddComponent(entity, componentType));
        }

        [Fact]
        public void AddComponent_NullComponent_Throw()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);

            Assert.Throws<ComponentMustBeNotNull>(() => componentManager.AddComponent(entity, null));
        }

        [Fact]
        public void EntityHasComponent_WithoutComponentAssociatedToId_ReturnFalse()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);

            Assert.False(componentManager.EntityHasComponent(entity, componentType));
        }

        [Fact]
        public void EntityHasComponent_WithComponentAssociatedToId_ReturnTrue()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);
            Type componentType2 = typeof(Velocity);

            componentManager.AddComponent(entity, componentType2);
            componentManager.AddComponent(entity, componentType);

            Assert.True(componentManager.EntityHasComponent(entity, componentType2));
            Assert.True(componentManager.EntityHasComponent(entity, componentType));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10)]
        [InlineData(1000000)]
        [InlineData(-150)]
        public void EntityHasComponent_WithGoodComponentButWrongAssociatedToId_ReturnFalse(int wrongId)
        {
            ComponentManager componentManager = new ComponentManager();
            Entity goodEntity = new Entity(0);
            Entity wrongEntity = new Entity(wrongId);
            Type componentType = typeof(Position);

            componentManager.AddComponent(goodEntity, componentType);

            Assert.False(componentManager.EntityHasComponent(wrongEntity, componentType));
        }

        [Fact]
        public void RemoveComponent_RemoveAComponentThatExist_RemoveAndReturnTrue()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);

            componentManager.AddComponent(entity, componentType);
            componentManager.RemoveComponent(entity, componentType);

            Assert.False(componentManager.EntityHasComponent(entity, componentType));
        }

        [Fact]
        public void RemoveComponent_AComponentThatDoesNotExist_ThrowDenseListOfTypeDoesNotExists()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);

            Assert.Throws<DenseListOfTypeDoesNotExists>(() => componentManager.RemoveComponent(entity, componentType));
        }

        [Fact]
        public void RemoveComponent_ComponentThatExistButForTheWrongEntity_ThrowEntityDoesNotHaveComponent()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity goodEntity = new Entity(0);
            Entity wrongEntity = new Entity(1);
            Type componentType = typeof(Position);
            
            // Doing this allows the DenseList to create an table for the type Position
            componentManager.AddComponent(goodEntity, componentType);

            Assert.Throws<EntityDoesNotHaveComponent>(() => componentManager.RemoveComponent(wrongEntity, componentType));
        }

        [Fact]
        public void RemoveComponent_NullComponent_ThrowsComponentMustBeNotNull()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);

            Assert.Throws<ComponentMustBeNotNull>(() => componentManager.RemoveComponent(entity, null));
        }
    }
}
