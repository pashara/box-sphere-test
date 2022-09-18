using System;
using ProjectCore.Common;
using ProjectCore.Common.Visibility;
using ThirdParty.EventBus;
using UniRx;

namespace ProjectCore.BattleLogic.BattleUi.VisibilityServices
{
    public interface IBattleCancelVisibilityService : IVisibilityService
    {
    }
    
    public class BattleCancelVisibilityService : BaseVisibilityService, IBattleCancelVisibilityService, IDisposable
    {
        private CompositeDisposable _disposable = new();

        public BattleCancelVisibilityService(IEventBus eventBus) : base()
        {
            eventBus.WasTriggered(EventKeys.ReadyForShowCancelContext)
                .Subscribe(x => _visibleStateHandler.Value = true).AddTo(_disposable);

            eventBus.WasTriggered(EventKeys.DismissToShowCancelContext)
                .Subscribe(x => _visibleStateHandler.Value = false).AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}