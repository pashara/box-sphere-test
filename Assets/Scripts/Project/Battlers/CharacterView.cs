using Project.Battlers.ViewConfiguring;
using Project.MonoTarget;
using ProjectShared.Battler;
using UnityEngine;

namespace Project.Battlers
{
    public class CharacterView : MonoBehaviour, ICharacterView
    {
        [SerializeField] private CharacterComponentsContainer characterComponentsContainer;
        [SerializeField] private float characterRadius;
        [SerializeField] private MonoBehaviourTarget targetToCharacter;
        
        public ICharacterComponentsContainer CharacterComponentsContainer => characterComponentsContainer;
        public GameObject GameObject => gameObject;

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
    }
}