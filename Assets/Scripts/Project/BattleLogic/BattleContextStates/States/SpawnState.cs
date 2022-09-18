using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ECS.Components;
using ECS.Systems;
using Project.Configs.CharacterViewsProviding;
using Project.Factories;
using ProjectShared.Battler;
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
        private readonly IECSContext _ecsContext;
        private readonly ICharacterColorsProvider _colorsProvider;
        private SpawnStatePayload _payload;
            
        public SpawnState(
            IBattleContextStateMachine battleContextStateMachine,
            ICharacterFactory characterFactory, 
            ICharacterViewFactory characterViewFactory,
            IECSContext ecsContext,
            ICharacterColorsProvider colorsProvider)
        {
            _battleContextStateMachine = battleContextStateMachine;
            _characterFactory = characterFactory;
            _characterViewFactory = characterViewFactory;
            _ecsContext = ecsContext;
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

                var battlerEntity = _ecsContext.Contexts.battler.CreateEntity();
                var statsEntity = _ecsContext.Contexts.stats.CreateEntity();
                battlerEntity.AddStats(statsEntity);
                battlerEntity.AddCharacterReference(character);
                character.PositionHandler.ApplyPosition(spawnInfo.Position);
                FillStats(spawnInfo, statsEntity);

                if (character.CharacterComponentsContainer.TryGet<IColorComponent>(out var colorComponent))
                {
                    colorComponent.Color.Value = _colorsProvider.Get(spawnInfo.CharacterColorType);
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

        void FillStats(CharacterSpawnInfoDTO dto, StatsEntity statsEntity)
        {
            foreach (var statInfo in dto.StatValues)
            {
                statsEntity.ReplaceStatValue(statInfo.Key, statInfo.Value);
            }
           
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
    
    [Serializable]
    public class CharacterSpawnInfoDTO
    {
        private ViewType ViewType { get; }
        public int TeamId { get; }
        public Vector3 Position { get; }
        public CharacterColorType CharacterColorType { get; }
        
        public float Size { get; }
        public Dictionary<StatType, float> StatValues { get; }

        public CharacterSpawnInfoDTO(
            int teamId, 
            Vector3 position, 
            CharacterColorType characterColorType, 
            ViewType viewType, 
            float size, 
            Dictionary<StatType, float> statValues
            )
        {
            TeamId = teamId;
            Position = position;
            CharacterColorType = characterColorType;
            ViewType = viewType;
            Size = size;
            StatValues = statValues;
        }
    }
}