using System;
using System.Collections.Generic;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class Shooting : MonoBehaviour
    {
        [Header("Shooting Settings")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _shootCooldown = 0.25f;

        private ObjectPull _bulletPool;
        private float _cooldownTimer;

        public event Action onShootCooldown;

        private void Awake()
        {
            _bulletPool = ObjectPullsManager.GetPull(_bulletPrefab.gameObject);
        }

        private void Update()
        {
            if (_cooldownTimer > 0f)
            {
                _cooldownTimer -= Time.deltaTime;
                if(_cooldownTimer <= 0f)
                {
                    onShootCooldown?.Invoke();
                }
            }
        }

        /// <summary>
        /// Fires a bullet in the given direction if the cooldown has elapsed.
        /// </summary>
        public void FireIfCanTo(Vector2 dir)
        {
            if (dir == Vector2.zero) return;
            if (_cooldownTimer > 0f) return;

            Bullet bullet = GetPooledBullet();
            bullet.SetShooter(this);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.gameObject.SetActive(true);
            bullet.Launch(dir);

            _cooldownTimer = _shootCooldown;
        }

        // ── Pool ──────────────────────────────────────────────────────────────

        private Bullet GetPooledBullet()
        {
            return _bulletPool.GetNextObject().GetComponent<Bullet>();
        }
    }
}
