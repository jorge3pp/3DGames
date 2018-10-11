using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Base
{
    public abstract class Scene
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        public List<GameObject> GameObjects { get { return gameObjects; } }

        private List<string> awaitingRemoval = new List<string>();

        protected bool isInitialized = false;

        public Scene() { }

        public void AddObject(GameObject newObject)
        {
            newObject.Scene = this;
            if (isInitialized) newObject.Initialize();

            newObject.OnDestroy += NewObject_OnDestroy;

            gameObjects.Add(newObject);
        }

        private void NewObject_OnDestroy(string id)
        {
            awaitingRemoval.Add(id);
        }

        public List<GameObject> GetGameObjects<T>()
        {
            return gameObjects.FindAll(go => go.GetType() == typeof(T)).Cast<GameObject>().ToList();
        }

        public int GetObjectIndexInPool(string objectID)
        {
            int index = -1;
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].ID == objectID)
                {
                    index = i;
                }
            }
            return index;
        }

        public GameObject GetObject(string objectID)
        {
            if (gameObjects.Exists(go => go.ID == objectID))
            {
                return gameObjects.Find(go => go.ID == objectID);
            } else return null;
        }

        public GameObject GetObject<T>() {
            return gameObjects.Find(go => go.GetType() == typeof(T));
        }

        public void RemoveObject(string objectID)
        {
            int index = GetObjectIndexInPool(objectID);

            if (index != -1)
            {
                gameObjects.RemoveAt(index);
            }
        }

        public T GetComponentInObject<T>(string objectID) where T : Component
        {
            GameObject GO = gameObjects.Find(go => go.ID == objectID);
            if (GO.HasComponent<T>())
            {
                return GO.GetComponent<T>();
            } else return null;
        }

        public virtual void Initialize()
        {
            foreach (GameObject go in gameObjects)
            {
                go.Initialize();
                go.Enabled = true;
            }
            isInitialized = true;
        }

        public virtual void Update()
        {
            foreach (GameObject go in gameObjects)
            {
                if (go.Enabled) go.Update();
            }
            
            foreach (string s in awaitingRemoval)
            {
                RemoveObject(s);
            }

            awaitingRemoval.Clear();
        }

        public virtual void Draw(CameraComponent camera)
        {
            foreach (GameObject go in gameObjects) go.Draw(camera);
        }

        public bool HasObject(string id)
        {
            return gameObjects.Exists(go => go.ID == id);
        }

        public bool HasObject<T>()
        {
            return gameObjects.Exists(go => go.GetType() == typeof(T));
        }

        public virtual void DrawUI()
        {
            //Virtual method with no implementation
        }


    }
}
