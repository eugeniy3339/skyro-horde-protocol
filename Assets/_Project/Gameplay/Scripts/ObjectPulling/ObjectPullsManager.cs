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

        public static ObjectPull<T> GetPull<T>(GameObject pullPrefab) where T : MonoBehaviour
        {
            if(objectPulls.TryGetValue(pullPrefab, out var pull))
            {
                if(pull is ObjectPull<T> typedPull)
                    return typedPull;
            }

            var newPull = new ObjectPull<T>(pullPrefab.GetComponent<T>());
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
