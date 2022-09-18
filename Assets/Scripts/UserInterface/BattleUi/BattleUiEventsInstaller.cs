using Project.BattleLogic.GridSizeInstallers;
using Project.BattleLogic.VisibilityServices;
using Zenject;

namespace UserInterface.BattleUi
{
    public class BattleUiEventsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BattleConfigureVisibilityService>().AsSingle();
            Container.BindInterfacesTo<BattleCancelVisibilityService>().AsSingle();
            
            Container.BindInterfacesTo<GridSizeInputHandler>().AsSingle();
        }
    }
}
