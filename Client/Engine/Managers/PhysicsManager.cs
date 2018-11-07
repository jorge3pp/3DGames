using BEPUphysics.Entities;
using BEPUphysics;
using BEPUphysics.BroadPhaseEntries;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers
{
    public sealed class PhysicsManager : GameComponent
    {
        public static Space WorldSpace;
        
        public PhysicsManager(Game game) : base(game)
        {
            WorldSpace = new Space();

            SetGravity(-9.8f);

            game.Components.Add(this);
        }

        public static void SetGravity(float gravity)
        {
            if (WorldSpace != null)
            {
                WorldSpace.ForceUpdater.Gravity = new BEPUutilities.Vector3(0, gravity, 0);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public static void AddEntity(Entity newEntity)
        {
            if(newEntity != null)
            {
                if (!WorldSpace.Entities.Contains(newEntity))
                {
                    WorldSpace.Add(newEntity);
                }
            }
        }

        public static void RemoveEntity(Entity entity)
        {
            if (WorldSpace.Entities.Contains(entity))
            {
                    WorldSpace.Remove(entity);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (WorldSpace != null)
            {
                WorldSpace.Update(GameUtilities.DeltaTime);
            }
            base.Update(gameTime);
        }

        public static void AddStaticMesh(StaticMesh mesh)
        {
            WorldSpace.Add(mesh);
        }
        public static void RemoveStaticMesh(StaticMesh mesh)
        {
            WorldSpace.Remove(mesh);
        }
    }
}
