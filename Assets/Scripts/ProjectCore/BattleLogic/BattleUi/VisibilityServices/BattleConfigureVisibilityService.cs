using System;
using ProjectCore.Common;
using ProjectCore.Common.Visibility;
using ThirdParty.EventBus;
using UniRx;

namespace ProjectCore.BattleLogic.BattleUi.VisibilityServices
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
                .Subscribe(x => _visibleStateHandler.Value = true).AddTo(_disposable);

            eventBus.WasTriggered(EventKeys.DismissToShowPrepareBattleContext)
                .Subscribe(x => _visibleStateHandler.Value = false).AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}