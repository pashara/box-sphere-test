using ProjectCore.Sessions;
using Zenject;

namespace ProjectCore.BootstrapLogic
{
    public class BootstrapSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneProvider>().AsSingle().WithArguments(gameObject.scene);
            Container.BindInterfacesTo<GameToBattlerSwitchService>().AsSingle();
        }
    }
}
