using UnityEngine;

namespace HordeProtocol.Gameplay
{
    [RequireComponent(typeof(AIMovement))]
    public class Enemy : Character
    {
        private AIMovement _aiMovement;

        [SerializeField] private float _damage = 100f;

        private void Awake()
        {
            _aiMovement = GetComponent<AIMovement>();
        }

        private void Start()
        {
            if (Player.Instance != null)
                _aiMovement.MoveTo(Player.Instance.transform);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
                playerHealth.Damage(_damage);
        }
    }
}
