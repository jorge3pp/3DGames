using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Base;
using Microsoft.Xna.Framework;

namespace Engine.Managers
{
    public sealed class CameraManager : GameComponent
    {
        private static Dictionary<string, CameraComponent> cameras;

        private static CameraComponent activeCamera;
        public static CameraComponent ActiveCamera { get { return activeCamera; } }

        public CameraManager(Game game) : base(game)
        {
            game.Components.Add(this);
        }

        public static void SetActiveCamera(string id)
        {
            if (null != activeCamera)
            {
                if (id != activeCamera.ID)
                {
                    if (cameras.ContainsKey(id))
                    {
                        activeCamera = cameras[id];
                    }
                }
            }else
            {
                if (cameras.ContainsKey(id))
                {
                    activeCamera = cameras[id];
                }
            }
        }
        public static void AddCamera(CameraComponent camera)
        {
            if (!cameras.ContainsKey(camera.ID))
            {
                cameras.Add(camera.ID,camera);
                if (cameras.Count==1) activeCamera = camera;
            }
        }
        public static void Clear()
        {
            cameras.Clear();
            activeCamera = null;
        }
        public static void RemoveCamera(string id)
        {
            if (cameras.ContainsKey(id))
            {
                cameras.Remove(id);
                if (activeCamera.ID == id) activeCamera = null;
            }
        }

        public static List<string> GetCurrentCameraIDs()
        {
            return cameras.Keys.ToList();
        }

        public static List<CameraComponent> GetCurrentCameras()
        {
            return cameras.Values.ToList();
        }

        public static void EnableActiveCamera()
        {
            if (activeCamera != null)
            {
                activeCamera.Enabled = true;
            }
        }
        public static void DisableActiveCamera()
        {
            if (activeCamera != null)
            {
                activeCamera.Enabled = false;
            }
        }
    }
}
