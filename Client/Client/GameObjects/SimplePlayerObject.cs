using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Base;
using Engine.Components.Input;
using Engine.Components.Cameras;
using Microsoft.Xna.Framework;
using Client.Scripts;

namespace Client.GameObjects
{
    public class SimplePlayerObject: GameObject
    {
        public SimplePlayerObject(Vector3 location) : base(location)
        {

        }
        public override void Initialize()
        {
            AddComponent(new FixedCamera(new Vector3(0, 0, -1)));
            AddComponent(new PlayerMovementController(20));

            base.Initialize();
        }
    }
}
