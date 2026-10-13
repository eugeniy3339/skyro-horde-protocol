using UnityEngine;

namespace HordeProtocol.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _maxLifetime = 3f;

        private Rigidbody2D _rb;
        private float _lifetimeTimer;
        private Shooting _shooter;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _lifetimeTimer = _maxLifetime;
        }

        private void Update()
        {
            _lifetimeTimer -= Time.deltaTime;
            if (_lifetimeTimer <= 0f)
                Deactivate();
        }

        public void Launch(Vector2 dir)
        {
            _rb.linearVelocity = dir.normalized * _speed;
        }

        public void SetShooter(Shooting shooter)
        {
            _shooter = shooter;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_shooter != null && other.gameObject == _shooter.gameObject) return;

            if (other.TryGetComponent<Enemy>(out _))
                Destroy(other.gameObject);

            Deactivate();
        }

        private void Deactivate()
        {
            _rb.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }

        public float MaxLifetime => _maxLifetime;
        public float Speed => _speed;
    }
}
