using Cysharp.Threading.Tasks;
using Project.Configs.CharacterViewsProviding;
using ProjectShared.Battler;
using UnityEngine;
using Zenject;

namespace Project.Factories
{
    public interface ICharacterViewFactory
    {
        UniTask<ICharacterView> Create(ViewType viewType);
    }
    
    public class CharacterViewFactory : ICharacterViewFactory
    {
        private readonly DiContainer _container;
        private readonly ICharacterViewProvider _characterViewProvider;

        public CharacterViewFactory(DiContainer container, ICharacterViewProvider characterViewProvider)
        {
            _container = container;
            _characterViewProvider = characterViewProvider;
        }
        
        public async UniTask<ICharacterView> Create(ViewType viewType)
        {
            var viewPrefab = _characterViewProvider.GetViewPrefab(viewType);
            if (viewPrefab == null)
            {
                return null;
            }
            return _container.InstantiatePrefabForComponent<ICharacterView>(viewPrefab);
        }
    }
}
