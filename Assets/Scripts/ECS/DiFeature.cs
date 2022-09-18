using Entitas;
using Zenject;

namespace ECS
{
    public class DiFeature : Feature
    {
        private DiContainer _diContainer;

        public DiFeature(DiContainer diContainer, string name) : base(name)
        {
            _diContainer = diContainer;
        }
        
        public sealed override Entitas.Systems Add(ISystem system)
        {
            return base.Add(Inject(system));
        }

        protected T Inject<T>(T system)
        {
            _diContainer.Inject(system);
            return system;
        }
    }
}