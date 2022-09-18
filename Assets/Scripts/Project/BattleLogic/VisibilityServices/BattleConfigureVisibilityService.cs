using System;
using Project.Common;
using ProjectShared;
using ThirdParty.EventBus;
using UniRx;

namespace Project.BattleLogic.VisibilityServices
{
    public interface IBattleConfigureVisibilityService : IVisibilityService
    {
    }
    
    public class BattleConfigureVisibilityService : BaseVisibilityService, IBattleConfigureVisibilityService, IDisposable
    {
        private CompositeDisposable _disposable = new();

        public BattleConfigureVisibilityService(IEventBus eventBus) : base()
        {
            eventBus.WasTriggered(EventKeys.ReadyForShowPrepareBattleContext)
                .Subscribe(x => VisibleStateHandler.Value = true).AddTo(_disposable);

            eventBus.WasTriggered(EventKeys.DismissToShowPrepareBattleContext)
                .Subscribe(x => VisibleStateHandler.Value = false).AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}