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
        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        [Fact]
        public void Constructor_Nothing_CreateObject()
        {
            DenseList<string> denseList = new DenseList<string>();

            Assert.NotNull(denseList);
        }

        /// <summary>
        /// ADD
        /// </summary>
        [Theory]
        [InlineData(10, "data")]
        [InlineData(5550, "pedro")]
        public void Add_SimpleStringData_ReturnSameString(int id, string expected)
        {
            DenseList<string> denseList = new DenseList<string>();

            denseList.Add(id, expected);

            Assert.Equal(expected, denseList.Get(id));
        }

        [Theory]
        [InlineData(10, 45.2)]
        [InlineData(5550, 999999.25454)]
        public void Add_SimpleDoubleData_ReturnDoubleString(int id, double expected)
        {
            DenseList<double> denseList = new DenseList<double>();

            denseList.Add(id, expected);

            Assert.Equal(expected, denseList.Get(id));
        }

        [Fact]
        public void Add_StringAtEntityIdZero_ReturnSameString()
        {
            DenseList<string> denseList = new DenseList<string>();
            string expected = "data";
            int id = 0;

            denseList.Add(id, expected);

            Assert.Equal(expected, denseList.Get(id));
        }

        [Fact]
        public void Add_SimpleDataCountDataShouldIncrease_CountPlusOne()
        {
            DenseList<string> denseList = new DenseList<string>();
            int initialCount = denseList.DataCount;

            denseList.Add(0, "data");

            Assert.Equal(initialCount + 1, denseList.DataCount);
        }

        [Fact]
        public void Add_PassNullData_ThrowArgumentNull()
        {
            DenseList<string> denseList = new DenseList<string>();
            string expected = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                denseList.Add(0, expected);
            });
        }

        [Fact]
        public void Add_StringWithMaxId_ThrowOutOfBoundError()
        {
            DenseList<string> denseList = new DenseList<string>();

            Assert.Throws<DenseListOutOfBoundException>(() =>
            {
                denseList.Add(int.MaxValue, "data");
            });
        }

        [Fact]
        public void Add_NegativeId_ThrowOutOfBound()
        {
            DenseList<string> denseList = new DenseList<string>();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                denseList.Add(-15, "data");
            });
        }

        /// <summary>
        /// GET
        /// </summary>
        [Theory]
        [InlineData(0, true)]
        [InlineData(125, true)]
        [InlineData(699, false)]
        public void Get_SimpleDataWithSimpleId_ReturnSameData(int id, bool expected)
        {
            DenseList<bool> denseList = new DenseList<bool>();

            denseList.Add(id, expected);

            Assert.Equal(expected, denseList.Get(id));
        }

        [Fact]
        public void Get_NegativeId_Throw()
        {
            DenseList<long> denseList = new DenseList<long>();
            int negativeId = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                denseList.Get(negativeId);
            });
        }

        [Fact]
        public void Get_IdWithoutDataAssociated_ThrowArgument()
        {
            DenseList<string> denseList = new DenseList<string>();
            denseList.Add(20, "data");

            Assert.Throws<ArgumentException>(() =>
            {
                denseList.Get(15);
            });
        }

        /// <summary>
        /// GET
        /// </summary>
        [Fact]
        public void Remove_RemovingBasicData_CountShouldDecrease()
        {
            DenseList<string> denseList = new DenseList<string>();
            int index = 0;
            denseList.Add(index, "data");

            denseList.Remove(index);

            Assert.Equal(0, denseList.DataCount);
        }

        [Fact]
        public void Remove_RemovingMultipleData_CountShouldDecrease()
        {
            DenseList<string> denseList = new DenseList<string>();
            int index = 5;
            denseList.Add(0, "data");
            denseList.Add(index, "data");
            denseList.Add(10, "data");
            int countBeforeRemoving = denseList.DataCount;

            denseList.Remove(index);

            Assert.Equal(countBeforeRemoving - 1, denseList.DataCount);
        }

        [Fact]
        public void Remove_TryRemovingItemWithoutDataInserted_ThrowArgument()
        {
            DenseList<string> denseList = new DenseList<string>();

            Assert.Throws<ArgumentException>(() =>
            {
                denseList.Remove(1);
            });
        }

        [Fact]
        public void Remove_NegativeId_ThrowArgumentOutOfRange()
        {
            DenseList<string> denseList = new DenseList<string>();
            denseList.Add(0, "data");
            denseList.Add(1, "data");

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                denseList.Remove(-1);
            });
        }

        // Remove wrongs ids
    }
}
