using Entitas;
using Entitas.CodeGeneration.Attributes;
using ProjectShared;
using ProjectShared.Battler;
using ProjectShared.Battler.Components;

namespace ECS.Components
{
    [Battler]
    public class CharacterReferenceComponent : IComponent
    {
        [PrimaryEntityIndex]
        public ICharacter Value;
    }

    [Battler]
    public class StatsComponent : IComponent
    {
        public StatsEntity Value;
    }

    [Battler]
    public class BattlerColorTypeComponent : IComponent
    {
        public CharacterColorType Value;
    }

    [Battler]
    public class TeamIdComponent : IComponent
    {
        public int Value;
    }
}