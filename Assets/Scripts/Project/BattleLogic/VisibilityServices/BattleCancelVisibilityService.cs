using System;
using Project.Common;
using ProjectShared;
using ThirdParty.EventBus;
using UniRx;

namespace Project.BattleLogic.VisibilityServices
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
                .Subscribe(x => VisibleStateHandler.Value = true).AddTo(_disposable);

            eventBus.WasTriggered(EventKeys.DismissToShowCancelContext)
                .Subscribe(x => VisibleStateHandler.Value = false).AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}