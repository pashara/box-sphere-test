using System;
using System.Collections.Generic;
using ThirdParty.StateMachine.States;

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
    }
}