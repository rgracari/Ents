#define LOG_INFO

using Ents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

// ECS      -> Archetypes, Queries, Components, Entities, Systems, Worlds, Groups
// Advenced -> Order of group execution, struct/class dif, sharedComponent
// Quality  -> profiling, tests, codecov, documentation
// Generational arena

/// <summary>
/// 
/// World world = new World()
/// world.register<Position>();
/// world.register<Velocity>();
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

namespace Sandbox
{
    public class Program
    {
        static void Main( string[] args)
        {
        }
    }
}
