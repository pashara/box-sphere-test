using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Configs.CharacterViewsProviding;
using Project.Factories;
using ProjectShared.Battler.Components;
using ThirdParty.StateMachine.States;
using UnityEngine;

namespace Project.BattleLogic.BattleContextStates.States
{
    public class SpawnState : IBattleContextState, IPayloadableState<SpawnState.SpawnStatePayload>
    {
        private readonly IBattleContextStateMachine _battleContextStateMachine;
        private readonly ICharacterFactory _characterFactory;
        private readonly ICharacterViewFactory _characterViewFactory;
        private readonly ICharacterColorsProvider _colorsProvider;
        private SpawnStatePayload _payload;
            
        public SpawnState(
            IBattleContextStateMachine battleContextStateMachine,
            ICharacterFactory characterFactory, 
            ICharacterViewFactory characterViewFactory,
            ICharacterColorsProvider colorsProvider)
        {
            _battleContextStateMachine = battleContextStateMachine;
            _characterFactory = characterFactory;
            _characterViewFactory = characterViewFactory;
            _colorsProvider = colorsProvider;
        }

        public void Configure(SpawnStatePayload payload)
        {
            _payload = payload;
        }
        
        public async void Enter()
        {
            foreach (var spawnInfo in _payload.SpawnInfo)
            {
                var teamColor = _payload.TeamColors[spawnInfo.TeamId];
                
                var character = _characterFactory.Create();
                var view = await _characterViewFactory.Create(ViewType.Circle);
                character.Initialize(view);

                character.PositionHandler.ApplyPosition(spawnInfo.Position);

                if (character.CharacterComponentsContainer.TryGet<IColorComponent>(out var colorComponent))
                {
                    colorComponent.Color.Value = _colorsProvider.Get(spawnInfo.ColorType);
                }
                if (character.CharacterComponentsContainer.TryGet<ITeamIndicatorComponent>(out var indicatorComponent))
                {
                    indicatorComponent.Color.Value = teamColor;
                }
            }

            await UniTask.Yield();
            _battleContextStateMachine.Enter<BattleState>();
        }

        public void Exit()
        {
            _payload = null;
        }
        
        
        public class SpawnStatePayload : IStatePayload
        {
            public Dictionary<int, Color> TeamColors { get; }
            public List<CharacterSpawnInfoDTO> SpawnInfo { get; }


            public SpawnStatePayload(Dictionary<int, Color> teamColors, List<CharacterSpawnInfoDTO> spawnInfo)
            {
                TeamColors = teamColors;
                SpawnInfo = spawnInfo;
            }

        }
    }
    
    public class CharacterSpawnInfoDTO
    {
        private ViewType ViewType { get; }
        public int TeamId { get; }
        public Vector3 Position { get; }
        public ColorType ColorType { get; }
        
        public CharacterSpawnInfoDTO(int teamId, Vector3 position, ColorType colorType, ViewType viewType)
        {
            TeamId = teamId;
            Position = position;
            ColorType = colorType;
            ViewType = viewType;
        }
    }
}