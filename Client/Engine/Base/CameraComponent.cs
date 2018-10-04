using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public abstract class CameraComponent : Component
    {
        public Matrix View { get; set; }
        public Matrix Projection { get; set; }

        public BoundingFrustum Frustrum { get { return new BoundingFrustum(View * Projection); } }

        public float NearPlane { get; set; }
        public float FarPlane { get; set; }
        public Vector3 CurrentTarget { get; set; }
        public Vector3 CameraDirection { get; set; }
        public Vector3 UpVector { get; set; }

        public CameraComponent(string id) : base(id) { }
        public CameraComponent() : base() { }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
