using ECS.Systems;
using Project.CustomSceneManagement;
using Project.Sessions;
using Project.Ticking;
using ThirdParty.EventBus;
using Zenject;

namespace ProjectInstallers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EventBus>().AsSingle();
            Container.BindInterfacesTo<CustomSceneManagement>().AsSingle();
            
            Container.BindInterfacesTo<SessionInfo>().AsSingle();
            
            Container.BindInterfacesTo<GameplayTicker>().AsSingle();
            Container.BindInterfacesTo<MainECSContextInstaller>().AsSingle();
        }
    }
}
