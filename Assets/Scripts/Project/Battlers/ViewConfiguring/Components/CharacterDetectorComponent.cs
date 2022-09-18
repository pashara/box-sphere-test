using System.Collections.Generic;
using Project.MonoTarget;
using ProjectShared.Battler;
using ProjectShared.Battler.Components;
using UniRx;
using UnityEngine;

namespace Project.Battlers.ViewConfiguring.Components
{
    public class CharacterDetectorComponent : BaseCharacterComponent, ICharacterDetectorComponent
    {
        [SerializeField] private SphereCollider detectorCollider;


        public HashSet<ICharacter> HandledCharacters { get; } = new();

        private ReactiveProperty<float> _value;

        public ReactiveProperty<float> Value
        {
            get
            {
                if (_value != null) return _value;
                
                _value = new ReactiveProperty<float>(detectorCollider.radius);
                _value.Subscribe(x =>
                {
                    detectorCollider.radius = x;
                }).AddTo(this);

                return _value;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            var character = other.GetComponent<IMonoBehaviourTarget>()?.GetTarget<ICharacter>();
            if (character != null)
            {
                HandledCharacters.Add(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var character = other.GetComponent<IMonoBehaviourTarget>()?.GetTarget<ICharacter>();
            if (character != null)
            {
                HandledCharacters.Remove(character);
            }
        }
    }
}