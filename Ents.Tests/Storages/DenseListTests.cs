using Ents.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace Ents.Tests.Storages
{
    public class DenseListTests
    {
        [Fact]
        public void Add_Entity_ReturnVoid()
        {
            // Arrage
            DenseList<string> denseList = new DenseList<string>();
            Entity entity = new Entity(10);
            string data = "data";

            // Act
            denseList.Add(entity, data);

            // Assert
            //Assert.Equal("3333", denseList.Get(entity));
        }
    }
}
