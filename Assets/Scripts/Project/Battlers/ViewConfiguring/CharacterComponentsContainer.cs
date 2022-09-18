using System.Collections.Generic;
using System.Linq;
using ProjectShared.Battler;
using UnityEngine;

namespace Project.Battlers.ViewConfiguring
{
    public class CharacterComponentsContainer : MonoBehaviour, ICharacterComponentsContainer
    {
        [SerializeField] private List<BaseCharacterComponent> _characterComponents; 
        
        public IReadOnlyList<ICharacterComponent> Components => _characterComponents;
        
        
        public bool TryGet<TType>(out TType component) where TType : ICharacterComponent
        {
            component = default;
            var requestResult = _characterComponents.FirstOrDefault(x => x is TType);
            if (requestResult == null)
                return false;

            component = (TType)(requestResult as ICharacterComponent);
            return true;
        }


        public void Put(BaseCharacterComponent item)
        {
            _characterComponents.Add(item);
        }

        public void Clear()
        {
            _characterComponents.Clear();
        }
        
        
        public void Merge(ICharacterComponentsContainer container)
        {
            foreach (var component in container.Components)
            {
                _characterComponents.Add(component as BaseCharacterComponent);
            }
        }
    }
}