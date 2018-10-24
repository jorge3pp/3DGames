using Engine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Engine.Components.Graphics;
using Engine.Components.Cameras;
using Engine;
using Engine.Managers;

namespace Client.Scripts
{
    class DebugComponent : Component
    {
        public BoundingBox AABB;
        public BoundingSphere AABS;

        BasicEffectModel modelComponent;
        FixedCamera cameraComponent;


        public override void PostInitialize()
        {
            if (Owner.HasComponent<BasicEffectModel>())
            {
                //get model component
                modelComponent = Owner.GetComponent<BasicEffectModel>();
                
                if (modelComponent.Model != null)
                {
                    //extract vertices from the model
                    Vector3[] vertices;
                    int[] indices;

                    ModelDataExtractor.GetVerticesAndIndicesFromModel
                        (modelComponent.Model,out vertices,out indices);
                    AABB = BoundingBox.CreateFromPoints(vertices);
                    AABB.Min = Vector3.Transform(AABB.Min, Owner.World);
                    AABB.Max = Vector3.Transform(AABB.Max, Owner.World);

                    AABS = BoundingSphere.CreateFromPoints(vertices);
                    AABS.Center = Vector3.Transform(AABS.Center, Owner.World);
                    AABS.Radius = 2.0f;
                    
                }
            }

            if (Owner.HasComponent<FixedCamera>())
            {
                cameraComponent = Owner.GetComponent<FixedCamera>();
            }

            base.PostInitialize();
        }

        public override void Update()
        {
            if(AABB != null && AABS != null)
            {
                DebugManager.AddBoundingBox(AABB, Color.LawnGreen);
                DebugManager.AddBoundingSphere(AABS, Color.BlueViolet);
            }

            if (cameraComponent != null)
            {
                //point the camera in the same direction as the GameObject
                cameraComponent.CameraDirection = Owner.World.Forward;


                DebugManager.AddBoundingFrustum(cameraComponent.Frustrum,Color.Red);

            }

            base.Update();
        }
    }
}
