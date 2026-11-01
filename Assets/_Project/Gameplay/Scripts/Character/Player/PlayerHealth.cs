using System;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class PlayerHealth : Health
    {
        public static event Action onPlayerDied;

        protected override void DieAction()
        {
            base.DieAction();
            onPlayerDied?.Invoke();
        }
    }
}
