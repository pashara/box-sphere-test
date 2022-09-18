using UniRx;

namespace Project.Common
{
    public interface IVisibilityService
    {
        ReadOnlyReactiveProperty<bool> OnVisibleState { get; }
        void AffectVisibility(bool isVisible);
    }
    
    public abstract class BaseVisibilityService
    {
        protected readonly ReactiveProperty<bool> VisibleStateHandler = new();
        public ReadOnlyReactiveProperty<bool> OnVisibleState { get; }

        protected BaseVisibilityService()
        {
            OnVisibleState = new (VisibleStateHandler);
        }

        public void AffectVisibility(bool isVisible)
        {
            VisibleStateHandler.Value = isVisible;
        }
    }
}