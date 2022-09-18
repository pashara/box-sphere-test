using Entitas;

namespace ECS.Components
{
    
    public interface IStatComponent : IComponent 
    {
        public float StatValue { get; set; }
    }

    [Stats]
    public class HealthPointsComponent : IStatComponent
    {
        public float Value;

        float IStatComponent.StatValue
        {
            get => Value;
            set => Value = value;
        }
    }
    
    [Stats]
    public class AttackPointsComponent : IStatComponent
    {
        public float Value;
        

        float IStatComponent.StatValue
        {
            get => Value;
            set => Value = value;
        }
    }
    
    [Stats]
    public class SpeedComponent : IStatComponent
    {
        public float Value;
        

        float IStatComponent.StatValue
        {
            get => Value;
            set => Value = value;
        }
    }
    
    [Stats]
    public class AttackSpeedComponent : IStatComponent
    {
        public float Value;
        

        float IStatComponent.StatValue
        {
            get => Value;
            set => Value = value;
        }
    }
}