using System.Linq;
using ProjectShared.Battler;
using ThirdParty.ComponentsBaker;
using UnityEngine;

namespace Project.Battlers.ViewConfiguring
{
    public class CharacterComponentsBaker : ComponentsBaker
    {
        [SerializeField] private GameObject target;
        
        protected override void FindComponents()
        {
            var components = gameObject.GetComponentsInChildren<BaseCharacterComponent>();
            var provider = target.GetComponent<ICharacterComponentsProvider>();
            var container = (provider.CharacterComponentsContainer as CharacterComponentsContainer);
            
            container.Clear();
            foreach (var component in components)
            {
                container.Put(component);
            }
        }
    }
}
