using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace ECS.Systems.DamageApply
{
    public class HealthCheckSystem : ReactiveSystem<StatsEntity>
    {
        private readonly Contexts _context;

        public HealthCheckSystem(Contexts context) : base(context.stats)
        {
            _context = context;
        }

        protected override ICollector<StatsEntity> GetTrigger(IContext<StatsEntity> context)
        {
            return context.CreateCollector(StatsMatcher.HealthPoints);
        }

        protected override bool Filter(StatsEntity entity)
        {
            return entity.hasHealthPoints;
        }

        protected override void Execute(List<StatsEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (Mathf.Approximately(entity.healthPoints.Value, 0f))
                {
                    if (entity.hasBattlerSourceId)
                    {
                        var battlerEntity = _context.battler.GetEntityWithId(entity.battlerSourceId.Value);

                        if (battlerEntity is { isKilled: false })
                        {
                            battlerEntity.isKilled = true;
                        }
                    }
                }
            }
        }
    }
}