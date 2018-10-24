using Client.Scripts;
using Engine.Base;
using Engine.Components.Cameras;
using Engine.Components.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.GameObjects
{
    public class SimpleMeshObject : GameObject
    {
        private string asset;
        public SimpleMeshObject(string asset, Vector3 location) : base(location)
        {
            this.asset = asset;
        }

        public override void Initialize()
        {
            AddComponent(new BasicEffectModel(asset));
            AddComponent(new RotateObject(new Vector3(90, 50, 0)));
            AddComponent(new DebugComponent());
            AddComponent(new FixedCamera(Vector3.Forward,1,10));


            //AddComponent(new BobbingObject(10.0f));
            AddComponent(new WaypointFollowScript(this));

            base.Initialize();
        }

    }
}
