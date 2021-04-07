#define LOG_INFO

// ECS      -> Archetypes, Queries, Components, Entities, Systems, Worlds, Groups
// Advenced -> Order of group execution, struct/class dif, sharedComponent
// Quality  -> profiling, tests, codecov, documentation

using Ents.Sandbox.Components;
using System;
using System.Collections.Generic;

/// <summary>
/// World world = new World()
/// 
/// foreach (var entity in Registry.EntitiesWith<Mob>())
/// {
///     ref var mob = ref entity.Get<Mob>();
/// </summary>

namespace Ents.Sandbox
{
    public class Program
    {
        static void Main(string[] args)
        {
            World world = new World();

            world.CreateEntity();
            world.CreateEntity();
            Entity entity = world.CreateEntity();
            world.CreateEntity();

            world.DestroyEntity(entity);

            Query query = new Query();

            foreach (Entity item in query.Iterate(world))
            {
                Console.WriteLine(item);
            }
        }
    }
}


