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

            Assert.True(componentManager.HasComponent(entity, componentType));
        }

        [Fact]
        public void AddComponent_ComponentWithArgs_HasComponentMustReturnTrue()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);

            componentManager.AddComponent(entity, componentType, 1, 1);

            Assert.True(componentManager.HasComponent(entity, componentType));
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

            Assert.False(componentManager.HasComponent(entity, componentType));
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

            Assert.True(componentManager.HasComponent(entity, componentType2));
            Assert.True(componentManager.HasComponent(entity, componentType));
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

            Assert.False(componentManager.HasComponent(wrongEntity, componentType));
        }

        [Fact]
        public void RemoveComponent_RemoveAComponentThatExist_RemoveAndReturnTrue()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);

            componentManager.AddComponent(entity, componentType);
            componentManager.RemoveComponent(entity, componentType);

            Assert.False(componentManager.HasComponent(entity, componentType));
        }

        [Fact]
        public void RemoveComponent_AComponentThatDoesNotExist_ThrowEntityDoesNotHaveComponent()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Type componentType = typeof(Position);

            Assert.Throws<EntityDoesNotHaveComponent>(() => componentManager.RemoveComponent(entity, componentType));
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

        [Fact]
        public void GetComponent_BasicComponentExist_ReturnSameComponent()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            componentManager.AddComponent(entity, typeof(Position));

            IComponent expected = componentManager.GetComponent<Position>(entity);

            Assert.Equal(new Position(), expected);
        }

        [Fact]
        public void GetComponent_BasicComponentDoesNotExist_ThrowsEntityDoesNotHaveComponent()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);

            Assert.Throws<EntityDoesNotHaveComponent>(() => componentManager.GetComponent<Position>(entity));
        }

        [Fact]
        public void GetComponents_OneComponentEntity_ReturnAListWithTheComponent()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Position component = new Position(15, 15);
            componentManager.AddComponent(entity, component.GetType(), component.x, component.y);
            List<IComponent> expected = new List<IComponent> { component };

            List<IComponent> actual = componentManager.GetComponents(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetComponents_TwoComponentsEntity_ReturnAListWithTheTwoComponents()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Position position = new Position(15, 15);
            Velocity velocity = new Velocity(30, 30);
            componentManager.AddComponent(entity, position.GetType(), position.x, position.y);
            componentManager.AddComponent(entity, velocity.GetType(), velocity.x, velocity.y);
            List<IComponent> expected = new List<IComponent> { position, velocity };

            List<IComponent> actual = componentManager.GetComponents(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetComponents_NoComponentEntity_ReturnAnEmptyList()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            List<IComponent> expected = new List<IComponent>();

            List<IComponent> actual = componentManager.GetComponents(entity);

            Assert.Equal(expected, actual);
        }

        /*[Fact]
        public void GetComponentsOfType_GetTypeWithoutComponent_EmptyList()
        {
            ComponentManager componentManager = new ComponentManager();
            List<Position> expected = new List<Position>();

            List<Position> actual = componentManager.GetComponentsOfType<Position>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetComponentsOfType_GetTypeWithOnComponent_ListWithOneItem()
        {
            ComponentManager componentManager = new ComponentManager();
            Entity entity = new Entity(0);
            Velocity component = new Velocity(3, 3);
            componentManager.AddComponent(entity, component.GetType(), component.x, component.y);
            List<Velocity> expected = new List<Velocity> { component };

            List<Velocity> actual = componentManager.GetComponentsOfType<Velocity>();

            Assert.Equal(expected, actual);
        }*/
    }
}
