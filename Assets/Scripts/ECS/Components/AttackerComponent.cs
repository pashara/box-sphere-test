using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace ECS.Components
{
    [Battler]
    [Attacker]
    [Stats]
    [Input]
    [Team]
    public class IdComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int Value;
    }
    
    
    [Attacker]
    public class SourceAttack : IComponent
    {
        public int BattlerId;
    }
    [Attacker]
    public class DestinationAttack : IComponent
    {
        public int BattlerId;
    }
    
    [Attacker]
    public class StatsSourceCopyComponent : IComponent
    {
        public StatsEntity Value;
    }
}