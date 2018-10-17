using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Managers;
using Engine.Base;

namespace Engine.Components.Cameras
{
    public class FixedCamera : CameraComponent
    {
        public FixedCamera(Vector3 direction) : base()
        {
            CameraDirection = direction;
        }

        public override void Initialize()
        {
            NearPlane = 1.0f;
            FarPlane = 10000.0f;
            UpVector = Vector3.Up;
            CameraDirection.Normalize();

            Update();

            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.AspectRatio,
                NearPlane,
                FarPlane);

            CameraManager.AddCamera(this);

            base.Initialize();
        }

        public override void Update()
        {
            CurrentTarget = Owner.Location + CameraDirection;

            View = Matrix.CreateLookAt(
               Owner.Location,
                CurrentTarget,
                UpVector);

            base.Update();
        }
    }
}
