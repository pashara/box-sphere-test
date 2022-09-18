using System.Linq;
using ProjectCore.BattleLogic.EnvironmentProviders;
using UnityEngine;

namespace ProjectCore.BattleLogic.Spawners
{
    public class SpawnersBaker : ComponentsBaker.ComponentsBaker
    {
        [SerializeField] private SpawnerAnchorsProvider anchorsProvider;
        protected override void FindComponents()
        {
            anchorsProvider.Anchors.Clear();
            anchorsProvider.Anchors.AddRange(FindObjectsOfType<SpawnerAnchor>().ToList());
        }
    }
}