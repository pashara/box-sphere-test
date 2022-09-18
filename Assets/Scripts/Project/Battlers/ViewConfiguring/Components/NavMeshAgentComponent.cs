using ProjectShared.Battler.Components;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Battlers.ViewConfiguring.Components
{
    public class NavMeshAgentComponent : BaseCharacterComponent, IAgentComponent
    {
        [SerializeField] private NavMeshAgent agent;
        public NavMeshAgent Value => agent;
    }
}