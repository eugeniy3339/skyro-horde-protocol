using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class ObjectPull
    {
        private GameObject objectToSpawn;
        private Queue<GameObject> pull;

        public GameObject GetNextObject()
        {
            int count = pull.Count;
            for(int i = 0; i<count; i++)
            {
                var obj = pull.Dequeue();
                if(!obj.gameObject.activeSelf)
                {
                    pull.Enqueue(obj);
                    return obj;
                }
                pull.Enqueue(obj);
            }

            var newObject = CreateNewObject();
            return newObject;
        }

        private GameObject CreateNewObject()
        {
            GameObject spawnedobject = GameObject.Instantiate(objectToSpawn.gameObject);
            pull.Enqueue(spawnedobject);
            return spawnedobject;
        }

        public ObjectPull(GameObject objectToSpawnPrefab)
        {
            pull = new Queue<GameObject>();
            objectToSpawn = objectToSpawnPrefab;
        }
    }
}
