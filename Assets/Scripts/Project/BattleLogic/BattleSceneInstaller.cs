using Project.BattleLogic.BattleContextStates;
using Project.BattleLogic.EnvironmentProviders;
using Project.BattleLogic.Spawners;
using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project.BattleLogic
{
    public class BattleSceneInstaller : MonoInstaller
    {
        [SerializeField] private SpawnerAnchorsProvider spawnerAnchorsProvider;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SpawnerAnchorsProvider>().FromInstance(spawnerAnchorsProvider);
            Container.BindInterfacesTo<BattleContextStateMachine>().AsSingle();
            Container.BindInterfacesTo<SpawnPointsCalculator>().AsSingle();

            BindFactories();
        }

        private void BindFactories()
        {
            Container.BindInterfacesTo<CharacterFactory>().AsSingle();
            Container.BindInterfacesTo<CharacterViewFactory>().AsSingle();
        }
    }
}
