using System;

namespace ProjectShared
{
    public interface IGameplayTicker
    {
        IObservable<float> OnTick { get; }
    }
}