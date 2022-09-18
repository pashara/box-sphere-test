using System.Collections.Generic;
using UnityEngine;

namespace Project.BattleLogic.Spawners
{
    public interface ISpawnPointsCalculator
    {
        List<Vector3> SpawnPoints(Vector3 center, Vector2Int gridSize);
    }
    
    public class SpawnPointsCalculator : ISpawnPointsCalculator
    {
        private readonly Vector2 _distanceBetween = new(4f, 4f);
        
        public List<Vector3> SpawnPoints(Vector3 center, Vector2Int gridSize)
        {
            var points = new List<Vector3>();
            var lbPoint = 0.5f * Vector2.Scale(_distanceBetween, new Vector2(gridSize.x, gridSize.y));
            
            var offset = center - new Vector3(lbPoint.x, 0f, lbPoint.y);
            for (var x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    points.Add(offset + new Vector3(x * _distanceBetween.x, 0f, y * _distanceBetween.y));
                }
            }

            return points;
        }
    }
}