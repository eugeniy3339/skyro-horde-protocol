using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;
using static HordeProtocol.Gameplay.Health;

namespace HordeProtocol.Gameplay
{
    public class Health : MonoBehaviour
    {
        private List<ICommand> commandsStack = new List<ICommand>();

        [SerializeField] private float maxHealth = 100f;
        private float _ch;
        private float curHealth
        {
            get { return _ch; }
            set
            {
                value = Mathf.Clamp(value, 0f, maxHealth);
                if (_ch == value) return;

                _ch = value;
                onHealthChanged?.Invoke(value);

                if (value <= 0f)
                    Die();
            }
        }

        private HealthState _cs = HealthState.Alive;
        public HealthState CurState
        {
            get { return _cs; }
            private set
            {
                _cs = value;
            }
        }

        public event Action<float> onHealthChanged;
        public event Action<float> onDamaged;
        public event Action<float> onHealed;
        public event Action onDied;



        protected virtual void Awake()
        {
            CurState = HealthState.Alive;
            curHealth = maxHealth;
        }



        public void Damage(float damage)
        {
            DamageCommand damageCommand = new DamageCommand(damage, this);
            TryToExecuteCommand(damageCommand);
        }

        public void Heal(float health)
        {
            HealthCommand healthCommand = new HealthCommand(health, this);
            TryToExecuteCommand(healthCommand);
        }

        public void Die()
        {
            DieCommand dieCommand = new DieCommand(this);
            TryToExecuteCommand(dieCommand);
        }



        private void TryToExecuteCommand(ICommand command)
        {
            if (!command.Execute())
                commandsStack.Add(command);
        }

        private void TryToExcecuteStackedCommands()
        {
            for(int i = commandsStack.Count - 1; i >= 0; i--)
            {
                if (commandsStack[i].Execute())
                    commandsStack.RemoveAt(i);
            }
        }



        private void OnGameStateChanged(GameStateHandler.GameState gameState)
        {
            if (gameState == GameStateHandler.GameState.Run)
                TryToExcecuteStackedCommands();
        }

        private void OnEnable()
        {
            GameStateHandler.onGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateHandler.onGameStateChanged -= OnGameStateChanged;
        }



        public enum HealthState
        {
            Alive,
            Dead
        }



        public class DamageCommand : ICommand
        {
            private float _damage;
            private Health _healthToDamage;

            public event Action<ICommand> onCommandExecuted;

            public bool Execute()
            {
                if (!CanExecute()) return false;
                Damage(_healthToDamage, _damage);
                onCommandExecuted?.Invoke(this);
                return true;
            }

            private void Damage(Health healthToDamage, float damage)
            {
                healthToDamage.curHealth -= damage;
            }

            public bool CanExecute()
            {
                return _healthToDamage != null && GameStateHandler.CurGameState == GameStateHandler.GameState.Run;
            }

            public DamageCommand(float damage, Health healthToDamage)
            {
                _damage = damage;
                _healthToDamage = healthToDamage;
            }
        }

        public class HealthCommand : ICommand
        {
            private float _health;
            private Health _healthToHeal;

            public event Action<ICommand> onCommandExecuted;

            public bool Execute()
            {
                if (!CanExecute()) return false;
                Heal(_healthToHeal, _health);
                onCommandExecuted?.Invoke(this);
                return true;
            }

            private void Heal(Health healthToHealth, float health)
            {
                healthToHealth.curHealth += health;
            }

            public bool CanExecute()
            {
                return _healthToHeal != null && GameStateHandler.CurGameState == GameStateHandler.GameState.Run;
            }

            public HealthCommand(float health, Health healthToHeal)
            {
                _health = health;
                _healthToHeal = healthToHeal;
            }
        }

        public class DieCommand : ICommand
        {
            private Health _health;

            public event Action<ICommand> onCommandExecuted;

            public bool Execute()
            {
                if (!CanExecute()) return false;
                Die(_health);
                onCommandExecuted?.Invoke(this);
                return true;
            }

            private void Die(Health health)
            {
                Destroy(health.gameObject);
                health.CurState = HealthState.Dead;
                health.onDied?.Invoke();
            }

            public bool CanExecute()
            {
                return _health != null && _health.CurState != HealthState.Dead && GameStateHandler.CurGameState == GameStateHandler.GameState.Run;
            }

            public DieCommand(Health health)
            {
                _health = health;
            }
        }
    }
}
