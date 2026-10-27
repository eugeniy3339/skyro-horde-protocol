using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using static Codice.Client.Commands.WkTree.WorkspaceTreeNode;
using static HordeProtocol.Gameplay.Health;

namespace HordeProtocol.Gameplay
{
    [DisallowMultipleComponent]
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

        private HealthState curState = HealthState.Alive;
        public HealthState CurState
        {
            get { return curState; }
        }

        public event Action<float> onHealthChanged;
        public event Action<float> onDamaged;
        public event Action<float> onHealed;
        public event Action onDied;



        public void Damage(float damage)
        {
            TryToExecuteCommand(new HealthCommand<float>(this, damage, DamageAction));
        }

        protected virtual void DamageAction(float damage)
        {
            curHealth -= damage;
            onDamaged?.Invoke(damage);
            if (curHealth <= 0f)
                Die();
        }

        public void Heal(float health)
        {
            TryToExecuteCommand(new HealthCommand<float>(this, health, HealAction));
        }

        protected virtual void HealAction(float health)
        {
            curHealth += health;
            onHealed?.Invoke(health);
        }

        public void Die()
        {
            TryToExecuteCommand(new HealthCommand(this, DieAction));
        }

        protected virtual void DieAction()
        {
            gameObject.SetActive(false);
            curState = HealthState.Dead;
            onDied?.Invoke();
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
            curState = HealthState.Alive;
            curHealth = maxHealth;

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
            protected Health _health;
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

        public class HealthCommand<T> : HealthCommand
        {
            private T _value;
            private Action<T> _commandExecuteAction;

            public override bool Execute()
            {
                if (!CanExecute()) return false;
                _commandExecuteAction?.Invoke(_value);
                return true;
            }

            public HealthCommand(Health health, T value, Action<T> commandExecuteAction) : base(health, null)
            {
                _value = value;
                _commandExecuteAction = commandExecuteAction;
            }
        }
    }
}
