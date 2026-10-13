using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class AIMovement : Movement
    {
        private Transform _destinationTransform;

        private void Update()
        {
            if (_destinationTransform == null) { moveDir = Vector2.zero; return; }

            Vector2 toDestination = (Vector2)_destinationTransform.position - (Vector2)transform.position;

            print(toDestination);

            if (toDestination.magnitude > 0.1f)
                moveDir = toDestination.normalized;
            else
                moveDir = Vector2.zero;
        }

        public void MoveTo(Transform destination)
        {
            _destinationTransform = destination;
        }

        public void MoveTo(Vector2 destination)
        {
            // Use a temporary helper transform to reuse MoveTo(Transform) logic
            if (_tempDestination == null)
                _tempDestination = new GameObject("AIMovement_TempDestination").transform;

            _tempDestination.position = destination;
            MoveTo(_tempDestination);
        }

        private Transform _tempDestination;

        private void OnDestroy()
        {
            if (_tempDestination != null)
                Destroy(_tempDestination.gameObject);
        }
    }
}
