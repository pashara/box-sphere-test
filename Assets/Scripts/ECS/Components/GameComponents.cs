using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace ECS.Components
{
    [Team]
    public class TeamIdComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int Value;
    }
}
