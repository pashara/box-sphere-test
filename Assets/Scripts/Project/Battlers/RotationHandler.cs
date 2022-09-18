using ProjectShared.Battler;
using UnityEngine;

namespace Project.Battlers
{
    public class RotationHandler : MonoBehaviour, IRotationHandler
    {
        [SerializeField] private Transform handledItem;

        public Quaternion Rotation => handledItem.rotation;
        
        public void ApplyRotation(Quaternion rotation)
        {
            handledItem.rotation = rotation;
        }
    }
}