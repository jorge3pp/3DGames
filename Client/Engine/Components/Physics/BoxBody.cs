using BEPUphysics.Entities.Prefabs;
using Engine.Base;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Components.Physics
{
    public class BoxBody : PhysicsComponent
    {

        public BoxBody(float mass) :base(mass)
        {

        }

        public override void Initialize()
        {
            var model = TryAndGetModelFromOwner();

            if (model != null)
            {
                Vector3[] vertices;
                int[] indices;

                ModelDataExtractor.GetVerticesAndIndicesFromModel(
                    model, out vertices, out indices);

                BoundingBox _aabb = BoundingBox.CreateFromPoints(vertices);
                Vector3 xsize = _aabb.Max - _aabb.Min;

                if(Mass <= 0)
                {
                    //kinematic
                    Entity = new Box(MathConverter.Convert(Owner.World.Translation),
                        xsize.X, xsize.Y, xsize.Z);
                }
                else
                {
                    //dynamic
                    Entity = new Box(MathConverter.Convert(Owner.World.Translation),
                        xsize.X, xsize.Y, xsize.Z, Mass);

                }

            }

            base.Initialize();
        }
    }
}
