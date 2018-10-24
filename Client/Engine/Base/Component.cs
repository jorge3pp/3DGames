using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public abstract class Component
    {
        public string ID { get; set; }
        public bool Enabled { get; set; }
        public GameObject Owner { get; set; }
        public event ObjectIDHandler OnDestroy;

        public Component()
        {
            ID = this.GetType().Name + Guid.NewGuid();
            Enabled = true;
        }
        public Component(string id)
        {
            ID = ID;
            Enabled = true;
        }

        public virtual void PostInitialize() { }
        public virtual void Initialize() { }
        public virtual void Update() { }
        public virtual void Destroy()
        {
            if(OnDestroy != null)
            {
                OnDestroy(ID);
            }
        }


    }
}
