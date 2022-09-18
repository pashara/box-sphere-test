using System.Collections.Generic;
using System.Linq;
using ProjectShared.Battler;
using UnityEngine;

namespace Project.Battlers.ViewConfiguring
{
    public class CharacterComponentsContainer : MonoBehaviour, ICharacterComponentsContainer
    {
        [SerializeField] private List<BaseCharacterComponent> characterComponents = new(); 
        
        public IReadOnlyList<ICharacterComponent> Components => characterComponents;
        
        
        public bool TryGet<TType>(out TType component) where TType : ICharacterComponent
        {
            component = default;
            var requestResult = characterComponents.FirstOrDefault(x => x is TType);
            if (requestResult == null)
                return false;

            component = (TType)(requestResult as ICharacterComponent);
            return true;
        }


        public void Put(BaseCharacterComponent item)
        {
            characterComponents.Add(item);
        }

        public void Clear()
        {
            characterComponents.Clear();
        }
        
        
        public void Merge(ICharacterComponentsContainer container)
        {
            foreach (var component in container.Components)
            {
                characterComponents.Add(component as BaseCharacterComponent);
            }
        }
    }
}