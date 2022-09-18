using System;
using ProjectShared;
using ThirdParty.EventBus;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.BattleLogic.GridSizeInstallers
{
    public interface IGridSizeInputProvider
    {
        ReadOnlyReactiveProperty<Vector2Int> Size { get; }
    }
    
    public class GridSizeInputHandler : IInitializable, IDisposable, IGridSizeInputProvider
    {
        private CompositeDisposable _disposable = new();
        private int _verticalSize;
        private int _horizontalSize;
        private ReactiveProperty<Vector2Int> _size = new();

        public ReadOnlyReactiveProperty<Vector2Int> Size { get; }
        
        private readonly IEventBus _eventBus;

        public GridSizeInputHandler(IEventBus eventBus)
        {
            Size = new(_size);
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.WasTriggered<int>(EventKeys.HorizontalSizeChanged).Subscribe((v) =>
            {
                _size.Value = new Vector2Int(v.Item2, _size.Value.y);
            }).AddTo(_disposable);
            
            _eventBus.WasTriggered<int>(EventKeys.VerticalSizeChanged).Subscribe((v) =>
            {
                _size.Value = new Vector2Int(_size.Value.x, v.Item2);
            }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

    }
}
