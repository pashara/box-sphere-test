using UniRx;

namespace ProjectCore.Common.Visibility
{
    public interface IVisibilityService
    {
        ReadOnlyReactiveProperty<bool> OnVisibleState { get; }
        void AffectVisibility(bool isVisible);
    }
    
    public abstract class BaseVisibilityService
    {
        protected readonly ReactiveProperty<bool> _visibleStateHandler = new ReactiveProperty<bool>();
        public ReadOnlyReactiveProperty<bool> OnVisibleState { get; }

        public BaseVisibilityService()
        {
            OnVisibleState = new (_visibleStateHandler);
        }

        public void AffectVisibility(bool isVisible)
        {
            _visibleStateHandler.Value = isVisible;
        }
    }
}