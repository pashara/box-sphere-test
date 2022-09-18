using Cysharp.Threading.Tasks;
using ProjectShared;
using ThirdParty.EventBus;
using ThirdParty.TextMeshPro.UniRxExtensions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.BattleUi.Panels
{
    public class BattleConfigurePanelView : MonoBehaviour, IUiShowable
    {
        [SerializeField] private TMP_InputField horizontalSize;
        [SerializeField] private TMP_InputField verticalSize;
        [SerializeField] private Button startBattleButton;
        [SerializeField] private GameObject root;

        private IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            Subscribe();
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

        private void Subscribe()
        {
            horizontalSize.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (int.TryParse(x, out var value))
                    _eventBus.Send(EventKeys.HorizontalSizeChanged, value);
            }).AddTo(gameObject);
            
            verticalSize.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (int.TryParse(x, out var value))
                    _eventBus.Send(EventKeys.VerticalSizeChanged, value);
            }).AddTo(gameObject);
            
            startBattleButton.OnClickAsObservable().Subscribe(_ =>
            {
                _eventBus.Send(EventKeys.BattleStartClick);
            }).AddTo(gameObject);
        }
    }
}