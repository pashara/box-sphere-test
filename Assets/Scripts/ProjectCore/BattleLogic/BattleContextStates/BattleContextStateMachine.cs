using System;
using System.Collections.Generic;
using ProjectCore.BattleLogic.BattleContextStates.States;
using ThirdParty.StateMachine;
using ThirdParty.StateMachine.States;
using Zenject;

namespace ProjectCore.BattleLogic.BattleContextStates
{
    public interface IBattleContextStateMachine : IStateMachine<IBattleContextState>
    {
    }
    
    public class BattleContextStateMachine : StateMachine<IBattleContextState>, IBattleContextStateMachine, IInitializable, IDisposable
    {
        private readonly DiContainer _container;
        
        public BattleContextStateMachine(DiContainer container)
        {
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

    }
}
