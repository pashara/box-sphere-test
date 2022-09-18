using Cysharp.Threading.Tasks;
using ProjectCore.BattleLogic.BattleUi.Panels;
using ProjectCore.BattleLogic.BattleUi.VisibilityServices;
using ProjectCore.ProjectShared;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectCore.BattleLogic.BattleUi
{
    public class BattleScreen : MonoBehaviour, IScreen
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private BattleConfigurePanelView battleConfigurePanelView;
        [SerializeField] private StopBattlePanelView stopBattlePanelView;
        
        private readonly CompositeDisposable _disposable = new();
        
        private IBattleConfigureVisibilityService _battleConfigureVisibilityService;
        private IBattleCancelVisibilityService _cancelBattleVisibilityService;

        [Inject]
        private void Construct(
            IBattleConfigureVisibilityService battleConfigureVisibilityService,
            IBattleCancelVisibilityService cancelBattleVisibilityService
            )
        {
            _battleConfigureVisibilityService = battleConfigureVisibilityService;
            _cancelBattleVisibilityService = cancelBattleVisibilityService;
            _disposable.Clear();
            Prepare();
        }

        private void OnDestroy()
        {
            _disposable.Clear();
            _disposable.Dispose();
        }

        public UniTask Show(bool isImmediately)
        {
            // mainCanvas.enabled = true;
            mainCanvas.gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide(bool isImmediately)
        {
            // mainCanvas.enabled = false;
            mainCanvas.gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
        
        private void Prepare()
        {
            Show(true);

            _battleConfigureVisibilityService.OnVisibleState.Subscribe(isVisible =>
            {
                battleConfigurePanelView.AffectVisibility(isVisible);
            }).AddTo(_disposable);
            
            
            _cancelBattleVisibilityService.OnVisibleState.Subscribe(isVisible =>
            {
                stopBattlePanelView.AffectVisibility(isVisible);
            }).AddTo(_disposable);
        }
    }
}
