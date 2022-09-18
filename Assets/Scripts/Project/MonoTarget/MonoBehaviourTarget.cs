using UnityEngine;

namespace Project.MonoTarget
{
    public class MonoBehaviourTarget : MonoBehaviour, IMonoBehaviourTarget
    {
        [SerializeField] private MonoBehaviour target;
        public MonoBehaviour Target => target;

        public void SetTarget(MonoBehaviour target)
        {
            this.target = target;
        }
    }
}