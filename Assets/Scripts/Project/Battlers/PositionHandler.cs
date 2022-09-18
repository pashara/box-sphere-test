using ProjectShared.Battler;
using UnityEngine;

namespace Project.Battlers
{
    public class PositionHandler : MonoBehaviour, IPositionHandler
    {
        [SerializeField] private Transform handledItem;

        public Vector3 Position => handledItem.position;
        
        public void ApplyPosition(Vector3 position)
        {
            handledItem.position = position;
        }
    }
}