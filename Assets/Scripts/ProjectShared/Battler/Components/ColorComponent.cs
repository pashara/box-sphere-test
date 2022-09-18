using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectShared.Battler.Components
{
    public interface IReactiveFloatValue
    {
        ReactiveProperty<float> Value { get; }
    }
    
    public interface IColorComponent : ICharacterComponent
    {
        ReactiveProperty<Color> Color { get; }
    }
    
    public interface ITeamIndicatorComponent : ICharacterComponent
    {
        ReactiveProperty<Color> Color { get; }
    }
    
    public interface IAgentComponent : ICharacterComponent
    {
        NavMeshAgent Value { get; }
    }
    
    public interface ISizeComponent : ICharacterComponent, IReactiveFloatValue
    {
    }
    public interface ICharacterDetectorComponent : ICharacterComponent, IReactiveFloatValue
    {
        HashSet<ICharacter> HandledCharacters { get; }
    }
}