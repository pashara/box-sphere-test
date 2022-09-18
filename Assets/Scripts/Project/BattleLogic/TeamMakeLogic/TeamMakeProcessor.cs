using System.Collections.Generic;
using Project.BattleLogic.BattleContextStates.States;
using Project.BattleLogic.EnvironmentProviders;
using Project.BattleLogic.GridSizeInstallers;
using Project.BattleLogic.Spawners;
using Project.Configs.CharacterViewsProviding;
using UnityEngine;

namespace Project.BattleLogic.TeamMakeLogic
{
    public class TeamMakeProcessor
    {
        private readonly ISpawnerAnchorsProvider _anchorsProvider;
        private readonly ISpawnPointsCalculator _spawnPointsCalculator;
        private readonly IGridSizeProvider _gridSizeProvider;

        public TeamMakeProcessor(
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

        private ColorType CalculateColorType()
        {
            return (UnityEngine.Random.value > 0.5f) ? ColorType.Blue : ColorType.Orange;
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
                        CaclulateViewType());

                    result.Add(spawnInfo);
                }
            }

            return result;
        }
    }
}