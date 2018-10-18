using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Engine.Base;
using Engine.Components.Input;
using Engine.Components.Cameras;
using Microsoft.Xna.Framework;

namespace Client.GameObjects
{
    public class Waypoint : GameObject
    {
        public Vector3 Location { get; set; }

        public Waypoint(Vector3 location) : base(location)
        {
            Location = location;
        }
        public override void Initialize()
        {
            base.Initialize();
        }


    }
}
