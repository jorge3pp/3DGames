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

        public void AddComponent (Component newComponent)
        {
            newComponent.Owner = this;

            if (isInitialized)
            {
                newComponent.Initialize();
            }

            newComponent.OnDestroy += NewComponent_OnDestroy;
            components.Add(newComponent);
        }
        private void NewComponent_OnDestroy(string ID)
        {
            awaitingRemoval.Add(ID);
        }

        public void RemoveComponent(int index)
        {
            components.RemoveAt(index);
        }
        public void RemoveComponent(string id)
        {
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
        public void RemoveComponent(Component c)
        {
            components.Remove(c);
        }
        public void Destroy()
        {
            components.Clear();
            //Raise the OnDestroy event
            OnDestroy.Invoke(ID);
        }
        public void Update()
        {
           foreach(Component c in components)
            {
                if (c.Enabled == true)
                {
                    c.Update();
                }
            }

           foreach(string s in awaitingRemoval)
            {
                RemoveComponent(s);
            }
        }
        public void Draw(CameraComponent cc)
        {
            foreach(RenderComponent rc in components)
            {
                if (rc.Enabled == true) rc.Draw(cc);
            }
        }

        public float getDistanceTo(GameObject go)
        {
            GameObject current = this;
            
            return current.getDistanceTo(go);
        }

        public bool hasComponent()
        {
            return true;
        }

        public Component getComponent(string s)
        {
            Component result = null;
            int index = -1;
            for(int i=0;i<components.Count;i++)
            {
                if (components[i].ID == s)
                {
                    index = i;
                }
            }

            if (index != -1) result = components[index];

            return result;
        }
        public Component getComponent(Type t)
        {
            Component result = null;
            
            return result;
        }
    }
}
