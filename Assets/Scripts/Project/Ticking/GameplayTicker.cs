using System;
using ProjectShared;
using UniRx;
using UnityEngine;

namespace Project.Ticking
{
    public class GameplayTicker : IGameplayTicker, IDisposable
    {
        private readonly Subject<float> _tick = new();
        private CompositeDisposable _disposable = new();
        public IObservable<float> OnTick => _tick;
        public bool IsActive { get; }

        public GameplayTicker()
        {
            IsActive = true;
            Observable.EveryUpdate().Where(x => IsActive).Subscribe(x =>
            {
                _tick.OnNext(Time.deltaTime);
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _tick?.Dispose();
            _disposable?.Dispose();
        }
    }
}