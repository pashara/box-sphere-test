using System.Collections.Generic;
using Entitas;
using ProjectShared;
using ProjectShared.Battler.Components;
using Zenject;

namespace ECS.Systems.StatsApplySystems
{
    public class ColorViewApplierSystem : ReactiveSystem<BattlerEntity>
    {
        [Inject] private ICharacterColorsProvider _characterColorsProvider;

        public ColorViewApplierSystem(Contexts context) : base(context.battler)
        {
        }

        protected override ICollector<BattlerEntity> GetTrigger(IContext<BattlerEntity> context)
        {
            return context.CreateCollector(BattlerMatcher.AnyOf(BattlerMatcher.BattlerColorType,
                BattlerMatcher.CharacterReference));
        }

        protected override bool Filter(BattlerEntity entity)
        {
            return entity.hasCharacterReference && entity.hasBattlerColorType;
        }

        protected override void Execute(List<BattlerEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.characterReference.Value.CharacterComponentsContainer
                    .TryGet<IColorComponent>(out var component))
                {
                    component.Color.Value = _characterColorsProvider.Get(entity.battlerColorType.Value);
                }
            }
        }
    }
}