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
    public class BattlerColorTypeComponent : IComponent
    {
        public CharacterColorType Value;
    }

    [Battler]
    public class TeamComponentComponent : IComponent
    {
        [EntityIndex]
        public TeamEntity Value;
    }

    [Battler]
    public class AttackTargetComponent : IComponent
    {
        [EntityIndex]
        public int Value;
    }

    [Battler]
    public class AliveComponent : IComponent { }

    [Battler]
    public class KilledComponent : IComponent { }
}