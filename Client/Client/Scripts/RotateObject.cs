using Engine;
using Engine.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Scripts
{
    public class RotateObject : ScriptComponent
    {
        public Vector3 RotationToBeApplied { get; set; }

        public RotateObject(Vector3 rotation) : base()
        {
            RotationToBeApplied = rotation;
        }

        public override void Update()
        {
            var translation = Owner.Location;

            //subtract current translation
            Owner.World *= Matrix.CreateTranslation(-translation);
            

            Owner.World *= Matrix.CreateRotationX(MathHelper.ToRadians(RotationToBeApplied.X) * GameUtilities.DeltaTime);

            Owner.World *= Matrix.CreateRotationY(MathHelper.ToRadians(RotationToBeApplied.Y) * GameUtilities.DeltaTime);

            Owner.World *= Matrix.CreateRotationZ(MathHelper.ToRadians(RotationToBeApplied.Z) * GameUtilities.DeltaTime);
            

            //add translation back on
            Owner.World *= Matrix.CreateTranslation(translation);

            base.Update();
        }

    }
}
