using ProjectShared.Battler.Components;
using UniRx;
using UnityEngine;

namespace Project.Battlers.ViewConfiguring.Components
{
    public class SizeComponent : BaseCharacterComponent, ISizeComponent
    {
        [SerializeField] private Transform sizeRoot;

        private ReactiveProperty<float> _value;

        public ReactiveProperty<float> Value
        {
            get
            {
                if (_value != null) return _value;
                
                _value = new ReactiveProperty<float>(sizeRoot.localScale.x);
                _value.Subscribe(x =>
                {
                    sizeRoot.transform.localScale = Vector3.one * x;
                }).AddTo(this);

                return _value;
            }
        }
    }
}