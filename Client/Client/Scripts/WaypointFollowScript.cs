using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.GameObjects;
using Engine;
using Engine.Base;
using Microsoft.Xna.Framework;

namespace Client.Scripts
{
    public class WaypointFollowScript : ScriptComponent
    {
        private List<Vector3> locations = new List<Vector3>();
        public List<Vector3> Locations { get { return locations; } }

        public int currentIndex { get; set; }

        public WaypointFollowScript(GameObject go) : base()
        {
            List<GameObject> waypoints = go.Scene.GetGameObjects<Waypoint>();
            foreach (Waypoint wp in waypoints)
            {
                Locations.Add(wp.Location);
            }
            
        }

        public WaypointFollowScript(List<Vector3> loc) : base()
        {
            foreach(Vector3 l in loc)
            {
                Locations.Add(l);
            }
        }

        public Vector3 MoveBetweenLocation(int index)
        {
            Vector3 direction = Locations[index] - Owner.Location;
            direction.Normalize();

            return direction;

        }

        public override void Initialize()
        {
            currentIndex = 0;
            base.Initialize();
        }

        public override void Update()
        {

            if (Vector3.Distance(Locations[currentIndex],Owner.Location) > 1.0f)
            {
                Owner.World *= Matrix.CreateTranslation(MoveBetweenLocation(currentIndex)* 10.0f * GameUtilities.DeltaTime);
            }
            else
            {
                if (currentIndex < Locations.Count-1)
                {
                    currentIndex++;
                }
                else currentIndex = 0;
            }

            base.Update();
        }
    }
}
