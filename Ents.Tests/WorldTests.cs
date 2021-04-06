using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ents.Tests
{
    public class WorldTests
    {
        [Fact]
        public void World_CreatingNewWord_ObjectMustExist()
        {
            World world = new World();

            Assert.IsType<World>(world);
        }

        [Fact]
        public void CreateEntity_WithoutArgs_EntityWithIdZero()
        {
            World world = new World();
            Entity entity = world.CreateEntity();

            Entity expected = new Entity(0);

            Assert.Equal(expected, entity);
        }

        [Fact]
        public void CreateEntity_MultipleCallWithoutArgs_EntityWithIdZero()
        {
            World world = new World();
            Entity expected = new Entity(3);

            // Arbitrary 4 calls of CreateEntity to simulate multiple creations
            world.CreateEntity();
            world.CreateEntity();
            world.CreateEntity();
            Entity actual = world.CreateEntity();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DestroyEntity_EntityWithoutComponent_EntityMustBeFree()
        {
            World world = new World();
            Entity entity = world.CreateEntity();

            world.DestroyEntity(entity);

            Assert.False(world.IsEntityAlive(entity));
        }

        [Fact]
        public void DestroyEntity_EntityWithOneComponent_EntityMustBeFreeAndComponentDeleted()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            world.AddComponent(entity, typeof(Position), 15, 15);

            world.DestroyEntity(entity);

            Assert.False(world.IsEntityAlive(entity));
            //world.
        }

        [Fact]
        public void IsEntityAlive_EntityIsAlive_ReturnTrue()
        {
            World world = new World();
            Entity entity = world.CreateEntity();

            bool actual = world.IsEntityAlive(entity);

            Assert.True(actual);
        }

        [Fact]
        public void IsEntityAlive_EntityDoesNotExist_ReturnFalse()
        {
            World world = new World();
            Entity entity = new Entity(0);

            bool actual = world.IsEntityAlive(entity);

            Assert.False(actual);
        }

        [Fact]
        public void AddComponent_BasicComponent_ComponentShouldBeAdded()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Velocity expected = new Velocity(40, 40);

            world.AddComponent(entity, expected.GetType(), expected.x, expected.y);
            Velocity actual = world.GetComponent<Velocity>(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddComponent_AddTwoSameComponents_ThrowsComponentAlreadyAssociatedToEntity()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Velocity component1 = new Velocity(40, 40);

            world.AddComponent(entity, component1.GetType(), component1.x, component1.y);

            Assert.Throws<ComponentAlreadyAssociatedToEntity>(() => world.AddComponent(entity, component1.GetType(), component1.x, component1.y));
        }

        [Fact]
        public void AddComponent_AddTwoSameComponentsWithDifferentArgs_ThrowsComponentAlreadyAssociatedToEntity()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Velocity component1 = new Velocity(40, 40);
            Velocity component2 = new Velocity(10, 10);

            world.AddComponent(entity, component1.GetType(), component1.x, component1.y);

            Assert.Throws<ComponentAlreadyAssociatedToEntity>(() => world.AddComponent(entity, component2.GetType(), component2.x, component2.y));
        }

        [Fact]
        public void AddComponent_AddTwoDifferentComponents_ThrowsComponentAlreadyAssociatedToEntity()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Position position = new Position(30, 30);
            Velocity expected = new Velocity(40, 40);

            world.AddComponent(entity, position.GetType(), position.x, position.y);
            world.AddComponent(entity, expected.GetType(), expected.x, expected.y);
            Velocity actual = world.GetComponent<Velocity>(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetComponent_BasicComponentExist_ReturnSameComponent()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Velocity expected = new Velocity(8, 8);
            
            world.AddComponent(entity, expected.GetType(), expected.x, expected.y);
            Velocity actual = world.GetComponent<Velocity>(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetComponent_MultipleComponentsExist_ReturnSameComponents()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Velocity expected1 = new Velocity(8, 8);
            Position expected2 = new Position(16, 16);

            world.AddComponent(entity, expected1.GetType(), expected1.x, expected1.y);
            world.AddComponent(entity, expected2.GetType(), expected2.x, expected2.y);

            Position actual2 = world.GetComponent<Position>(entity);
            Velocity actual1 = world.GetComponent<Velocity>(entity);

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
        }

        [Fact]
        public void GetComponent_BasicComponentDoesNotExist_ThrowsEntityDoesNotHaveComponent()
        {
            World world = new World();
            Entity entity = world.CreateEntity();

            Assert.Throws<EntityDoesNotHaveComponent>(() => world.GetComponent<Velocity>(entity));  
        }

        [Fact]
        public void GetComponents_EntityWithoutComponents_EmptyIComponentList()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            List<IComponent> expected = new List<IComponent>();

            List<IComponent> actual = world.GetComponents(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetComponents_EntityWithOneComponent_ListWithTheComponent()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Position component = new Position(40, 40);
            world.AddComponent(entity, component.GetType(), component.x, component.y);
            List<IComponent> expected = new List<IComponent> { component };

            List<IComponent> actual = world.GetComponents(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetComponents_EntityWithTwoComponents_ListWithTwoComponents()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Position position = new Position(40, 40);
            Velocity velocity = new Velocity(20, 20);
            world.AddComponent(entity, position.GetType(), position.x, position.y);
            world.AddComponent(entity, velocity.GetType(), velocity.x, velocity.y);
            List<IComponent> expected = new List<IComponent> { position, velocity };

            List<IComponent> actual = world.GetComponents(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetComponents_TwoEntityWithTwoComponents_TwoListWithComponents()
        {
            World world = new World();
            Entity entity1 = world.CreateEntity();
            Entity entity2 = world.CreateEntity();

            Position position1 = new Position(40, 40);
            Velocity velocity1 = new Velocity(20, 20);

            Velocity velocity2 = new Velocity(5, 5);

            world.AddComponent(entity1, position1.GetType(), position1.x, position1.y);
            world.AddComponent(entity1, velocity1.GetType(), velocity1.x, velocity1.y);

            world.AddComponent(entity2, velocity2.GetType(), velocity2.x, velocity2.y);

            List<IComponent> expected1 = new List<IComponent> { position1, velocity1 };
            List<IComponent> expected2 = new List<IComponent> { velocity2 };

            List<IComponent> actual1 = world.GetComponents(entity1);
            List<IComponent> actual2 = world.GetComponents(entity2);

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
        }

        [Fact]
        public void GetComponents_TwoEntityWithTwoComponentsAndOneDeleted_OneListWithComponents()
        {
            World world = new World();
            Entity entity1 = world.CreateEntity();
            Entity entity2 = world.CreateEntity();

            Position position1 = new Position(40, 40);
            Velocity velocity1 = new Velocity(20, 20);

            Velocity velocity2 = new Velocity(5, 5);

            world.AddComponent(entity1, position1.GetType(), position1.x, position1.y);
            world.AddComponent(entity1, velocity1.GetType(), velocity1.x, velocity1.y);

            world.DestroyEntity(entity1);

            world.AddComponent(entity2, velocity2.GetType(), velocity2.x, velocity2.y);

            List<IComponent> expected1 = new List<IComponent> { };
            List<IComponent> expected2 = new List<IComponent> { velocity2 };

            List<IComponent> actual1 = world.GetComponents(entity1);
            List<IComponent> actual2 = world.GetComponents(entity2);

            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
        }

        [Fact]
        public void RemoveComponent_RemoveOneComponentFromEntity_ListWithOneLessComponent()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Position position = new Position(5, 5);
            Velocity velocity = new Velocity(51, 51);
            world.AddComponent(entity, position.GetType(), position.x, position.y);
            world.AddComponent(entity, velocity.GetType(), velocity.x, velocity.y);
            List<IComponent> expected = new List<IComponent> { velocity };

            world.RemoveComponent(entity, typeof(Position));
            List<IComponent> actual = world.GetComponents(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveComponent_RemoveTwoComponentFromEntity_ListZeroComponent()
        {
            World world = new World();
            Entity entity = world.CreateEntity();
            Position position = new Position(5, 5);
            Velocity velocity = new Velocity(51, 51);
            world.AddComponent(entity, position.GetType(), position.x, position.y);
            world.AddComponent(entity, velocity.GetType(), velocity.x, velocity.y);
            List<IComponent> expected = new List<IComponent> { };

            world.RemoveComponent(entity, typeof(Position));
            world.RemoveComponent(entity, typeof(Velocity));
            List<IComponent> actual = world.GetComponents(entity);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveComponent_RemoveComponentThatDoesNotExist_ThrowsEntityDoesNotHaveComponent()
        {
            World world = new World();
            Entity entity = world.CreateEntity();

            Assert.Throws<EntityDoesNotHaveComponent>(() => world.RemoveComponent(entity, typeof(Position)));
        }

        [Fact]
        public void RemoveComponent_RemoveComponentFromEntityThatDoesNotExist_ThrowsEntityIdDoesNotExist()
        {
            World world = new World();
            Entity entity = new Entity(0);

            Assert.Throws<EntityIdDoesNotExist>(() => world.RemoveComponent(entity, typeof(Position)));
        }

        [Fact]
        public void RemoveComponent_RemoveComponentThatIsNotIComponent_ThrowsEntityDoesNotHaveComponent()
        {
            World world = new World();
            Entity entity = world.CreateEntity();

            Assert.Throws<EntityDoesNotHaveComponent>(() => world.RemoveComponent(entity, typeof(WrongPosition)));
        }
    }
}
