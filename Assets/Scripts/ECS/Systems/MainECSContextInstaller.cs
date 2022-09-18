using ProjectShared;
using UniRx;
using Zenject;

namespace ECS.Systems
{
    public class MainECSContextInstaller : IECSContext, IInitializable
    {
        private readonly DiContainer _container;
        private readonly IGameplayTicker _gameplayTicker;
        private readonly Feature _systems;
        public Contexts Contexts { get; }
        
        public MainECSContextInstaller(DiContainer container, IGameplayTicker gameplayTicker)
        {
            _container = container;
            _gameplayTicker = gameplayTicker;
            Contexts = new Contexts();
            _systems = new Feature("BattleSystems");
        }

        public void Initialize()
        {
                
            // _systems.


            _gameplayTicker.OnTick.Subscribe(Tick);
        }

        private void Tick(float deltaTime)
        {
            
        }
    }
}