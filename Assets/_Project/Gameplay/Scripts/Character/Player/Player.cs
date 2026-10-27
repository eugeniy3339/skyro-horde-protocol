using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class Player : Character
    {
        public static Player Instance { get; private set; }

        private void OnEnable()
        {
            Player.Instance = this;
        }

        private void OnDisable()
        {
            Player.Instance = null;
        }
    }
}
