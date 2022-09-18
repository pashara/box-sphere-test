using UniRx;

namespace ProjectCore.Common.Visibility
{
    public interface IVisibilityService
    {
        ReadOnlyReactiveProperty<bool> OnVisibleState { get; }
    }
    
    public abstract class BaseVisibilityService
    {
        protected readonly ReactiveProperty<bool> _visibleStateHandler = new ReactiveProperty<bool>();
        public ReadOnlyReactiveProperty<bool> OnVisibleState { get; }

        public BaseVisibilityService()
        {
            OnVisibleState = new (_visibleStateHandler);
        }
    }
}