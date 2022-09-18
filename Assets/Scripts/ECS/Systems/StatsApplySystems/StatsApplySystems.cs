using Zenject;

namespace ECS.Systems.StatsApplySystems
{
    public class StatsApplySystems : DiFeature
    {
        public StatsApplySystems(Contexts contexts, DiContainer diContainer, string name) : base(diContainer, name)
        {
            Add(new ColorViewApplierSystem(contexts));
            Add(new CharacterScaleApplySystem(contexts));
            Add(new SpeedApplierSystem(contexts));
        }
    }
}