using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Client.GameObjects;
using Engine.Base;
using Client.Scripts;
using Engine;
using System.Diagnostics;

namespace Client.Scenes
{
    public class SimpleScene : Scene
    {
        DebugComponent cube1Debug;
        DebugComponent cube2Debug;

        public SimpleScene() : base()
        {

        }
        public override void Initialize()
        {
            AddObject(new SimplePlayerObject(new Vector3(0, 0, 10)));

            var cube1 = new SimpleMeshObject("cube", new Vector3(0, 0, 0));
            var cube2 = new SimpleMeshObject("cube", new Vector3(5, 0, 0));
            AddObject(cube1);
            AddObject(cube2);

            //AddObject(new SimpleMeshObject("cube", new Vector3(5, -7, 10)));
            //AddObject(new SimpleMeshObject("cube", new Vector3(-5, -7, 10)));
            //AddObject(new SimpleMeshObject("cube", new Vector3(5, 7, -10)));
            //AddObject(new SimpleMeshObject("cube", new Vector3(5, 7,2)));


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

            cube1Debug = cube1.GetComponent<DebugComponent>();
            cube2Debug = cube2.GetComponent<DebugComponent>();

        }

        public override void Update()
        {
            if (cube1Debug.AABB.Intersects(cube2Debug.AABB))
            {
                Debug.WriteLine("Collision!");
            }else
            {
                Debug.WriteLine("No Collision!");
            }

            base.Update();
        }
        
    }
}
