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
        public void Constructor_Nothing_CreateObject()
        {
            DenseList<string> denseList = new DenseList<string>();

            Assert.NotNull(denseList);
        }

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

            Assert.Throws<NoDataAssociatedWithThisLookupId>(() =>
            {
                denseList.Get(15);
            });
        }

        [Fact]
        public void GetAll_EmptyDenseList_ReturnEmptyList()
        {
            DenseList<string> denseList = new DenseList<string>();
            List<string> expected = new List<string>();

            List<string> actual = denseList.GetAll();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("data1", 0)]
        [InlineData("", 0)]
        [InlineData("data2", 150)]
        [InlineData("data4", 30)]
        public void GetAll_OneItemDenseList_ReturnListWithTheItem(string data, int id)
        {
            DenseList<string> denseList = new DenseList<string>();
            denseList.Add(id, data);
            List<string> expected = new List<string> { data };

            List<string> actual = denseList.GetAll();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("data1", 0, "data2", 15)]
        [InlineData("data3", 35, "data4", 128)]
        [InlineData("data5", 445, "data6", 889)]
        [InlineData("data5", 40, "data6", 35)]
        public void GetAll_TwoItemsWithSpaceBetween_ReturnListWithTheseItems(string data1, int id1, string data2, int id2)
        {
            DenseList<string> denseList = new DenseList<string>();
            denseList.Add(id1, data1);
            denseList.Add(id2, data2);
            List<string> expected = new List<string> { data1, data2 };

            List<string> actual = denseList.GetAll();

            Assert.Equal(expected, actual);
        }

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

            Assert.Throws<NoDataAssociatedWithThisLookupId>(() =>
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

        [Fact]
        public void Remove_TryRemoveSomethingOutsiteOfTheLookupRange_ThrowNoDataAssociatedWithThisLookupId()
        {
            DenseList<string> denseList = new DenseList<string>();

            // The default size of a lookup is 4
            Assert.Throws<LookupIsSmallerThanGivenId>(() => denseList.Remove(5));
        }

        [Fact]
        public void Remove_RemoveDataBeforeAntotherOne_GetSameCorrectData()
        {
            DenseList<string> denseList = new DenseList<string>();

            denseList.Add(0, "data0");
            denseList.Add(1, "data1");

            denseList.Remove(0);

            Assert.Equal("data1", denseList.Get(1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        [InlineData(10)]
        public void HasData_WithPreviousData_ReturnTrue(int id)
        {
            DenseList<string> denseList = new DenseList<string>();
            Entity entity = new Entity(id);

            denseList.Add(entity.id, "data");

            Assert.True(denseList.HasData(entity.id));
        }

        [Fact]
        public void HasData_WithoutPreviousData_ReturnFalse()
        {
            DenseList<string> denseList = new DenseList<string>();
            Entity entity = new Entity(0);

            Assert.False(denseList.HasData(entity.id));
        }

        [Fact]
        public void HasData_ImpossibleNegativeId_ReturnFalse()
        {
            DenseList<string> denseList = new DenseList<string>();
            Entity entity = new Entity(0);

            Assert.False(denseList.HasData(-1));
        }
    }
}
