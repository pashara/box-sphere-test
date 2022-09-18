using ProjectCore.BattleLogic.BattleContextStates;
using ProjectCore.BattleLogic.EnvironmentProviders;
using UnityEngine;
using Zenject;

namespace ProjectCore.BattleLogic
{
    public class BattleSceneInstaller : MonoInstaller
    {
        [SerializeField] private SpawnerAnchorsProvider spawnerAnchorsProvider;


        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SpawnerAnchorsProvider>().FromInstance(spawnerAnchorsProvider);
            // Container.BindInterfacesTo<BattleContextStateMachine>().FromInstance(spawnerAnchorsProvider);
        }
    }
}
