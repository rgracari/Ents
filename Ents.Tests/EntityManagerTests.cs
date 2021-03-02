using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ents.Tests
{
    public struct GoodPositionTest : IComponent
    {
        public int x;
        public int y;
    }

    public struct WrongPositionTest
    {
        public int x;
        public int y;
    }

    public class EntityManagerTests
    {

        [Fact]
        public void EntityManager_CreatingNewObject_ObjectMustExist()
        {
            EntityManager entityManager = new EntityManager();

            Assert.NotNull(entityManager);
        }

        [Fact]
        public void Create_OneEntity_ReturnEntityWithIdZero()
        {
            EntityManager entityManager = new EntityManager();

            Entity entity = entityManager.Create();

            Assert.Equal(0, entity.id);
        }

        [Fact]
        public void Create_ThreeEntity_ReturnEntityWithIds()
        {
            EntityManager entityManager = new EntityManager();

            Entity entity1 = entityManager.Create();
            Entity entity2 = entityManager.Create();
            Entity entity3 = entityManager.Create();

            Assert.Equal(0, entity1.id);
            Assert.Equal(1, entity2.id);
            Assert.Equal(2, entity3.id);
        }

        [Fact]
        public void Destroy_ExistingEntity_CountShouldDecrease()
        {
            EntityManager entityManager = new EntityManager();

            Entity entity = entityManager.Create();
            entityManager.Create();

            Assert.Equal(2, entityManager.Count);
            entityManager.Destroy(entity);

            Assert.Equal(1, entityManager.Count);
        }

        [Fact]
        public void Destroy_WrongEntity_ThrowEntityIdDoesNotExist()
        {
            EntityManager entityManager = new EntityManager();

            entityManager.Create();
            Entity wrongEntity = new Entity(-1);

            Assert.Throws<EntityIdDoesNotExist>(() => entityManager.Destroy(wrongEntity));
        }

        [Fact]
        public void AddComponent_GoodComponent_ComponentTypeMustBeStored()
        {
            EntityManager entityManager = new EntityManager();
            Type componentType = typeof(GoodPositionTest);

            Entity entity = entityManager.Create();
            entityManager.AddComponent(entity, componentType);

            Assert.True(entityManager.EntityHasComponent(entity, componentType));
        }

        [Fact]
        public void AddComponent_WrongComponent_EntityComponentNotImplement()
        {
            EntityManager entityManager = new EntityManager();
            Type componentType = typeof(WrongPositionTest);

            Entity entity = entityManager.Create();

            Assert.Throws<EntityComponentNotImplementIComponent>(() =>
            {
                entityManager.AddComponent(entity, componentType);
            });
        }

        [Fact]
        public void AddComponent_WrongEntity_ThrowEntityIdDoesNotExist()
        {
            EntityManager entityManager = new EntityManager();
            Type componentType = typeof(GoodPositionTest);

            Entity wrongEntity = new Entity(-1);

            Assert.Throws<EntityIdDoesNotExist>(() =>
            {
                entityManager.AddComponent(wrongEntity, componentType);
            });
        }

        [Fact]
        public void RemoveComponent_WrongEntity_ThrowEntityIdDoesNotExist()
        {
            EntityManager entityManager = new EntityManager();
            Type componentType = typeof(GoodPositionTest);

            Entity wrongEntity = new Entity(-1);

            Assert.Throws<EntityIdDoesNotExist>(() =>
            {
                entityManager.RemoveComponent(wrongEntity, componentType);
            });
        }

        [Fact]
        public void RemoveComponent_TryDeleteExistingComponent_ReturnTrueAndRemoveComponent()
        {
            Type componentType = typeof(GoodPositionTest);
            EntityManager entityManager = new EntityManager();
            Entity entity = entityManager.Create();

            entityManager.AddComponent(entity, componentType);
            
            Assert.True(entityManager.RemoveComponent(entity, componentType));
            Assert.False(entityManager.EntityHasComponent(entity, componentType));
        }

        [Fact]
        public void RemoveComponent_TryDeleteNotExistingComponent_ReturnFalse()
        {
            Type componentType = typeof(GoodPositionTest);
            EntityManager entityManager = new EntityManager();

            Entity entity = entityManager.Create();

            Assert.False(entityManager.RemoveComponent(entity, componentType));
        }

        [Fact]
        public void EntityHasComponent_EntityAsTheComponent_ReturnTrue()
        {
            Type componentType = typeof(GoodPositionTest);
            EntityManager entityManager = new EntityManager();

            Entity entity = entityManager.Create();
            entityManager.AddComponent(entity, componentType);

            Assert.True(entityManager.EntityHasComponent(entity, componentType));
        }

        [Fact]
        public void EntityHasComponent_EntityDoesNotHaveTheComponent_ReturnFalse()
        {
            Type componentType = typeof(GoodPositionTest);
            EntityManager entityManager = new EntityManager();

            Entity entity = entityManager.Create();

            Assert.False(entityManager.EntityHasComponent(entity, componentType));
        }

        [Fact]
        public void EntityHasComponent_WrongEntity_ThrowEntityIdDoesNotExist()
        {
            Type componentType = typeof(GoodPositionTest);
            EntityManager entityManager = new EntityManager();
            Entity wrongEntity = new Entity(-1);

            Assert.Throws<EntityIdDoesNotExist>(() => entityManager.EntityHasComponent(wrongEntity, componentType));
        }
    }
}
