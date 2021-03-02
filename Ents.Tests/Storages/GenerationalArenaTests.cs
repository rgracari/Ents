using Ents.Storages;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ents.Tests.Storages
{
    public class GenerationalArenaTests
    {
        [Fact]
        public void Contructor_EmptyParams_ShouldNotBeNull()
        {
            GenerationalArena<string> arena = new GenerationalArena<string>();

            Assert.NotNull(arena);
        }

        [Fact]
        public void Insert_SimpleString_SizeMustIncrease()
        {
            GenerationalArena<string> arena = new GenerationalArena<string>();

            arena.Insert("data");
            arena.Insert("data");
            arena.Insert("data");

            Assert.Equal(3, arena.GetLength());
        }

        [Fact]
        public void Insert_SimpleString_ReturnValidGenerationalIndex()
        {
            GenerationalArena<string> arena = new GenerationalArena<string>();

            GenerationalIndex index =  arena.Insert("data");

            Assert.Equal(0, index.index);
        }
    }
}
