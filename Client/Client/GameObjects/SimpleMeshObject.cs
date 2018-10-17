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

            base.Initialize();
        }

    }
}
