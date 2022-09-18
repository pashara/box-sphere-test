using System;
using System.Collections.Generic;
using ThirdParty.StateMachine.States;
using UnityEngine;

namespace ThirdParty.StateMachine
{
    public class StateMachineMonoBehaviour<T> : IStateMachine<T> where T : IState
    {
        private readonly Dictionary<Type, T> _states = new();
        protected T ActualState => _actualState;
        private T _actualState = default;
        
        public void Enter<T>()
        {
            if (_actualState != null)
            {
                _actualState.Exit();
            }
            
            if (!_states.TryGetValue(typeof(T), out var stateHandler) || stateHandler == null)
            {
                Debug.LogError($"No state {typeof(T)}");
                return;
            }

            OnPreEnter();
            ChangeState(stateHandler);
            stateHandler.Enter();
            OnPostEnter();
        }

        protected void Put(T instance)
        {
            _states.Add(typeof(T), instance);
        }

        protected void Clear()
        {
            if (ActualState != null)
            {
                ActualState.Exit();
            }
            _states.Clear();
        }
        
        protected void ChangeState(T state)
        {
            _actualState = state;
        }

        protected virtual void OnPreEnter()
        {
        }

        protected virtual void OnPostEnter()
        {
        }
    }
}