using Engine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Engine.Managers;
using Microsoft.Xna.Framework.Input;

namespace Engine.Components.Input
{
    public class PlayerMovementController : ScriptComponent
    {
        public float MovementSpeed { get; set; }

        public PlayerMovementController(float speed):base()
        {
            MovementSpeed = speed;
        }

        public override void Update()
        {
            if (InputManager.IsKeyHeld(Keys.A))
            {
                Owner.World *= Matrix.CreateTranslation(-MovementSpeed * GameUtilities.DeltaTime  , 0, 0);
            }

            if (InputManager.IsKeyHeld(Keys.D))
            {
                Owner.World *= Matrix.CreateTranslation(MovementSpeed * GameUtilities.DeltaTime, 0, 0);
            }
            if (InputManager.IsKeyHeld(Keys.W))
            {
                Owner.World *= Matrix.CreateTranslation(new Vector3(0 , 0, -MovementSpeed * GameUtilities.DeltaTime));
            }
            if (InputManager.IsKeyHeld(Keys.S))
            {
                Owner.World *= Matrix.CreateTranslation(new Vector3(0 , 0, MovementSpeed * GameUtilities.DeltaTime));
            }
            if (InputManager.IsKeyHeld(Keys.Space))
            {
                Owner.World *= Matrix.CreateTranslation(new Vector3(0, MovementSpeed * GameUtilities.DeltaTime, 0));
            }
            if (InputManager.IsKeyHeld(Keys.LeftControl))
            {
                Owner.World *= Matrix.CreateTranslation(new Vector3(0, -MovementSpeed * GameUtilities.DeltaTime, 0));
            }


            base.Update();
        }
    }
}
