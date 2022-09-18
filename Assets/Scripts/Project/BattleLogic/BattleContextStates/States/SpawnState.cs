using ThirdParty.StateMachine.States;
using UnityEngine;

namespace Project.BattleLogic.BattleContextStates.States
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
            Debug.Log(_payload.size);
        }

        public void Exit()
        {
            _payload = null;
        }
        
        public class SpawnStatePayload : IStatePayload
        {
            // public Dictionary<int, List<Vector3>> PointsByTeam { get; }

            public Vector2Int size { get; set; }
        }
        public class SpawnStatePayload11 : IStatePayload
        {
            // public Dictionary<int, List<Vector3>> PointsByTeam { get; }

            public Vector2Int size { get; set; }
        }
    }
}