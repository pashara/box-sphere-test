using System;
using System.Collections.Generic;
using Entitas;
using ProjectShared.Battler;
using UnityEngine;

namespace ECS.Components
{
    public static class StatExtensions
    {
        private static Dictionary<StatType, int> _statIndexes = new()
        {
            { StatType.HealthPoints, StatsComponentsLookup.HealthPoints },
            { StatType.AttackSpeed, StatsComponentsLookup.AttackSpeed },
            { StatType.Speed, StatsComponentsLookup.Speed },
            { StatType.AttackPoints, StatsComponentsLookup.AttackPoints },
        };
        
        private static Dictionary<StatType, Type> _statComponentTypes = new()
        {
            { StatType.HealthPoints, typeof(HealthPointsComponent) },
            { StatType.AttackSpeed, typeof(AttackSpeedComponent) },
            { StatType.Speed, typeof(SpeedComponent) },
            { StatType.AttackPoints, typeof(AttackPointsComponent) },
        };
        
        public static bool TryGetStat(this StatsEntity entity, StatType statType, ref float value)
        {
            return _statIndexes.TryGetValue(statType, out var index) &&
                   TryGetStatValue(entity, index, ref value);
        }

        public static int GetStatComponentIndex(this StatsEntity entity, StatType statType)
        {
            return _statIndexes[statType];
        }

        public static IStatComponent GetStatComponent(this StatsEntity entity, StatType statType)
        {
            var index = _statIndexes[statType];
            return (entity.HasComponent(index)) ? entity.GetComponent(index) as IStatComponent : null;
        }

        private static bool TryGetStatValue(this IEntity entity, int index, ref float value)
        {
            if (entity.HasComponent(index) && entity.GetComponent(index) is IStatComponent statComponent)
            {
                value = statComponent.StatValue;
                return true;
            }

            return false;
        }

        public static void ReplaceStatValue(this StatsEntity entity, StatType statType, float value)
        {
            if (!_statComponentTypes.TryGetValue(statType, out var type) ||
                !_statIndexes.TryGetValue(statType, out var index))
            {
                Debug.LogError("Not correct ECS config for stats");
                return;
            }
            
            var component = entity.CreateComponent(index, type) as IStatComponent;
            component.StatValue = value;

            entity.ReplaceComponent(index, component);

        }
    }
}