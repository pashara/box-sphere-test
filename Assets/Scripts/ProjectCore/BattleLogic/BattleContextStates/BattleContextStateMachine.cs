using System;
using System.Collections.Generic;
using ProjectCore.BattleLogic.BattleContextStates.States;
using ThirdParty.StateMachine;
using ThirdParty.StateMachine.States;
using UniRx;
using Zenject;

namespace ProjectCore.BattleLogic.BattleContextStates
{
    public interface IBattleContextStateMachine : IStateMachine<IBattleContextState>
    {
        ReadOnlyReactiveProperty<IBattleContextState> CurrentState { get; }
    }
    
    public class BattleContextStateMachine : StateMachine<IBattleContextState>, IBattleContextStateMachine, IInitializable, IDisposable
    {
        private readonly ReactiveProperty<IBattleContextState> _currentState = new();
        public ReadOnlyReactiveProperty<IBattleContextState> CurrentState { get; }

        private readonly DiContainer _container;
        
        public BattleContextStateMachine(DiContainer container)
        {
            CurrentState = new ReadOnlyReactiveProperty<IBattleContextState>(_currentState);
            _container = container;
            PutState<LinkDependenciesState>();
            PutState<ConfigureSpawnersState>();
            PutState<BattleState>();
            PutState<RespawnState>();
            
        }
        
        public void Initialize()
        {
            Enter<ConfigureSpawnersState>();
        }

        public void Dispose()
        {
            Clear();
        }
        
        private void PutState<T>(List<object> extraArgs = null) where T : IBattleContextState
        {
            Put(_container.Instantiate<T>(extraArgs));
        }

        protected override void ChangeState(IBattleContextState state)
        {
            base.ChangeState(state);
            _currentState.Value = state;
        }
    }
}
