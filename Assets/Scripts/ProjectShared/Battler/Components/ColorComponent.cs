using UniRx;
using UnityEngine;

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
    
    public interface ISizeComponent : ICharacterComponent, IReactiveFloatValue
    {
    }
    
    public interface IFindAreaComponent : ICharacterComponent, IReactiveFloatValue
    {
    }
}