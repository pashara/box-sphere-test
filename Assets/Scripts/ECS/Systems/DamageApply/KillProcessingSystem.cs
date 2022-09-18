using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace ECS.Systems.DamageApply
{
    public class KillProcessingSystem : ReactiveSystem<BattlerEntity>
    {
        private readonly Contexts _context;
        private HashSet<int> cache = new();
        public KillProcessingSystem(Contexts context) : base(context.battler)
        {
            _context = context;
        }

        protected override ICollector<BattlerEntity> GetTrigger(IContext<BattlerEntity> context)
        {
            return context.CreateCollector(BattlerMatcher.Killed);
        }

        protected override bool Filter(BattlerEntity entity)
        {
            return entity.isKilled;
        }

        protected override void Execute(List<BattlerEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.isAlive = false;
                
                _context.stats.GetEntityWithBattlerSourceId(entity.id.Value)?.Destroy();
                
                //TODO
                GameObject.Destroy((entity.characterReference.Value as MonoBehaviour).gameObject);
                
                entity.Destroy();
                
                
            }

            foreach (var i in cache)
            {
                foreach (var battlerEntity in _context.battler.GetEntitiesWithAttackTarget(i).ToList())
                {
                    battlerEntity.RemoveAttackTarget();
                }
            }
            cache.Clear();
        }
    }
}