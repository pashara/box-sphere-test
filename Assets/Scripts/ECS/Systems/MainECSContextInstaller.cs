using ECS.Systems.DamageApply;
using Entitas;
using ProjectShared;
using UniRx;
using Zenject;

namespace ECS.Systems
{
    public class MainECSContextInstaller : IECSContext, IInitializable
    {
        private readonly DiContainer _container;
        private readonly IGameplayTicker _gameplayTicker;
        private readonly DiFeature _systems;
        public Contexts Contexts { get; }
        private int idCounter = 0;
        
        public MainECSContextInstaller(DiContainer container, IGameplayTicker gameplayTicker)
        {
            idCounter = 0;
            _container = container;
            _gameplayTicker = gameplayTicker;
            Contexts = new Contexts();
            _systems = new DiFeature(container, "BattleSystems");
            ListenContextCreation();
        }

        public void Initialize()
        {
            _systems.Add(new StatsApplySystems.StatsApplySystems(Contexts, _container, "StatsConfigurators"));
            
            _systems.Add(new DamageApplySystem(Contexts));
            _systems.Add(new HealthCheckSystem(Contexts));
            _systems.Add(new KillProcessingSystem(Contexts));

            _gameplayTicker.OnTick.Subscribe(Tick);
        }

        private void Tick(float deltaTime)
        {
            _systems.Execute();
        }

        private void ListenContextCreation()
        {
            Contexts.attacker.OnEntityCreated += AttackerOnOnEntityCreated;
            Contexts.battler.OnEntityCreated += AttackerOnOnEntityCreated;
            Contexts.input.OnEntityCreated += AttackerOnOnEntityCreated;
            Contexts.stats.OnEntityCreated += AttackerOnOnEntityCreated;
            Contexts.team.OnEntityCreated += AttackerOnOnEntityCreated;
        }

        private void AttackerOnOnEntityCreated(IContext context, IEntity entity)
        {
            if (entity is IIdEntity idEntity)
            {
                idEntity.AddId(idCounter);
                idCounter++;
            }
        }
    }
}