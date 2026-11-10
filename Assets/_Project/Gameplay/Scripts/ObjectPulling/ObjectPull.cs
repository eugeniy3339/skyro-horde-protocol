using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public abstract class ObjectPull { }

    public class ObjectPull<T> : ObjectPull where T : MonoBehaviour
    {
        private T objectToSpawn;
        private Queue<T> pull;

        public T GetNextObject()
        {
            int count = pull.Count;
            for(int i = 0; i<count; i++)
            {
                var obj = pull.Dequeue();
                if (obj == null)
                    continue;
                if (!obj.gameObject.activeSelf)
                {
                    pull.Enqueue(obj);
                    return obj;
                }
                pull.Enqueue(obj);
            }

            var newObject = CreateNewObject();
            return newObject;
        }

        private T CreateNewObject()
        {
            T spawnedobject = GameObject.Instantiate(objectToSpawn);
            pull.Enqueue(spawnedobject);
            return spawnedobject;
        }

        public ObjectPull(T objectToSpawnPrefab)
        {
            pull = new Queue<T>();
            objectToSpawn = objectToSpawnPrefab;
        }
    }
}
