using System.Collections.Generic;
using Entitas;
using ProjectShared;
using ProjectShared.Battler.Components;

namespace ECS.Systems.StatsApplySystems
{
    public class CharacterScaleApplySystem : ReactiveSystem<StatsEntity>
    {
        private readonly Contexts _context;

        public CharacterScaleApplySystem(Contexts context) : base(context.stats)
        {
            _context = context;
        }

        protected override ICollector<StatsEntity> GetTrigger(IContext<StatsEntity> context)
        {
            return context.CreateCollector(StatsMatcher.AnyOf(StatsMatcher.Size,
                StatsMatcher.BattlerSourceId));
        }

        protected override bool Filter(StatsEntity entity)
        {
            return entity.hasBattlerSourceId && entity.hasSize;
        }

        protected override void Execute(List<StatsEntity> entities)
        {
            foreach (var entity in entities)
            {
                var battler = _context.battler.GetEntityWithId(entity.battlerSourceId.Value);
                if (battler != null && battler.characterReference.Value.CharacterComponentsContainer
                        .TryGet(out ISizeComponent sizeComponent))
                {
                    sizeComponent.Value.Value = entity.size.Value;
                }
            }
        }
    }
}