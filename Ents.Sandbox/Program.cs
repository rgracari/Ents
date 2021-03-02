#define LOG_INFO

// ECS      -> Archetypes, Queries, Components, Entities, Systems, Worlds, Groups
// Advenced -> Order of group execution, struct/class dif, sharedComponent
// Quality  -> profiling, tests, codecov, documentation
// Generational arena


using Ents.Storage;
using Ents.Storages;
using System;
/// <summary>
/// 
/// World world = new World()
/// 
/// Entity player = world.CreateEntity();
/// world.AddComponent<Position>(player, 1.0, 1.0);
/// 
/// Entity player = world.CreateEntity(typeof(Position));
/// 
/// world.SetComponentData()
/// 
/// foreach (var entity in Registry.EntitiesWith<Mob>())
/// {
///     ref var mob = ref entity.Get<Mob>();
/// 
/// </summary>
namespace Ents.Sandbox
{
    public struct Position : IComponent
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"Position -> x: {x}, y: {y}";
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

        public override string ToString()
        {
            return $"Velocity -> x: {x}, y: {y}";
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            EntityManager entityManager = new EntityManager();

            Entity entity = entityManager.Create();

            entityManager.AddComponent(entity, typeof(Position));
            entityManager.AddComponent(entity, typeof(Velocity));

            Console.WriteLine(entityManager);
        }
    }
}
