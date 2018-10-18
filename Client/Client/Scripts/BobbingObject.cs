using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.Base;
using Microsoft.Xna.Framework;

namespace Client.Scripts
{
    public class BobbingObject : ScriptComponent
    {
        public float BobbingAmount { get; set; }
        public Vector3 StartLocation { get; set; }
        private float MaxY, MinY;
        private bool IsMovingUp = true;

        public BobbingObject(float bobbing): base()
        {
            BobbingAmount = bobbing;
        }
        public BobbingObject() : base()
        {
            BobbingAmount = GameUtilities.Random.Next(1,10);
        }
        public override void Initialize()
        {
            MaxY = 5.0f;
            MinY = -5.0f;
            StartLocation = new Vector3(0,5,0);
            Owner.World *= Matrix.CreateTranslation(StartLocation);

            base.Initialize();
        }

        public override void Update()
        {
            if (IsMovingUp)
            {
                if (MaxY < Owner.Location.Y)
                {
                    IsMovingUp = false;
                }
                else
                {
                    Owner.World *= Matrix.CreateTranslation(0, BobbingAmount * GameUtilities.DeltaTime, 0);
                }

            }
            else
            {
                if (MinY > Owner.Location.Y)
                {
                    IsMovingUp = true;
                }
                else
                {
                    Owner.World *= Matrix.CreateTranslation(0, -BobbingAmount * GameUtilities.DeltaTime, 0);
                }
            }

            base.Update();
        }

        public void Stop()
        {

        }
    }
}
