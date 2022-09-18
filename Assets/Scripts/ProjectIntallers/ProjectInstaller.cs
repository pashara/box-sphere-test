using ProjectCore.Sessions;
using ThirdParty.EventBus;
using Zenject;

namespace ProjectCore.ProjectIntallers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SessionInfo>().AsSingle();
            Container.BindInterfacesTo<EventBus>().AsSingle();
            Container.BindInterfacesTo<CustomSceneManagement.CustomSceneManagement>().AsSingle();
        }
    }
}
