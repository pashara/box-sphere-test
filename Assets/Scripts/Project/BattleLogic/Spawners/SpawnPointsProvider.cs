using System.Collections.Generic;
using UnityEngine;

namespace Project.BattleLogic.Spawners
{
    public interface ISpawnPointsProvider
    {
        List<Vector3> SpawnPoints();
        void Configure(Vector2Int gridSize);
    }
    
    public class SpawnPointsProvider : MonoBehaviour, ISpawnPointsProvider
    {
        private Vector2Int _size;
        private readonly Vector2 _distanceBetween = new Vector2(0.5f, 0.5f);
        
        public void Configure(Vector2Int gridSize)
        {
            _size = gridSize;
        }
        
        public List<Vector3> SpawnPoints()
        {
            var points = new List<Vector3>();
            var lbPoint = Vector2.Scale(_distanceBetween, new Vector2(_size.x * 0.5f, _size.y * 0.5f));
            var lbWorldPoint = new Vector3(lbPoint.x, 0f, lbPoint.y);
            for (var x = 0; x < _size.x; x++)
            {
                for (var y = 0; y < _size.y; y++)
                {
                    points.Add(lbWorldPoint + new Vector3(x * _distanceBetween.x, 0f, y * _distanceBetween.y));
                }
            }

            return points;
        }
    }
}