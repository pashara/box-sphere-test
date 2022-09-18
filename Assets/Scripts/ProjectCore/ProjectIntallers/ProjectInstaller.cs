using ProjectCore.Sessions;
using Zenject;

namespace ProjectCore.ProjectIntallers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SessionInfo>().AsSingle();
            Container.BindInterfacesTo<CustomSceneManagement.CustomSceneManagement>().AsSingle();
        }
    }
}
