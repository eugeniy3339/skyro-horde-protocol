using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public class Player : Character
    {
        public static Player Instance { get; private set; }

        private void Awake()
        {
            Player.Instance = this;
        }
    }
}
