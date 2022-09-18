using ProjectCore.BattleLogic.BattleUi.VisibilityServices;
using Zenject;

namespace ProjectCore.BattleLogic.BattleUi
{
    public class BattleUiEventsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BattleConfigureVisibilityService>().AsSingle();
            Container.BindInterfacesTo<BattleCancelVisibilityService>().AsSingle();
        }
    }
}
