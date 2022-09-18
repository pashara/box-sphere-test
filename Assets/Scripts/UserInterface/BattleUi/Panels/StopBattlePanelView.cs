using Cysharp.Threading.Tasks;
using ProjectCore.ProjectShared;
using ThirdParty.EventBus;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectCore.BattleLogic.BattleUi.Panels
{
    public class StopBattlePanelView : MonoBehaviour, IUiShowable
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Button stopButton;
        
        private IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            Prepare();
        }

        public UniTask Show(bool isImmediately)
        {
            // mainCanvas.enabled = true;
            root.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide(bool isImmediately)
        {
            // mainCanvas.enabled = false;
            root.SetActive(false);
            return UniTask.CompletedTask;
        }

        private void Prepare()
        {
            stopButton.OnCancelAsObservable().Subscribe(x =>
            {
                _eventBus.Send(EventKeys.CancelBattleClick);
            }).AddTo(this);
        }
    }
}
