using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Client.GameObjects;
using Engine.Base;
using Client.Scripts;

namespace Client.Scenes
{
    public class SimpleScene : Scene
    {
        public SimpleScene() : base()
        {

        }
        public override void Initialize()
        {
            AddObject(new SimplePlayerObject(new Vector3(0, 0, 10)));
            AddObject(new SimpleMeshObject("cube",new Vector3(0, 0, -10)));

            AddObject(new Waypoint(new Vector3(2, 0, -1)));
            AddObject(new Waypoint(new Vector3(0, 5, -1)));
            AddObject(new Waypoint(new Vector3(10, 1, -1)));
            AddObject(new Waypoint(new Vector3(7, -5, 2)));

            //SimpleMeshObject plane = new SimpleMeshObject("plane", new Vector3(0, -2, 0));
            //AddObject(plane);

            //var comp = plane.GetComponent<RotateObject>();
            // plane.RemoveComponent(comp);

            base.Initialize();

            //plane.RemoveComponent<RotateObject>();
        }
    }
}
