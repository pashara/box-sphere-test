using Project.BattleLogic.GridSizeInstallers;
using Project.BattleLogic.TeamMakeLogic;
using Project.BattleLogic.VisibilityServices;
using ProjectShared;
using ThirdParty.EventBus;
using UniRx;
using Zenject;

namespace Project.BattleLogic.BattleContextStates.States
{
    public class ConfigureSpawnersState : IBattleContextState
    {
        private readonly CompositeDisposable _disposable = new();
        private readonly IBattleContextStateMachine _battleContextStateMachine;
        private readonly IBattleConfigureVisibilityService _battleConfigureVisibilityService;
        private readonly IGridSizeProvider _gridSizeProvider;
        private readonly IEventBus _eventBus;
        private readonly DiContainer _container;

        public ConfigureSpawnersState(
            IBattleContextStateMachine battleContextStateMachine,
            IBattleConfigureVisibilityService battleConfigureVisibilityService,
            IGridSizeProvider gridSizeProvider,
            IEventBus eventBus,
            DiContainer container
            )
        {
            _battleContextStateMachine = battleContextStateMachine;
            _battleConfigureVisibilityService = battleConfigureVisibilityService;
            _gridSizeProvider = gridSizeProvider;
            _eventBus = eventBus;
            _container = container;
        }
        
        public void Enter()
        {
            _battleConfigureVisibilityService.AffectVisibility(true);
            _eventBus.WasTriggered(EventKeys.BattleStartClick).Subscribe(x =>
            {
                if (Validate())
                {
                    var data = CreatePayload();
                    _battleContextStateMachine.Enter<SpawnState, SpawnState.SpawnStatePayload>(data);
                }
            }).AddTo(_disposable);
        }

        public void Exit()
        {
            _battleConfigureVisibilityService.AffectVisibility(false);
        }

        private bool Validate()
        {
            return _gridSizeProvider.Size.Value.x > 0 &&
                   _gridSizeProvider.Size.Value.y > 0 &&
                   _gridSizeProvider.Size.Value.x < 8 &&
                   _gridSizeProvider.Size.Value.y < 8;
        }

        private SpawnState.SpawnStatePayload CreatePayload()
        {
            var teamProcessor = _container.Instantiate<TeamMakeProcessor>();
            var spawnData = teamProcessor.Calculate();
            var teamsData = teamProcessor.CalculateTeamsColor();
            var payload = new SpawnState.SpawnStatePayload(teamsData, spawnData);
            return payload;
        }
    }
}