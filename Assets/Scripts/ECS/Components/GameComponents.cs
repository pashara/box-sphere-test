using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace ECS.Components
{
    [Team]
    public class TeamIdComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int Value;
    }
}
