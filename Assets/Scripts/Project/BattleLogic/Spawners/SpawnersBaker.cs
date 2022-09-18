using System.Linq;
using Project.BattleLogic.EnvironmentProviders;
using ThirdParty.ComponentsBaker;
using UnityEngine;

namespace Project.BattleLogic.Spawners
{
    public class SpawnersBaker : ComponentsBaker
    {
        [SerializeField] private SpawnerAnchorsProvider anchorsProvider;
        protected override void FindComponents()
        {
            anchorsProvider.Anchors.Clear();
            anchorsProvider.Anchors.AddRange(FindObjectsOfType<SpawnerAnchor>().ToList());
        }
    }
}