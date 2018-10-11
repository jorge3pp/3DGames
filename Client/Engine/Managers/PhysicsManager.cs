using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers
{
    public sealed class PhysicsManager : DrawableGameComponent
    {
        public PhysicsManager(Game game) : base(game)
        {
            game.Components.Add(this);
        }
    }
}
