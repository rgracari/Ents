#define LOG_INFO

// ECS      -> Archetypes, Queries, Components, Entities, Systems, Worlds, Groups
// Advenced -> Order of group execution, struct/class dif, sharedComponent
// Quality  -> profiling, tests, codecov, documentation

///<summary>
/// TODO:
/// Write comments for ComponentManager
/// Test if method<GenericType>() is better than method(typeof(type))
/// 
/// </summary>


using System;

/// <summary>
/// 
/// World world = new World()
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
            World world = new World();
            Entity entity1 = world.CreateEntity();
            Entity entity2 = world.CreateEntity();

            world.AddComponent(entity1, typeof(Position), 15, 15);

            world.GetComponent<Velocity>(entity1);

            Console.WriteLine(world);
        }
    }
}
