using System.Collections.Generic;
using ECS.Components;
using Entitas;
using ProjectShared.Battler;
using UnityEngine;

namespace ECS.Systems.DamageApply
{
    public class DamageApplySystem : ReactiveSystem<AttackerEntity>
    {
        private HashSet<AttackerEntity> _processed = new();
        private readonly Contexts _context;

        public DamageApplySystem(Contexts context) : base(context.attacker)
        {
            _context = context;
        }

        protected override ICollector<AttackerEntity> GetTrigger(IContext<AttackerEntity> context)
        {
            return context.CreateCollector(AttackerMatcher.AllOf(AttackerMatcher.DestinationAttack, AttackerMatcher.StatsSourceCopy));
        }

        protected override bool Filter(AttackerEntity entity)
        {
            return entity.hasDestinationAttack && entity.hasStatsSourceCopy;
        }

        protected override void Execute(List<AttackerEntity> entities)
        {
            foreach (var entity in entities)
            {
                var statsEntity = entity.statsSourceCopy.Value;
                var damage = 0f;
                if (statsEntity.TryGetStat(StatType.AttackPoints, ref damage))
                {
                    var destinationBattlerId = entity.destinationAttack.BattlerId;
                    var battlerStats = _context.stats.GetEntityWithBattlerSourceId(destinationBattlerId);
                    var hp = 0f;
                    if (battlerStats != null && battlerStats.TryGetStat(StatType.HealthPoints, ref hp))
                    {
                        hp -= damage;
                        
                        battlerStats.ReplaceStatValue(StatType.HealthPoints, Mathf.Max(hp, 0f));
                    }
                }

                _processed.Add(entity);
            }

            foreach (var entity in _processed)
            {
                entity.statsSourceCopy.Value.Destroy();
                entity.Destroy();
            }
            _processed.Clear();
        }
    }
}