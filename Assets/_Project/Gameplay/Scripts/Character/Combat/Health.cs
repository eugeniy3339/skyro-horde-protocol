using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
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
            TryToExecuteCommand(new HealthCommand(this, () =>
            {
                curHealth -= damage;
                onDamaged?.Invoke(damage);
                if (curHealth <= 0f)
                    Die();
            }));
        }

        public void Heal(float health)
        {
            TryToExecuteCommand(new HealthCommand(this, () =>
            {
                curHealth += health;
                onHealed?.Invoke(health);
            }));
        }

        public void Die()
        {
            TryToExecuteCommand(new HealthCommand(this, () =>
            {
                Destroy(gameObject);
                CurState = HealthState.Dead;
                onDied?.Invoke();
            }));
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



        public class HealthCommand : ICommand
        {
            private Health _health;
            private Action _commandExecuteAction;

            public virtual bool Execute()
            {
                if (!CanExecute()) return false;
                _commandExecuteAction?.Invoke();
                return true;
            }

            public virtual bool CanExecute()
            {
                return _health.CurState == HealthState.Alive && GameStateHandler.CurState == GameStateHandler.GameState.Run;
            }

            public HealthCommand(Health health, Action commandExecuteAction)
            {
                _health = health;
                _commandExecuteAction = commandExecuteAction;
            }
        }
    }
}
