using System;
using Xunit;

namespace Ents.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void NotFailingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
