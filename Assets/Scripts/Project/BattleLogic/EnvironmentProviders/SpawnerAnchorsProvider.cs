using System.Collections.Generic;
using Project.BattleLogic.Spawners;
using UnityEngine;

namespace Project.BattleLogic.EnvironmentProviders
{
    public interface ISpawnerAnchorsProvider
    {
        IReadOnlyList<ISpawnerAnchor> SpawnerAnchors { get; }
    }
    
    public class SpawnerAnchorsProvider : MonoBehaviour, ISpawnerAnchorsProvider
    {
        [SerializeField] private List<SpawnerAnchor> spawnerAnchors;
        
        public List<SpawnerAnchor> Anchors => spawnerAnchors;
        public IReadOnlyList<ISpawnerAnchor> SpawnerAnchors => spawnerAnchors;
    }
}