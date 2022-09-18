using System.Collections.Generic;
using Project.BattleLogic.BattleContextStates.States;
using Project.BattleLogic.EnvironmentProviders;
using Project.BattleLogic.GridSizeInstallers;
using Project.BattleLogic.Spawners;
using Project.Configs.CharacterViewsProviding;
using ProjectShared;
using ProjectShared.Battler;
using UnityEngine;

namespace Project.BattleLogic.TeamMakeLogic
{
    public class MatchMakingProcessor
    {
        private readonly ISpawnerAnchorsProvider _anchorsProvider;
        private readonly ISpawnPointsCalculator _spawnPointsCalculator;
        private readonly IGridSizeProvider _gridSizeProvider;

        public MatchMakingProcessor(
            ISpawnerAnchorsProvider anchorsProvider, 
            ISpawnPointsCalculator spawnPointsCalculator,
            IGridSizeProvider gridSizeProvider
            )
        {
            _anchorsProvider = anchorsProvider;
            _spawnPointsCalculator = spawnPointsCalculator;
            _gridSizeProvider = gridSizeProvider;
        }

        private ViewType CaclulateViewType()
        {
            return (UnityEngine.Random.value > 0.5f) ? ViewType.Rectangle : ViewType.Circle;
        }

        private CharacterColorType CalculateColorType()
        {
            return (UnityEngine.Random.value > 0.5f) ? CharacterColorType.Blue : CharacterColorType.Orange;
        }

        public Dictionary<int, Color> CalculateTeamsColor()
        {
            var result = new Dictionary<int, Color>();
            foreach (var anchor in _anchorsProvider.SpawnerAnchors)
            {
                result.Add(anchor.TeamId, anchor.Color);
            }

            return result;
        }

        public List<CharacterSpawnInfoDTO> Calculate()
        {
            var result = new List<CharacterSpawnInfoDTO>();
            foreach (var anchor in _anchorsProvider.SpawnerAnchors)
            {
                var points = _spawnPointsCalculator.SpawnPoints(anchor.Position, _gridSizeProvider.Size.Value);

                foreach (var point in points)
                {
                    var spawnInfo = new CharacterSpawnInfoDTO(
                        anchor.TeamId, 
                        point, 
                        CalculateColorType(),
                        CaclulateViewType(),
                        new Dictionary<StatType, float>()
                        {
                            { StatType.Speed , UnityEngine.Random.Range(2f, 11f)},
                            { StatType.AttackPoints , UnityEngine.Random.Range(2f, 50f)},
                            { StatType.AttackSpeed , UnityEngine.Random.Range(0.5f, 3f)},
                            { StatType.HealthPoints , UnityEngine.Random.Range(100f, 500f)},
                            { StatType.Size , UnityEngine.Random.Range(1f, 3f)},
                        }
                        );

                    result.Add(spawnInfo);
                }
            }

            return result;
        }
    }
}