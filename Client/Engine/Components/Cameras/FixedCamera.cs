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
            NearPlane = 1.0f;
            FarPlane = 1000.0f;
        }
        public FixedCamera(Vector3 direction, float nearPlane, float farPlane) : base()
        {
            CameraDirection = direction;
            NearPlane = nearPlane;
            FarPlane = farPlane;
        }

        public override void Initialize()
        {
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
