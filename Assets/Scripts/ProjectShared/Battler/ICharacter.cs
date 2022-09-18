using UnityEngine;

namespace ProjectShared.Battler
{
    public interface ICharacter : ICharacterComponentsProvider
    {
        IPositionHandler PositionHandler { get; }
        IRotationHandler RotationHandler { get; }

        void Initialize(ICharacterView characterView);
    }

    public interface IPositionHandler
    {
        Vector3 Position { get; }
        void ApplyPosition(Vector3 position);
    }

    public interface IRotationHandler
    {
        Quaternion Rotation { get; }
        void ApplyRotation(Quaternion rotation);
    }
}