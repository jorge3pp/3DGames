using Client.Scripts;
using Engine.Base;
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
            AddComponent(new RotateObject(new Vector3(0, 30, 0)));
            //AddComponent(new BobbingObject(10.0f));


            List<Vector3> loc = new List<Vector3>();

            List<GameObject> waypoints = Scene.GetGameObjects<Waypoint>();
            foreach(Waypoint wp in waypoints)
            {
                loc.Add(wp.Location);
            }

            AddComponent(new WaypointFollowScript(loc));

            base.Initialize();
        }

    }
}
