using System.Collections.Generic;
using System.Numerics;
using ThirdParty.StateMachine.States;

namespace ProjectCore.BattleLogic.BattleContextStates.States
{
    public class SpawnState : IBattleContextState, IPayloadableState<SpawnState.SpawnStatePayload>
    {
        private SpawnStatePayload _payload;
            
        public SpawnState()
        {
            
        }

        public void Configure(SpawnStatePayload payload)
        {
            _payload = payload;
        }
        
        public void Enter()
        {
        }

        public void Exit()
        {
            _payload = null;
        }
        
        public class SpawnStatePayload : IStatePayload
        {
            public Dictionary<int, List<Vector3>> PointsByTeam { get; }
        }
    }
}