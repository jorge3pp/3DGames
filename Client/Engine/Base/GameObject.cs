using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public abstract class GameObject
    {
        public string ID;

        public Scene Scene { get; set; }
        public bool Enabled { get; set; }

        public event ObjectIDHandler OnDestroy;

        public Matrix World { get; set; }
        public Vector3 Location { get { return World.Translation; } }
        public Vector3 Scale { get { return World.Scale; } }
        public Quaternion Rotation { get { return World.Rotation; } }

        public GameObject Parent { get; set; }
        public List<GameObject> Children { get; set; }

        public List<Component> components = new List<Component>();
        public List<Component> Components { get { return components; } }

        private List<string> awaitingRemoval = new List<string>();

        private bool isInitialized = false;
        public bool IsInitialized { get { return isInitialized; } }


        public GameObject()
        {
            ID = this.GetType().Name + Guid.NewGuid();
            Enabled = true;
            World = Matrix.Identity;
            Children = new List<GameObject>();
        }
        public GameObject(Vector3 location)
        {
            ID = this.GetType().Name + Guid.NewGuid();
            Enabled = true;
            World = Matrix.Identity * Matrix.CreateTranslation(location);
            Children = new List<GameObject>();
        }

        public virtual void Initialize()
        {
            foreach(Component c in components)
            {
                c.Initialize();
                c.Enabled = true;
            }
            isInitialized = true;
        }

        public void AddComponent (Component component)
        {
            component.Owner = this;

            if (isInitialized)
            {
                component.Initialize();
            }

            component.OnDestroy += Component_OnDestroy;
            components.Add(component);
        }
        private void Component_OnDestroy(string id)
        {
            awaitingRemoval.Add(id);
        }

        public void RemoveComponent(int index)
        {
            if(index< components.Count) components.RemoveAt(index);
        }
        public void RemoveComponent(string id)
        {
            /*components.IndexOf(components.Find(c => c.ID == id));
              for(int i=0;i<components.Count;i++){
                if(components[i].ID==id) RemoveComponent(i);
              }
            */
            int index = -1;
            for (int i= 0; i < components.Count;i++)
            {
                if (components[i].ID == id)
                {
                    index = i;
                }
            }
            if (index != -1) RemoveComponent(index);
        }
        public void RemoveComponent(Component component)
        {
            components.Remove(component);
        }
        public void RemoveComponent<T>()
        {
            int index = components.FindIndex(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)));

            if (index != -1) RemoveComponent(index);
        }
        public void Destroy(bool shouldDestroyChildren)
        {
            components.Clear();
            //Raise the OnDestroy event
            if (OnDestroy != null)
            {
                OnDestroy(ID);
                //OnDestroy.Invoke(ID);
            }
        }
        public virtual void Update()
        {
            //loop over components
            //components may call Destroy
            //cannot modify collection while in a foreach
           foreach(Component c in components)
            {
                c.Update();
            }

           //store IDs in a list for removal
           foreach(string s in awaitingRemoval)
            {
                RemoveComponent(s);
            }
        }
        public void Draw(CameraComponent camera)
        {
            foreach (RenderComponent rc in components.OfType<RenderComponent>())
            {
                if (rc.Enabled == true) rc.Draw(camera);
            }
        }
        public float GetDistanceTo(GameObject otherObject)
        {
            return Vector3.Distance(Location, otherObject.Location);
            //GameObject current = this;
            //return current.getDistanceTo(otherObject);
        }
        public bool HasComponent<T>() where T : Component
        {
            return components.Any(c=> c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)));
        }

        public Component getComponent(string id)
        {
            /*
            Component result = null;
            int index = -1;
            for(int i=0;i<components.Count;i++)
            {
                if (components[i].ID == id)
                {
                    index = i;
                }
            }

            if (index != -1) result = components[index];

            return result;
            */

            return components.Find(c => c.ID == id);
        }
        public Component getComponent(Type componentType)
        {
            return components.Find(c => c.GetType() == componentType);
        }
        public T GetComponent<T>() where T : Component
        {
            return (T)components.Find(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)));
        }
        public List<Component> GetComponents(Type componentType)
        {
            return components.FindAll(c => c.GetType() == componentType).Cast<Component>().ToList();
        }
        //TO DO: possibly clean up conversion from List<Component> to List<T>
        public List<T> GetComponents<T>() where T : Component
        {
            return components.FindAll(c => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T))).Cast<T>().ToList();
        }
    }
}
