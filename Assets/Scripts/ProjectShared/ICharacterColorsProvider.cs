using UnityEngine;

namespace ProjectShared
{
    public interface ICharacterColorsProvider
    {
        Color Get(CharacterColorType colorType);
    }
}