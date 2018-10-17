using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Client.GameObjects;
using Engine.Base;

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
            AddObject(new SimpleMeshObject("plane",new Vector3(0, -2, 0)));

            base.Initialize();
        }
    }
}
