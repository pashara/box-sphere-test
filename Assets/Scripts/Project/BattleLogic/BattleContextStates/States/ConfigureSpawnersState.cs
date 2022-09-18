using ProjectCore.BattleLogic.BattleUi.VisibilityServices;
using ProjectCore.BattleLogic.GridSizeInstallers;
using ThirdParty.EventBus;
using UniRx;

namespace ProjectCore.BattleLogic.BattleContextStates.States
{
    public class ConfigureSpawnersState : IBattleContextState
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly IBattleContextStateMachine _battleContextStateMachine;
        private readonly IBattleConfigureVisibilityService _battleConfigureVisibilityService;
        private readonly IGridSizeInputProvider _gridSizeInputProvider;
        private readonly IEventBus _eventBus;

        public ConfigureSpawnersState(
            IBattleContextStateMachine battleContextStateMachine,
            IBattleConfigureVisibilityService battleConfigureVisibilityService,
            IGridSizeInputProvider gridSizeInputProvider,
            IEventBus eventBus
            )
        {
            _battleContextStateMachine = battleContextStateMachine;
            _battleConfigureVisibilityService = battleConfigureVisibilityService;
            _gridSizeInputProvider = gridSizeInputProvider;
            _eventBus = eventBus;
        }
        
        public void Enter()
        {
            _battleConfigureVisibilityService.AffectVisibility(true);
            _eventBus.WasTriggered(EventKeys.BattleStartClick).Subscribe(x =>
            {
                if (Validate())
                {
                    _battleContextStateMachine.Enter<SpawnState, SpawnState.SpawnStatePayload>(new ()
                    {
                        size = _gridSizeInputProvider.Size.Value
                    });
                }
            }).AddTo(_disposable);
        }

        public void Exit()
        {
            _battleConfigureVisibilityService.AffectVisibility(false);
        }

        private bool Validate()
        {
            return _gridSizeInputProvider.Size.Value.x > 0 &&
                   _gridSizeInputProvider.Size.Value.y > 0 &&
                   _gridSizeInputProvider.Size.Value.x < 8 &&
                   _gridSizeInputProvider.Size.Value.y < 8;
        }
    }
}