using Cysharp.Threading.Tasks;

namespace ProjectCore.ProjectShared
{
    public interface IUiShowable
    {
        UniTask Show(bool isImmediately);
        UniTask Hide(bool isImmediately);
    }
    
    public static class UiShowableExtexensions
    {
        public static UniTask AffectVisibility(this IUiShowable showable, bool isVisible, bool isImmediately = false)
        {
            if (isVisible)
            {
                return showable.Show(isImmediately);
            }
            else
            {
                return showable.Hide(isImmediately);
            }
        }
    }
    
    public interface IScreen : IUiShowable
    {
    }
    
    public interface IPopup : IUiShowable
    {
    }
}