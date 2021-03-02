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

        /*[Fact]
        public void World_CreateAnBasicEntity_EntityWithIdZero()
        {
            // Arrage
            World world = new World();

            // Act
            //Entity entity = world.CreateEntity();

            // Assert
            Assert.Equal(new Entity(0), entity);
        }*/
    }
}
