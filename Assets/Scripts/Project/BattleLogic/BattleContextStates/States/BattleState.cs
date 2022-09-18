using Project.BattleLogic.VisibilityServices;

namespace Project.BattleLogic.BattleContextStates.States
{
    public class BattleState : IBattleContextState
    {
        private readonly IBattleCancelVisibilityService _battleConfigureVisibilityService;

        public BattleState(
            IBattleCancelVisibilityService battleConfigureVisibilityService)
        {
            _battleConfigureVisibilityService = battleConfigureVisibilityService;
        }
        
        public void Enter()
        {
            _battleConfigureVisibilityService.AffectVisibility(true);
        }

        public void Exit()
        {
            _battleConfigureVisibilityService.AffectVisibility(false);
        }
    }
}