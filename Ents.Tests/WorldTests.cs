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
        public void AddComponent_BasicComponentWithoutArgs()
        {
            World world = new World();

            Entity entity = world.CreateEntity();

            world.AddComponent(entity, typeof(Velocity));
        }
    }
}
