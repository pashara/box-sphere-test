using System;
using System.Collections.Generic;
using Project.BattleLogic.BattleContextStates.States;
using ThirdParty.StateMachine;
using ThirdParty.StateMachine.States;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.BattleLogic.BattleContextStates
{
    public interface IBattleContextStateMachine : IStateMachine<IBattleContextState>
    {
        ReadOnlyReactiveProperty<IBattleContextState> CurrentState { get; }
    }

    public class BattleContextStateMachine : StateMachine<IBattleContextState>, 
        IBattleContextStateMachine, IInitializable, IDisposable
    {
        private IStatePayload enterStatePayload = null;
        private readonly ReactiveProperty<IBattleContextState> _currentState = new();
        public ReadOnlyReactiveProperty<IBattleContextState> CurrentState { get; }

        private readonly DiContainer _container;

        public BattleContextStateMachine(DiContainer container)
        {
            CurrentState = new ReadOnlyReactiveProperty<IBattleContextState>(_currentState);
            _container = container;

        }

        public void Initialize()
        {
            PutState<LinkDependenciesState>();
            PutState<ConfigureSpawnersState>();
            PutState<BattleState>();
            PutState<RespawnState>();
            PutState<SpawnState>();
            PutState<DespawnState>();
            
            
            
            Enter<SpawnState, SpawnState.SpawnStatePayload>(new ()
            {
                size = new Vector2Int(3, 3)
            });
            // Enter<ConfigureSpawnersState>();
        }

        public void Dispose()
        {
            Clear();
        }
        
        private void PutState<T>(List<object> extraArgs = null) where T : IBattleContextState
        {
            if (extraArgs == null || extraArgs.Count == 0)
            {
                Put<T>(_container.Instantiate<T>());
            }
            else
            {
                Put<T>(_container.Instantiate<T>(extraArgs));
            }
        }

        protected override void ChangeState(IBattleContextState state)
        {
            base.ChangeState(state);
            _currentState.Value = state;
        }
    }
}