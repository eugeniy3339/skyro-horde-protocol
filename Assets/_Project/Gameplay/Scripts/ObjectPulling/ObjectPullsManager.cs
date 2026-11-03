using System.Collections.Generic;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public static class ObjectPullsManager
    {
        public static Dictionary<GameObject, ObjectPull> objectPulls = new Dictionary<GameObject, ObjectPull>();

        private static void AddPull(GameObject pullPrefab, ObjectPull pull)
        {
            if (objectPulls.ContainsKey(pullPrefab)) return;
            objectPulls.Add(pullPrefab, pull);
        }

        public static ObjectPull GetPull(GameObject pullPrefab)
        {
            if(objectPulls.ContainsKey(pullPrefab))
                return objectPulls[pullPrefab];

            var newPull = new ObjectPull(pullPrefab);
            AddPull(pullPrefab, newPull);
            return newPull;
        }

        public enum PullType
        {
            Enemies,
            Bullets
        }
    }
}
