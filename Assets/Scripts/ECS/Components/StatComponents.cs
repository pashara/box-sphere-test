using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace ECS.Components
{
    
    public abstract class BaseStatComponent : IStatComponent
    {
        public float Value;
        
        float IStatComponent.StatValue
        {
            get => Value;
            set => Value = value;
        }
    }
    
    public interface IStatComponent : IComponent 
    {
        public float StatValue { get; set; }
    }

    [Stats]
    public class HealthPointsComponent : BaseStatComponent
    {
    }
    
    [Stats]
    public class AttackPointsComponent : BaseStatComponent
    {
    }

    [Stats]
    public class SpeedComponent : BaseStatComponent
    {
    }
    
    [Stats]
    public class AttackSpeedComponent : BaseStatComponent
    {
    }
    
    [Stats]
    public class SizeComponent : BaseStatComponent
    {
    }

    [Stats]
    public class BattlerSourceIdComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int Value;
    }
}