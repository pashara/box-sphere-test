using System.Collections.Generic;
using UnityEngine;

namespace ProjectShared.Battler
{
    
    public interface IComponentsContainer<T>
    {
        bool TryGet<TType>(out TType component) where TType : T;

        IReadOnlyList<T> Components { get; }
    }

    public interface ICharacterComponent
    {
        
    }
    public interface ICharacterComponentsContainer : IComponentsContainer<ICharacterComponent>
    {
        
    }

    public interface ICharacterComponentsProvider
    {
        ICharacterComponentsContainer CharacterComponentsContainer { get; }
    }
    
    public interface ICharacterView : ICharacterComponentsProvider
    {
        GameObject GameObject { get; }
        void SetParent(Transform parent);
    }
}