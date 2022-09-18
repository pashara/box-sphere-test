using UniRx;
using UnityEngine;

namespace Project.Battlers.ViewConfiguring.Components
{
    public abstract class BaseMeshColorComponent : BaseCharacterComponent
    {
        [SerializeField] MeshRenderer _meshRenderer;

        private ReactiveProperty<Color> _value = null;

        public ReactiveProperty<Color> Color
        {
            get
            {
                if (_value != null) return _value;
                
                var materialInstance = new Material(_meshRenderer.sharedMaterial);
                _meshRenderer.material = materialInstance;
                _value = new ReactiveProperty<Color>(materialInstance.color);
                _value.Subscribe(x =>
                {
                    materialInstance.color = x;
                }).AddTo(this);

                return _value;
            }
        }
    }
}