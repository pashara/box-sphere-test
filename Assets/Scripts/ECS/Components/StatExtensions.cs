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
            { StatType.Size, StatsComponentsLookup.Size },
        };
        
        private static Dictionary<StatType, Type> _statComponentTypes = new()
        {
            { StatType.HealthPoints, typeof(HealthPointsComponent) },
            { StatType.AttackSpeed, typeof(AttackSpeedComponent) },
            { StatType.Speed, typeof(SpeedComponent) },
            { StatType.AttackPoints, typeof(AttackPointsComponent) },
            { StatType.Size, typeof(SizeComponent) },
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

        public static void Copy(this StatsEntity source, StatsEntity destination)
        {
            foreach (var statComponentType in _statComponentTypes)
            {
                var statType = statComponentType.Key;
                var type = statComponentType.Value;
                if (!_statIndexes.TryGetValue(statType, out var index))
                {
                    continue;
                }

                var statComponent = source.GetComponent(index) as IStatComponent;
                var component = destination.CreateComponent(index, type) as IStatComponent;

                if (component == null || statComponent == null) continue;
                
                component.StatValue = statComponent.StatValue;
                destination.ReplaceComponent(index, component);
            }

        }
    }
}