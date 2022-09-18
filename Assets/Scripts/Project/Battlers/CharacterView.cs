using Project.Battlers.ViewConfiguring;
using ProjectShared.Battler;
using UnityEngine;

namespace Project.Battlers
{
    public class CharacterView : MonoBehaviour, ICharacterView
    {
        [SerializeField] private CharacterComponentsContainer characterComponentsContainer;
        
        public ICharacterComponentsContainer CharacterComponentsContainer => characterComponentsContainer;
        
        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
    }
}