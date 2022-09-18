using Project.Configs.PrefabsProviding;
using ProjectShared.Battler;
using Zenject;

namespace Project.Factories
{
    public interface ICharacterFactory
    {
        ICharacter Create();
    }

    public class CharacterFactory : ICharacterFactory
    {
        private readonly DiContainer _container;
        private readonly IPrefabsProvider _prefabsProvider;

        public CharacterFactory(DiContainer container, IPrefabsProvider prefabsProvider)
        {
            _container = container;
            _prefabsProvider = prefabsProvider;
        }
        
        public ICharacter Create()
        {
            return _container.InstantiatePrefabForComponent<ICharacter>(
                _prefabsProvider.GetGameObject((int)PrefabKeys.CharacterWrapper));
        }
    }
}
