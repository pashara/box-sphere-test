using UnityEngine;

namespace Project.MonoTarget
{
    public class MonoBehaviourTargetReference : MonoBehaviour, IMonoBehaviourTarget
    {
        [SerializeField] private MonoBehaviourTarget target;
        public MonoBehaviour Target => target.Target;
    }
}