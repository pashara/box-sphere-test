using Project.BattleLogic.BattleContextStates;
using Project.BattleLogic.EnvironmentProviders;
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
        }
    }
}
