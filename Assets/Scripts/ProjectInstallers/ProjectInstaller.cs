using Project.CustomSceneManagement;
using Project.Sessions;
using ThirdParty.EventBus;
using Zenject;

namespace ProjectInstallers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SessionInfo>().AsSingle();
            Container.BindInterfacesTo<EventBus>().AsSingle();
            Container.BindInterfacesTo<CustomSceneManagement>().AsSingle();
        }
    }
}