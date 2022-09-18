using Project.Battlers.ViewConfiguring;
using Project.MonoTarget;
using ProjectShared.Battler;
using UnityEngine;

namespace Project.Battlers
{
    public class CharacterWrapper : MonoBehaviour, ICharacter
    {
        [SerializeField] private Transform viewRoot;
        [SerializeField] private PositionHandler positionHandler;
        [SerializeField] private RotationHandler rotationHandler;
        [SerializeField] private CharacterComponentsContainer baseCharacterComponentsContainer;
        [SerializeField] private GameObject aiRoot;
        private CharacterComponentsContainer _characterComponentsContainer;

        private CharacterComponentsContainer ComponentsContainer
        {
            get
            {
                if (_characterComponentsContainer == null)
                {
                    _characterComponentsContainer = gameObject.AddComponent<CharacterComponentsContainer>();
                    _characterComponentsContainer.Merge(baseCharacterComponentsContainer);
                }

                return _characterComponentsContainer;
            }
        }

        ICharacterComponentsContainer ICharacterComponentsProvider.CharacterComponentsContainer => ComponentsContainer;
        
        
        public IPositionHandler PositionHandler => positionHandler;
        public IRotationHandler RotationHandler => rotationHandler;
        public GameObject AIRoot => aiRoot;

        public void Initialize(ICharacterView characterView)
        {
            characterView.SetParent(viewRoot);
            characterView.GameObject.GetComponent<MonoBehaviourTarget>()?.SetTarget(this);
            ComponentsContainer.Merge(characterView.CharacterComponentsContainer);
        }
    }
}