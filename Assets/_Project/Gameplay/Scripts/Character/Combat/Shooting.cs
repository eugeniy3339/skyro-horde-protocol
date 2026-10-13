using System.Collections.Generic;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class Shooting : MonoBehaviour
    {
        [Header("Shooting Settings")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _shootCooldown = 0.25f;

        private Queue<Bullet> _bulletPool;
        private float _cooldownTimer;

        private void Awake()
        {
            InitPool();
        }

        private void Update()
        {
            if (_cooldownTimer > 0f)
                _cooldownTimer -= Time.deltaTime;
        }

        /// <summary>
        /// Fires a bullet in the given direction if the cooldown has elapsed.
        /// </summary>
        public void FireIfCanTo(Vector2 dir)
        {
            if (dir == Vector2.zero) return;
            if (_cooldownTimer > 0f) return;

            Bullet bullet = GetPooledBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.gameObject.SetActive(true);
            bullet.Launch(dir);

            _cooldownTimer = _shootCooldown;
        }

        // ── Pool ──────────────────────────────────────────────────────────────

        private void InitPool()
        {
            if (_bulletPrefab == null)
            {
                Debug.LogError("[Shooting] Bullet prefab is not assigned!", this);
                return;
            }

            int poolSize = CalculatePoolSize();
            _bulletPool = new Queue<Bullet>(poolSize);

            for (int i = 0; i < poolSize; i++)
            {
                InstantiateBullet();
            }
        }

        private Bullet InstantiateBullet()
        {
            Bullet b = Instantiate(_bulletPrefab);
            b.gameObject.SetActive(false);
            _bulletPool.Enqueue(b);
            return b;
        }

        /// <summary>
        /// Calculates how many bullets are alive at once at maximum fire rate:
        ///   ceil(maxLifetime / shootCooldown) + 1 buffer
        /// </summary>
        private int CalculatePoolSize()
        {
            float maxLifetime = _bulletPrefab.MaxLifetime;
            int needed = Mathf.CeilToInt(maxLifetime / Mathf.Max(_shootCooldown, 0.001f)) + 1;
            return Mathf.Max(needed, 4); // always keep at least 4
        }

        private Bullet GetPooledBullet()
        {
            // Find the first inactive bullet and move it to the back
            int count = _bulletPool.Count;
            for (int i = 0; i < count; i++)
            {
                Bullet b = _bulletPool.Dequeue();
                if (!b.gameObject.activeInHierarchy)
                {
                    _bulletPool.Enqueue(b);
                    return b;
                }
                _bulletPool.Enqueue(b);
            }

            // All bullets are active — instantiate an extra one and add to pool
            Debug.LogWarning("[Shooting] Pool exhausted, expanding.");
            Bullet extra = InstantiateBullet();
            return extra;
        }
    }
}
