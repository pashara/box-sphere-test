using System;
using System.Collections.Generic;
using ThirdParty.StateMachine.States;
using UnityEngine;

namespace ThirdParty.StateMachine
{
    public class StateMachineMonoBehaviour<T> : MonoBehaviour, IStateMachine<T> where T : IState
    {
        private readonly Dictionary<Type, T> _states = new();
        private T _actualState = default;
        
        public void Enter<TState>() where TState : T
        {
            ExitActualState();

            if (!TryGetHandler<TState>(out var stateHandler))
                return;
            
            ProcessEnterState(stateHandler);
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : T, IPayloadableState<TPayload> where TPayload : IStatePayload
        {
            ExitActualState();
            
            if (!TryGetHandler<TState>(out var stateHandler))
                return;

            var handler = stateHandler as IPayloadableState<TPayload>;
            handler.Configure(payload);
            
            ProcessEnterState(stateHandler);
        }
        
        protected void Put<TType>(TType instance) where TType : T
        {
            _states.Add(typeof(TType), instance);
        }

        protected void Clear()
        {
            if (_actualState != null)
            {
                _actualState.Exit();
            }
            _states.Clear();
        }

        protected virtual void ChangeState(T state)
        {
            _actualState = state;
        }

        protected virtual void OnPreEnter(T state)
        {
        }

        protected virtual void OnPostEnter(T state)
        {
        }

        protected virtual void OnPreExit(T state)
        {
        }

        protected virtual void OnPostExit(T state)
        {
        }

        private bool TryGetHandler<TType>(out T handler) where TType : T
        {
            handler = default;
            if (!_states.TryGetValue(typeof(TType), out var stateHandler) || stateHandler == null)
            {
                Debug.LogError($"No state {typeof(T)}");
                return false;
            }

            handler = stateHandler;
            return true;
        }

        private void ExitActualState()
        {
            if (_actualState == null) return;
            
            var state = _actualState;
            OnPreExit(state);
            state.Exit();
            OnPostExit(state);
        }

        private void ProcessEnterState(T stateHandler)
        {
            OnPreEnter(stateHandler);
            ChangeState(stateHandler);
            stateHandler.Enter();
            OnPostEnter(stateHandler);
        }
    }
}